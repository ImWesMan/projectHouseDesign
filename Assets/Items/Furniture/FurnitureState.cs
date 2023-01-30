using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FurnitureState : MonoBehaviour
{
    [SerializeField]
    public bool isSelected = true;
    [SerializeField]
    public bool isMoving = true;
    [SerializeField]
    public bool isFirstCreated = true;
    [SerializeField]
    public Vector2 position;
    [SerializeField]
    public float posx;
    [SerializeField]
    public float posy;
    float gridHeight;
    float gridWidth;
    public float furnitureWidth;
    public float temp;
    public float furnitureHeight;
    GameObject gridManager;
    public GameObject furnitureUI;
    GameObject furnUI;
    public float leftEdge;
    public float rightEdge;
    public float bottomEdge;
    public float topEdge;
    public bool rotated = false;
    public bool rotatedF = false;
    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.FindWithTag("GridManager");
        gridWidth = gridManager.GetComponent<GridCreation>().width;
        Debug.Log(gridWidth);
        gridHeight = gridManager.GetComponent<GridCreation>().height;
        Debug.Log(gridHeight);
        isFirstCreated = true;
        furnitureWidth = gameObject.transform.localScale.x;
        furnitureHeight = gameObject.transform.localScale.y;
        
    }

    public void createFurnitureUI()
    {
       furnUI = Instantiate(furnitureUI, GameObject.FindWithTag("ExampleUI").transform, false);
       furnUI.transform.localScale = new Vector3(65,65,1);
       furnUI.transform.localPosition = new Vector3(500,185,0);
    }
    public void destoryFurnitureUI()
    {
        Destroy(furnUI);
    }
    
    public void deletePressed()
    {
        int[] oldEdges = new int[4] {(int) leftEdge, (int) rightEdge, (int) bottomEdge, (int) topEdge};
        gridManager.GetComponent<TileManager>().furniturePlaced(oldEdges, new int [4] {-1, 0, 0, 0});

        Destroy(gameObject);
        Destroy(furnUI);
    }
    public void rotatePressed()
    {
        int[] oldEdges = new int[4] {(int) leftEdge, (int) rightEdge, (int) bottomEdge, (int) topEdge};
        temp = furnitureWidth;
        furnitureWidth = furnitureHeight;
        furnitureHeight = temp;
        gameObject.transform.localScale = new Vector3(furnitureWidth, furnitureHeight, 1);
        TMP_Text text = (TMP_Text) gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        if(furnitureHeight >= furnitureWidth) {
             text.transform.localScale = new Vector3((furnitureHeight/furnitureWidth) * (1.0f/furnitureHeight), 1.0f * (1.0f/furnitureHeight), 1.0f);
        }
        else if(furnitureWidth > furnitureHeight) {
             text.transform.localScale = new Vector3(1.0f * (1.0f/furnitureWidth), (furnitureWidth/furnitureHeight) * (1.0f/furnitureWidth), 1.0f);
        }

        if(furnitureHeight % 2 != 0 && furnitureWidth % 2 != 0)
        {
        
        }
        else if(furnitureHeight != furnitureWidth && rotated == false)
        {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
        }
        else if(furnitureHeight != furnitureWidth && rotated == true)
        {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z);
        }

        //gameObject.GetComponent<FurnitureMovement>().calculateEdges();
        rotated = !rotated;
        rotatedF = true;
        gameObject.GetComponent<FurnitureMovement>().oldEdges = oldEdges;
        gameObject.GetComponent<FurnitureMovement>().calculateEdges();
        
    }
    // Update is called once per frame
    void Update()
    {

        if(isMoving == false && isSelected == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        if(isMoving == true || isFirstCreated == true)
        {
           posx = gameObject.transform.position.x;
            posy = gameObject.transform.position.y;
           if(posx + (gameObject.transform.localScale.x / 2.0f) > gridWidth || posy + (gameObject.transform.localScale.y / 2.0f) > gridHeight || 
               posx - (gameObject.transform.localScale.x / 2.0f) < -1 || posy - (gameObject.transform.localScale.y / 2.0f) < -1) {

                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
           else {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
           }

           

        }
        else
        {
            posx = gameObject.transform.position.x;
            posy = gameObject.transform.position.y;
            position = new Vector2(posx, posy);
            if(isSelected == false)
            {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
            } 
            if(posx + (gameObject.transform.localScale.x / 2.0f) > gridWidth || posy + (gameObject.transform.localScale.y / 2.0f) > gridHeight || 
               posx - (gameObject.transform.localScale.x / 2.0f) < -1 || posy - (gameObject.transform.localScale.y / 2.0f) < -1 && isMoving == false)
            {
                Destroy(furnUI);
                Destroy(gameObject);
            }
        }
    }
}
