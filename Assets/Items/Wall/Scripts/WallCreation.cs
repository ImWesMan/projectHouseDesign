using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallCreation : MonoBehaviour {

    [SerializeField] GameObject WorkspaceManager;
    [SerializeField] GameObject PointPrefab;
    [SerializeField] GameObject LinePrefab;
    public bool creating;
    private bool intitialize = true;

    private bool first = true;
    private bool firstCreated = false;
    private bool firstSet = false;
    private GameObject firstPoint;

    private bool second = false;
    private bool secondCreated = false;
    private bool secondSet = false;
    private GameObject secondPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject workspace = WorkspaceManager.GetComponent<workspace_data>().currentWorkspace;
        if(creating) {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(first) {
                if(!firstCreated) {
                    if(mousePos.x > -0.5f && mousePos.x < workspace.GetComponent<workspaceInfo>().width - 0.5f && mousePos.y > -0.5f && mousePos.y < workspace.GetComponent<workspaceInfo>().height - 0.5f) {
                        firstPoint = Instantiate(PointPrefab, new Vector3(Mathf.Ceil(mousePos.x) - 0.5f, Mathf.Ceil(mousePos.y) - 0.5f, 1.0f), Quaternion.identity);
                        firstCreated = true;
                    }
                }
                else {
                    Vector3 screenPoint = Camera.main.WorldToScreenPoint(firstPoint.transform.position);
                    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
                    firstPoint.transform.position = new Vector3(Mathf.Ceil(cursorPosition.x) - 0.5f, Mathf.Ceil(cursorPosition.y) - 0.5f, cursorPosition.z);
                    if(!(mousePos.x > -0.5f && mousePos.x < workspace.GetComponent<workspaceInfo>().width - 0.5f && mousePos.y > -0.5f && mousePos.y < workspace.GetComponent<workspaceInfo>().height - 0.5f)) {
                        Destroy(firstPoint);
                        firstCreated = false;
                    }
                    if(Input.GetMouseButtonDown(0)) {
                        firstSet = true;
                        first = false;
                        second = true;
                    }
                }
                
            }
            else if(second) {
                if(!secondCreated) {
                   if(mousePos.x > -0.5f && mousePos.x < workspace.GetComponent<workspaceInfo>().width - 0.5f && mousePos.y > -0.5f && mousePos.y < workspace.GetComponent<workspaceInfo>().height - 0.5f) {
                        secondPoint = Instantiate(PointPrefab, new Vector3(Mathf.Ceil(mousePos.x) - 0.5f, Mathf.Ceil(mousePos.y) - 0.5f, 1.0f), Quaternion.identity);
                        secondCreated = true;
                    } 
                }
                else {
                    Vector3 screenPoint = Camera.main.WorldToScreenPoint(firstPoint.transform.position);
                    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
                    secondPoint.transform.position = new Vector3(Mathf.Ceil(cursorPosition.x) - 0.5f, Mathf.Ceil(cursorPosition.y) - 0.5f, cursorPosition.z);
                    if(!(mousePos.x > -0.5f && mousePos.x < workspace.GetComponent<workspaceInfo>().width - 0.5f && mousePos.y > -0.5f && mousePos.y < workspace.GetComponent<workspaceInfo>().height - 0.5f)) {
                        Destroy(secondPoint);
                        secondCreated = false;
                    }
                    if(Input.GetMouseButtonDown(0)) {
                        secondSet = true;
                        second = false;
                    }
                }
            }
             
            if(firstSet && secondSet) {
                GameObject line = Instantiate(LinePrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                line.GetComponent<LineRenderer>().startWidth = 0.15f;
                line.GetComponent<LineRenderer>().endWidth = 0.15f;
                line.GetComponent<LineRenderer>().positionCount = 2;
                line.GetComponent<LineRenderer>().useWorldSpace = true;    
                
                //For drawing line in the world space, provide the x,y,z values
                line.GetComponent<LineRenderer>().SetPosition(0, firstPoint.transform.position); //x,y and z position of the starting point of the line
                line.GetComponent<LineRenderer>().SetPosition(1, secondPoint.transform.position); //x,y and z position of the end point of the line

                Destroy(firstPoint);
                Destroy(secondPoint);
                firstPoint = null;
                secondPoint = null;
                
                creating = false;
                first = true;
                firstCreated = false;
                secondCreated = false;
                firstSet = false;
                secondSet = false;
            }
        }
    }
}
