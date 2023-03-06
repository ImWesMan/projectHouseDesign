using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class programStateManager : MonoBehaviour
{
    [SerializeField] private List<string> names = new List<string>();
    [SerializeField] private List<string> sizes = new List<string>();
    
    private string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/lists.json";
        LoadLists();
        fillCustomItemBank();
    }

    // Save the lists to a file
    public void SaveLists()
    {
        populateCustomItemLists();
        ListData data = new ListData(names, sizes);

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
}

[System.Serializable]
public class ListData
{
    public List<string> names;
    public List<string> sizes;

    public ListData(List<string> names, List<string> sizes)
    {
        this.names = names;
        this.sizes = sizes;
    }
}