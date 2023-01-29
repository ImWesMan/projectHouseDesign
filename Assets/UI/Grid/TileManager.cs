using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] furnitureObjects;
    public GameObject[] furnitureSelected;
    public void fillTileArray()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    // Update is called once per frame
    public void furniturePlaced()
    {
        furnitureObjects = GameObject.FindGameObjectsWithTag("Furniture");
        furnitureSelected = GameObject.FindGameObjectsWithTag("SelectedFurniture");
        furnitureObjects = furnitureObjects.Concat(furnitureSelected).ToArray();
        Debug.Log("Furniture Placed");
        foreach (GameObject tile in tiles)
        {
        tile.GetComponent<Tile>().isOccupied = false;
        foreach(GameObject furniture in furnitureObjects)
        {
        //NEEDS TO BE MODIFIED TO WORK FOR ODD FURNITURE PIECES.
        if((tile.transform.position.x >= furniture.GetComponent<FurnitureState>().leftEdge && tile.transform.position.x < furniture.GetComponent<FurnitureState>().rightEdge) &&
        (tile.transform.position.y >= furniture.GetComponent<FurnitureState>().bottomEdge && tile.transform.position.y < furniture.GetComponent<FurnitureState>().topEdge))
        {
            Debug.Log("Tile Triggered as occupied");
            tile.GetComponent<Tile>().isOccupied = true;
        }
        }  
        }
        
    }
}
