using UnityEngine;
using System.Collections;

public class FurnitureMovement : MonoBehaviour {
    public GameObject cameraController;
    private Vector3 screenPoint;
    private Vector3 offset;
    public bool isDragging;
    private bool creationMode;
    public bool movedItem = false;
    public GameObject GridManager;
    private int[] oldEdges =  new int[4];
    private int[] newEdges = new int[4];
    Vector3 oldPos;
    Vector3 objectScale;
    public void Start() {
        cameraController =  GameObject.FindWithTag("CameraController");
        GridManager = GameObject.FindWithTag("GridManager");
    }         

    void Select() {

        gameObject.GetComponent<FurnitureState>().isSelected = !gameObject.GetComponent<FurnitureState>().isSelected;
        if(gameObject.GetComponent<FurnitureState>().isSelected == true) {
            GameObject selected = GameObject.FindWithTag("SelectedFurniture");
            gameObject.GetComponent<FurnitureState>().createFurnitureUI();
            if(selected != null) {
                selected.GetComponent<FurnitureState>().destoryFurnitureUI();
                selected.GetComponent<FurnitureState>().isSelected = false;
                selected.tag = "Furniture";
            }
            gameObject.tag = "SelectedFurniture";
        }
        else {
            gameObject.GetComponent<FurnitureState>().destoryFurnitureUI();
            gameObject.tag = "Furniture";
        }
    }
    
    public void calculateEdges()
    {
         gameObject.GetComponent<FurnitureState>().leftEdge = gameObject.transform.position.x - ((objectScale.x - 1.0f) * 0.5f);
         gameObject.GetComponent<FurnitureState>().rightEdge = gameObject.GetComponent<FurnitureState>().leftEdge + objectScale.x;
         gameObject.GetComponent<FurnitureState>().bottomEdge = gameObject.transform.position.y - ((objectScale.y - 1.0f) * 0.5f);
         gameObject.GetComponent<FurnitureState>().topEdge = gameObject.GetComponent<FurnitureState>().bottomEdge + objectScale.y;
    }
    void Update()
    {
        objectScale = gameObject.transform.localScale;
        float halfX = objectScale.x / 2.0f;
        float halfY = objectScale.y / 2.0f;
        float halfZ = objectScale.z / 2.0f;
        Vector3 objectPos = gameObject.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        
        
        //selecting
        if((Input.GetMouseButtonUp(0)))
        { 
            
            if (mousePos.x > objectPos.x - halfX && 
                mousePos.x < objectPos.x + halfX &&
                mousePos.y > objectPos.y - halfY && 
                mousePos.y < objectPos.y + halfY &&
                mousePos.z > objectPos.z - halfZ && 
                mousePos.z < objectPos.z + halfZ)
            {
                
                if(cameraController.GetComponent<CameraController>().moved == false) {
                    Debug.Log("entered 1");
                    Select();
                }
                
                //gameObject.GetComponent<FurnitureState>().isSelected = !gameObject.GetComponent<FurnitureState>().isSelected;
                 
            }
        }

        if((Input.GetMouseButtonDown(0) && gameObject.GetComponent<FurnitureState>().isSelected) 
        
        || gameObject.GetComponent<FurnitureState>().isFirstCreated == true) {

            if(mousePos.x > objectPos.x - halfX && 
            mousePos.x < objectPos.x + halfX &&
            mousePos.y > objectPos.y - halfY && 
            mousePos.y < objectPos.y + halfY &&
            mousePos.z > objectPos.z - halfZ && 
            mousePos.z < objectPos.z + halfZ) {

                if(gameObject.GetComponent<FurnitureState>().isFirstCreated == true) {
                    Debug.Log("entered 2");
                    Select();
                }
                
                cameraController.GetComponent<CameraController>().isDraggable = false;
                //need this line for some reason
                gameObject.GetComponent<FurnitureState>().isFirstCreated = false;
                isDragging = true;
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

                oldEdges[0] = (int) gameObject.GetComponent<FurnitureState>().leftEdge;
                oldEdges[1] = (int) gameObject.GetComponent<FurnitureState>().rightEdge;
                oldEdges[2] = (int) gameObject.GetComponent<FurnitureState>().bottomEdge;
                oldEdges[3] = (int) gameObject.GetComponent<FurnitureState>().topEdge;

            }
        }   
                

        if ((Input.GetMouseButton(0) && isDragging))
        { 
            gameObject.GetComponent<FurnitureState>().destoryFurnitureUI();
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            gameObject.GetComponent<FurnitureState>().isMoving = true;
            float offsetX = 0.0f;
            float offsetY = 0.0f;
            float offsetZ = 0.0f;
            if(objectScale.x % 2 == 0) {
                offsetX = 0.5f;
            }
            if(objectScale.y % 2 == 0) {
                offsetY = 0.5f;
            }
            if(objectScale.z % 2 == 0) {
                offsetZ = 0.5f;
            }
            
            Vector3 newPosition = new Vector3(Mathf.Floor(cursorPosition.x) + offsetX, Mathf.Floor(cursorPosition.y) + offsetY, Mathf.Floor(cursorPosition.z) + offsetZ);
            //Debug.Log("new: " + newPosition);
            //Debug.Log("old: " + gameObject.transform.position);
            if(!(gameObject.transform.position.x == newPosition.x && gameObject.transform.position.y == newPosition.y)) {
                
                movedItem = true;
                gameObject.transform.position = newPosition; 
                calculateEdges();
                newEdges[0] = (int) gameObject.GetComponent<FurnitureState>().leftEdge;
                newEdges[1] = (int) gameObject.GetComponent<FurnitureState>().rightEdge;
                newEdges[2] = (int) gameObject.GetComponent<FurnitureState>().bottomEdge;
                newEdges[3] = (int) gameObject.GetComponent<FurnitureState>().topEdge;
            }
        }   
        
        if (Input.GetMouseButtonUp(0))
        {
            if(mousePos.x > objectPos.x - halfX && 
                mousePos.x < objectPos.x + halfX &&
                mousePos.y > objectPos.y - halfY && 
                mousePos.y < objectPos.y + halfY &&
                mousePos.z > objectPos.z - halfZ && 
                mousePos.z < objectPos.z + halfZ) {

                if(movedItem == true) {
                    Debug.Log("entered 3");
                    Select();
                }
            }

            if(cameraController.GetComponent<CameraController>().moved == false && movedItem == true) {
                GridManager.GetComponent<TileManager>().furniturePlaced(oldEdges, newEdges);
            }
            cameraController.GetComponent<CameraController>().isDraggable = true;
            //cameraController.GetComponent<CameraController>().moved = false;
            gameObject.GetComponent<FurnitureState>().isMoving = false;
            isDragging = false;
            gameObject.GetComponent<FurnitureState>().isFirstCreated = false;
            movedItem = false;
           
        }
    }

    void LateUpdate() {

        if(Input.GetMouseButtonUp(0)) {
            cameraController.GetComponent<CameraController>().moved = false;
        }
    }
}