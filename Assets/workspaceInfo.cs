using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class workspaceInfo : MonoBehaviour
{
    public float width;
    public float length;
    public List<GameObject> floors;
    public int FloorCount = 1;
    public GameObject floorButton;
    public GameObject currentFloor;
    public GameObject floor;
    public List<GameObject> floorButtons;
    // Start is called before the first frame update
    public void addToFloorList(GameObject floor)
    {
        floors.Add(floor); 
    }

    public void addToFloorButtonList(GameObject floorButton)
    {
        floorButtons.Add(floorButton);
    }
    public void addFloor()
    {
        GameObject newFloorButton = Instantiate(floorButton, GameObject.FindWithTag("WorkspaceManager").GetComponent<workspace_data>().currentFloorList.transform.GetChild(0).transform);
        newFloorButton.GetComponent<Button>().onClick.AddListener(() => SwitchFloor(newFloorButton));
        newFloorButton.GetComponent<Image>().color = Color.green;
        newFloorButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Floor " + FloorCount;
        addToFloorButtonList(newFloorButton);
        GameObject newFloor = Instantiate(floor, GameObject.FindWithTag("WorkspaceManager").GetComponent<workspace_data>().currentWorkspace.transform);
        addToFloorList(newFloor);
        currentFloor = newFloor;
        newFloor.name = "Floor " + FloorCount;
        FloorCount++;
        foreach (GameObject thefloor in floors)
        {
             if(thefloor != newFloor)
             {
                thefloor.SetActive(false);
               
             }   
        }
        foreach (GameObject floorButton in floorButtons)
        {
             if(floorButton != newFloorButton)
             {
                
               floorButton.GetComponent<Image>().color = Color.white;
             }   
        }
    }

    public void SwitchFloor(GameObject floor)
    {
        currentFloor.SetActive(false);
        int position = floorButtons.IndexOf(floor);
        GameObject switchToFloor = floors[position];
         foreach (GameObject thefloor in floors)
        {
             if(thefloor != switchToFloor)
             {
                thefloor.SetActive(false);
             }   
        }
        foreach (GameObject floorButton in floorButtons)
        {
             if(floorButton != floor)
             {
              floorButton.GetComponent<Image>().color = Color.white;
             }   
        }
        currentFloor = switchToFloor;
        switchToFloor.SetActive(true);
        floorButtons[position].GetComponent<Image>().color = Color.green;
    }


}
