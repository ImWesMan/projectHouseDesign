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
    private string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/lists.json";
        LoadLists();
        fillCustomItemBank();
        fillWorkspaceButtons();
    }

    // Save the lists to a file
    public void SaveLists()
    {
        populateCustomItemLists();
        populateWorkspaceLists();
        ListData data = new ListData(names, sizes, workspaceNames,workspaceHeights,workspaceWidths);

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
        GameObject[] nameObjects = GameObject.FindGameObjectsWithTag("workspaceButton");
        foreach (GameObject nameObj in nameObjects)
        {
            workspaceNames.Add(nameObj.transform.Find("WorkspaceName").gameObject.GetComponent<TMP_Text>().text);
            string[] dimensions = (nameObj.transform.Find("Dimensions").gameObject.GetComponent<TMP_Text>().text.Split(" x "));
            workspaceHeights.Add(int.Parse(dimensions[1]));
            workspaceWidths.Add(int.Parse(dimensions[0]));
        }
    }
     
    public void fillWorkspaceButtons()
    {
         for(int i = 0; i < workspaceNames.Count; i++)
        {
            GameObject.FindGameObjectWithTag("WorkspaceManager").GetComponent<workspace_data>().workspaceCreated(workspaceNames[i], workspaceWidths[i], workspaceHeights[i]);
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

    public ListData(List<string> names, List<string> sizes, List<string> workspaceNames, List<int> workspaceHeights, List<int> workspaceWidths)
    {
        this.names = names;
        this.sizes = sizes;
        this.workspaceNames = workspaceNames;
        this.workspaceHeights = workspaceHeights;
        this.workspaceWidths = workspaceWidths;
    }
}