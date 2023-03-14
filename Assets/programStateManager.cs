using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class programStateManager : MonoBehaviour
{
    [SerializeField] private List<string> names = new List<string>();
    [SerializeField] private List<string> sizes = new List<string>();
    [SerializeField] private List<string> workspaceNames = new List<string>();
    [SerializeField] private List<int> workspaceHeights = new List<int>();
    [SerializeField] private List<int> workspaceWidths = new List<int>();
    [SerializeField] private List<int> floorCounts = new List<int>();
    public GameObject[] workspaces;
    private string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/lists.json";
        LoadLists();
        fillCustomItemBank();
        fillWorkspaceButtons();
        populateFloors();
    }

    // Save the lists to a file
    public void SaveLists()
    {
        populateCustomItemLists();
        populateWorkspaceLists();
        ListData data = new ListData(names, sizes, workspaceNames,workspaceHeights,workspaceWidths,floorCounts);

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    // Load the lists from a file
    public void LoadLists()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ListData data = JsonConvert.DeserializeObject<ListData>(json);

            names = data.names;
            sizes = data.sizes;
            workspaceNames = data.workspaceNames;
            workspaceWidths = data.workspaceWidths;
            workspaceHeights = data.workspaceHeights;
            floorCounts = data.floorCounts;
        }
    }

    // Example method to add some data to the lists
    public void populateCustomItemLists()
    {
        names.Clear();
        sizes.Clear();
        GameObject[] labelObjects = GameObject.FindGameObjectsWithTag("Label");
        GameObject[] dimensionObjects = GameObject.FindGameObjectsWithTag("Sizes");
        foreach (GameObject labelObj in labelObjects)
        {
            names.Add(labelObj.GetComponent<TMP_Text>().text);
        }
        foreach (GameObject dimensionObj in dimensionObjects)
        {
            sizes.Add(dimensionObj.GetComponent<TMP_Text>().text);
        }
    }

    public void fillCustomItemBank()
    {
        for(int i = 0; i < names.Count; i++)
        {
            GameObject.FindGameObjectWithTag("GridManager").GetComponent<AddCustomItem>().Create(names[i], sizes[i]);
        }
    }

    public void populateWorkspaceLists()
    {
        workspaceNames.Clear();
        workspaceHeights.Clear();
        workspaceWidths.Clear();
        floorCounts.Clear();
        GameObject[] nameObjects = GameObject.FindGameObjectsWithTag("workspaceButton");
        foreach (GameObject nameObj in nameObjects)
        {
            workspaceNames.Add(nameObj.transform.Find("WorkspaceName").gameObject.GetComponent<TMP_Text>().text);
            string[] dimensions = (nameObj.transform.Find("Dimensions").gameObject.GetComponent<TMP_Text>().text.Split(" x "));
            workspaceHeights.Add(int.Parse(dimensions[1]));
            workspaceWidths.Add(int.Parse(dimensions[0]));
        }
        workspaces = GameObject.Find("WorkspaceManager").GetComponent<workspace_data>().workspaces.ToArray();
        foreach(GameObject workspaceObj in workspaces)
        {
            floorCounts.Add(workspaceObj.GetComponent<workspaceInfo>().FloorCount);
        }
    }
     
    public void fillWorkspaceButtons()
    {
         for(int i = 0; i < workspaceNames.Count; i++)
        {
            GameObject.FindGameObjectWithTag("WorkspaceManager").GetComponent<workspace_data>().workspaceCreated(workspaceNames[i], workspaceWidths[i], workspaceHeights[i]);
        }
    }

    public void populateFloors()
    {
       workspaces = GameObject.Find("WorkspaceManager").GetComponent<workspace_data>().workspaces.ToArray();
       for (int i = 0; i < workspaces.Length; i++)
       {
        GameObject workspaceObj = workspaces[i];
        int numFloors = floorCounts[i];

        for (int j = 1; j < numFloors; j++)
        {
            workspaceObj.GetComponent<workspaceInfo>().addFloor(workspaceObj);
        }
    }
    }
}

[System.Serializable]
public class ListData
{
    public List<string> names;
    public List<string> sizes;
    public List<string> workspaceNames;
    public List<int> workspaceHeights;
    public List<int> workspaceWidths;
    public List<int> floorCounts;
    public ListData(List<string> names, List<string> sizes, List<string> workspaceNames, List<int> workspaceHeights, List<int> workspaceWidths, List<int> floorCounts)
    {
        this.names = names;
        this.sizes = sizes;
        this.workspaceNames = workspaceNames;
        this.workspaceHeights = workspaceHeights;
        this.workspaceWidths = workspaceWidths;
        this.floorCounts = floorCounts;
    }
}