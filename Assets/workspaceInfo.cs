using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class workspaceInfo : MonoBehaviour
{
    public float width;
    public float height;
    public List<GameObject> floors;
    public int FloorCount = 0;
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
         FloorCount++;
        GameObject newFloorButton = Instantiate(floorButton, GameObject.FindWithTag("WorkspaceManager").GetComponent<workspace_data>().currentFloorList.transform.GetChild(0).transform);
        newFloorButton.transform.SetSiblingIndex(0);
        newFloorButton.GetComponent<Button>().onClick.AddListener(() => SwitchFloor(newFloorButton));
        newFloorButton.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => deleteFloor(newFloorButton));
        newFloorButton.GetComponent<Image>().color = Color.green;
        newFloorButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Floor " + FloorCount;
        addToFloorButtonList(newFloorButton);
        GameObject newFloor = Instantiate(floor, GameObject.FindWithTag("WorkspaceManager").GetComponent<workspace_data>().currentWorkspace.transform);
        newFloor.GetComponent<TileManager>().createOccupied(newFloor.transform.parent.gameObject.GetComponent<workspaceInfo>().width, newFloor.transform.parent.gameObject.GetComponent<workspaceInfo>().height);
        addToFloorList(newFloor);
        currentFloor = newFloor;
        newFloor.name = "Floor " + FloorCount;
        
        
        GameObject selected = GameObject.FindWithTag("SelectedFurniture");
        if(selected != null) {
            selected.GetComponent<FurnitureState>().destoryFurnitureUI();
            selected.GetComponent<FurnitureState>().isSelected = false;
            selected.tag = "Furniture";
            selected.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
        }
        
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
        GameObject selected = GameObject.FindWithTag("SelectedFurniture");
        if(selected != null) {
            selected.GetComponent<FurnitureState>().destoryFurnitureUI();
            selected.GetComponent<FurnitureState>().isSelected = false;
            selected.tag = "Furniture";
            selected.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
        }

        if(currentFloor != null)
        {
        currentFloor.SetActive(false);
        }
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

    public void deleteFloor(GameObject floor)
    {
        if(FloorCount >= 2)
        {
            int position = floorButtons.IndexOf(floor);
            Destroy(floorButtons[position].gameObject);
            Destroy(floors[position].gameObject);
            floorButtons.RemoveAt(position);
            floors.RemoveAt(position);
            FloorCount--;
            renameFloors();
            SwitchFloor(floorButtons[0]);
        }
    }

    public void renameFloors()
    {
        int counter = 1;
         foreach (GameObject theButton in floorButtons)
        {
            theButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Floor " + counter;
            counter++;
        }
        counter = 1;
          foreach (GameObject theFloor in floors)
        {
            theFloor.name = "Floor " + counter;
            counter++;
        }
    }

}
