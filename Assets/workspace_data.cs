using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class workspace_data : MonoBehaviour
{
    [SerializeField]
    public GameObject workspace;
    public List<GameObject> workspaces;
    public List<GameObject> buttons;
    public string name;
    public GameObject currentWorkspace;
    [SerializeField]
    public GameObject workspaceButton;
    [SerializeField]
    public GameObject buttonHolder;
      public void workspaceCreated()
    {
       name = GameObject.FindWithTag("ProjectNameInput").GetComponent<TMP_InputField>().text;
       GameObject created = Instantiate(workspace, GameObject.FindWithTag("WorkspaceManager").transform);
       created.name = name;
       GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().GenerateGrid(created);
       AddNewWorkspaceToList(created);
       currentWorkspace = created;
       created.GetComponent<workspaceInfo>().width =  GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().width;
       created.GetComponent<workspaceInfo>().length= GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().height;
       GameObject newbutton = Instantiate(workspaceButton);
       newbutton.transform.SetParent(buttonHolder.transform);
       newbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = created.name;
       newbutton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
       newbutton.transform.localPosition = new Vector3(newbutton.transform.localPosition.x,newbutton.transform.localPosition.y,0.0f);
       newbutton.GetComponent<Image>().color = Color.green;
       newbutton.GetComponent<Button>().onClick.AddListener(() => SwitchWorkspace(newbutton));
       AddNewWorkspaceToButtonList(newbutton);

        foreach (GameObject theworkspace in workspaces)
        {
             if(theworkspace != created)
             {
                theworkspace.SetActive(false);
             }   
        }
        foreach (GameObject button in buttons)
        {
             if(button != newbutton)
             {
                button.GetComponent<Image>().color = Color.white;
             }   
        }
    }


    public void AddNewWorkspaceToList(GameObject workspace)
    {
        workspaces.Add(workspace);
    }

     public void AddNewWorkspaceToButtonList(GameObject thebutton)
    {
        buttons.Add(thebutton);
    }

    public void SwitchWorkspace(GameObject theButton)
    {
        currentWorkspace.SetActive(false);
        int position = buttons.IndexOf(theButton);
        GameObject switchToWorkspace = workspaces[position];
        switchToWorkspace.SetActive(true);

         foreach (GameObject theworkspace in workspaces)
        {
             if(theworkspace != switchToWorkspace)
             {
                theworkspace.SetActive(false);
             }   
        }
         foreach (GameObject button in buttons)
        {
             if(button != theButton)
             {
                button.GetComponent<Image>().color = Color.white;
             }
        }
        theButton.GetComponent<Image>().color = Color.green;  
        currentWorkspace = switchToWorkspace;
        GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().width = switchToWorkspace.GetComponent<workspaceInfo>().width;
        GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().height = switchToWorkspace.GetComponent<workspaceInfo>().length;
        GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().resetGrid();
    }

    public void addWorkspaceClicked()
    {
        currentWorkspace.SetActive(false);
    }

}
