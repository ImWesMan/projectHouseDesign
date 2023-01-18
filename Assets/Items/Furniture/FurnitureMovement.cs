using UnityEngine;
using System.Collections;

public class FurnitureMovement : MonoBehaviour {
    public GameObject cameraController;
    private Vector3 screenPoint;
    private Vector3 offset;
    public bool isDragging;
    private bool creationMode;
    public bool movedItem = false;
    Vector3 mouseDownPos;
    public void Start() {
        cameraController =  GameObject.FindWithTag("CameraController");
    }         

    
    void Update()
    {
        Vector3 objectScale = gameObject.transform.localScale;
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
                
                gameObject.GetComponent<FurnitureState>().isSelected = !gameObject.GetComponent<FurnitureState>().isSelected;
                 
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

                cameraController.GetComponent<CameraController>().isDraggable = false;
                //need this line for some reason
                gameObject.GetComponent<FurnitureState>().isFirstCreated = false;
                isDragging = true;
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }
        }   
                

        if ((Input.GetMouseButton(0) && isDragging))
        { 

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
            Debug.Log("new: " + newPosition);
            Debug.Log("old: " + gameObject.transform.position);
            if(!(gameObject.transform.position.x == newPosition.x && gameObject.transform.position.y == newPosition.y)) {
                gameObject.transform.position = newPosition;
                gameObject.GetComponent<FurnitureState>().isSelected = false;
            }
            
            movedItem = true;
        }   
        
        if (Input.GetMouseButtonUp(0))
        {
            cameraController.GetComponent<CameraController>().isDraggable = true;
            gameObject.GetComponent<FurnitureState>().isMoving = false;
            isDragging = false;
            gameObject.GetComponent<FurnitureState>().isFirstCreated = false;
            movedItem = false;
        }
     
    }
}