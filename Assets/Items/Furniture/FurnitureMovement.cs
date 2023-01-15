using UnityEngine;
using System.Collections;

public class FurnitureMovement : MonoBehaviour {
    public GameObject cameraController;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging;

    public void Start() {
        cameraController =  GameObject.FindWithTag("CameraController");
    }         

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 objectPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            Vector3 mousePos = Input.mousePosition;

            if (objectPos.x < mousePos.x + 100f && objectPos.x > mousePos.x - 100f &&
                objectPos.y < mousePos.y + 100f && objectPos.y > mousePos.y - 100f &&
                objectPos.z < mousePos.z + 100f && objectPos.z > mousePos.z - 100f)
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
            transform.position = cursorPosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            cameraController.GetComponent<CameraController>().isDraggable = true;
            gameObject.GetComponent<FurnitureState>().isSelected = false;
            isDragging = false;
        }
    }
}