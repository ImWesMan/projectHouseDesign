using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class workspace_data : MonoBehaviour
{
    public string current_name;
    List<string> workspace_names_list;

    public void Start() {
        workspace_names_list = new List<string>();
    }

    public void GetNewName(){
        TMP_InputField text_object = GameObject.Find("ProjectNameInput").GetComponent<TMP_InputField>();
        current_name = text_object.text;
    }

    public void AddNewNameToList(){
        workspace_names_list.Add(current_name);
        Debug.Log(current_name);
    }

    public void AddNewWorkspaceTab(){
        
    }

    public void SetNamesToWorkspace(){
        // for now, just placing the first one in
        string item = workspace_names_list[0];
        GameObject.Find("Default Workspace").GetComponentInChildren<TMP_Text>().text = item;

    }

}
