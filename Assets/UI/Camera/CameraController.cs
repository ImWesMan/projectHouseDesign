using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mouseWorldPosStart;
    public Camera camera;
    private float zoomScale = 10.0f;
    private float zoomMin = 5.0f;
    private float zoomMax = 30.0f;

    // Update is called once per frame
    
    void Update()
   {
    Zoom(Input.GetAxis("Mouse ScrollWheel"));
   }

    private void Zoom(float zoomDiff)
    {
        if(zoomDiff != 0)
        {
            mouseWorldPosStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomDiff * zoomScale, zoomMin, zoomMax);
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            camera.transform.position += mouseWorldPosDiff;
        }
    }
}

