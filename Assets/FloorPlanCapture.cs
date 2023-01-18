using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorPlanCapture : MonoBehaviour
{
    public Canvas canvas;
    public Transform cam;
    private float width;
    private float height;


    public void ExportImage()
    {
        GridCreation gridScript = FindObjectOfType<GridCreation>();
        width = gridScript.width;
        height = gridScript.height;
        gridScript.resetGrid();

        RecenterCamera();

        Camera.main.cullingMask= ~(1<<LayerMask.NameToLayer("UI"));
        // canvas.gameObject.GetComponent<Canvas>().enabled = false;;
        ScreenCapture.CaptureScreenshot("Floorplan.png");
        Invoke("Activate", 0.02f);

    }

    private void Activate(){
        Camera.main.cullingMask |=(1<<LayerMask.NameToLayer("UI"));
        //  canvas.gameObject.GetComponent<Canvas>().enabled = true;
    }

    public void RecenterCamera()
    {
        Vector3 gridCenter = new Vector3(width / 2.0f, height / 2.0f, -10.0f);
        cam.position = gridCenter;
    }
}
