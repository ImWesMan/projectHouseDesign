using UnityEngine;
using System.Collections;

public class FurnitureMovement : MonoBehaviour {
    public GameObject cameraController;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging;
    private bool creationMode;
    public void Start() {
        cameraController =  GameObject.FindWithTag("CameraController");
    }         

    
    void Update()
    {
        if((Input.GetMouseButtonDown(0)) || gameObject.GetComponent<FurnitureState>().isFirstCreated == true)
        { 
            
            Vector3 objectPos = gameObject.transform.position;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 objectScale = gameObject.transform.localScale;
            float halfX = objectScale.x / 2.0f;
            float halfY = objectScale.y / 2.0f;
            float halfZ = objectScale.z / 2.0f;
            
            if (mousePos.x > objectPos.x - halfX && 
                mousePos.x < objectPos.x + halfX &&
                mousePos.y > objectPos.y - halfY && 
                mousePos.y < objectPos.y + halfY &&
                mousePos.z > objectPos.z - halfZ && 
                mousePos.z < objectPos.z + halfZ)
            {
                
                cameraController.GetComponent<CameraController>().isDraggable = false;
                gameObject.GetComponent<FurnitureState>().isSelected = true;
                isDragging = true;
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

            Vector3 objectScale = gameObject.transform.localScale;
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
            transform.position = new Vector3(Mathf.Round(cursorPosition.x) + offsetX, Mathf.Round(cursorPosition.y) + offsetY, Mathf.Round(cursorPosition.z) + offsetZ);
        }
        if (Input.GetMouseButtonUp(0))
        {
            cameraController.GetComponent<CameraController>().isDraggable = true;
            gameObject.GetComponent<FurnitureState>().isSelected = false;
            isDragging = false;
            gameObject.GetComponent<FurnitureState>().isFirstCreated = false;
        }
     
    }
}