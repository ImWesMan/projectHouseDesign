using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CaptureScreenshots : MonoBehaviour
{
    public float DelayBetweenScreenshots = 0.3f;
    public Button CaptureButton;
    public Transform MainCamera;

    private void Start()
    {
        CaptureButton.onClick.AddListener(TakeScreenshots);
    }

    private void TakeScreenshots()
    {
        StartCoroutine(CaptureScreenshotsCoroutine());
    }

    private IEnumerator CaptureScreenshotsCoroutine()
    {
        // get the list of floors
        workspaceInfo floorScript = FindObjectOfType<workspaceInfo>();
        List<GameObject> floors = floorScript.floors;
        
        // get the workspace name
        string workspaceName = floors[0].transform.parent.gameObject.name;

        // open the file save dialog, save chosen file path and file name.
        var path = EditorUtility.SaveFilePanel(
            "Save workspace as PNG",
            "",
            workspaceName,
            "png"
        );
        path = path.Remove(path.Length-4);

        

        // Loop through each floor in the workspace and capture a screenshot of each one
        if(path.Length != 0){
            ResetGrid(true);
            foreach (GameObject floor in floors)
            {
                yield return new WaitForEndOfFrame();
                floorScript.currentFloor.SetActive(false);
                floor.SetActive(true);

                yield return new WaitForSeconds(DelayBetweenScreenshots);

                // path to save is the given path + filename and the floors.
                string fileName = $"{path}_{floor.name}.png";
                ScreenCapture.CaptureScreenshot(fileName);

                yield return new WaitForSeconds(DelayBetweenScreenshots);
                floor.SetActive(false);
            }

            floorScript.currentFloor.SetActive(true);

            ResetGrid(false);
        }

    }

    // Sets the grid up to take a screenshot
    private void ResetGrid(bool setup){
        GridCreation gridScript = FindObjectOfType<GridCreation>();
        gridScript.resetGrid();

        DeactivateUI(setup);

        if(setup){
            // Center camera on floors
            Vector3 gridCenter = new Vector3(gridScript.width / 2.0f, gridScript.height / 2.0f, -10.0f);
            MainCamera.position = gridCenter;
        }
    }

    private void DeactivateUI(bool condition){
        GameObject mask = CaptureButton.transform.parent.Find("Mask").gameObject;
        mask.SetActive(condition);

        if(condition){
            Camera.main.cullingMask= ~(1<<LayerMask.NameToLayer("UI"));
        } else {
            Camera.main.cullingMask |=(1<<LayerMask.NameToLayer("UI"));
        }
    }
}
