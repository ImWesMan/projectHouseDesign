using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mouseWorldPosStart;
    public Camera camera;
    private float zoomScale = 10.0f;
    private float zoomMin = 5.0f;
    private float zoomMax = 30.0f;
    public GameObject topRight;
    public GameObject topLeft;
    public GameObject bottomRight;
    public GameObject bottomLeft;
     Vector3 topRightPos;
     Vector3 topLeftPos;
      Vector3 bottomRightPos;
      Vector3 bottomLeftPos;
    // Update is called once per frame
     public void getPos() {
        topRightPos = camera.WorldToScreenPoint(topRight.transform.position);
        topLeftPos = camera.WorldToScreenPoint(topLeft.transform.position);
        bottomRightPos = camera.WorldToScreenPoint(bottomRight.transform.position);
        bottomLeftPos = camera.WorldToScreenPoint(bottomLeft.transform.position);
        Debug.Log(topRightPos);
        Debug.Log(topLeftPos);
        Debug.Log(bottomRightPos);
        Debug.Log(bottomLeftPos);
    }


    void Update()
   {
    if(Input.mousePosition.x > bottomLeftPos.x && Input.mousePosition.y > bottomLeftPos.y && Input.mousePosition.x < bottomRightPos.x && Input.mousePosition.y < topRightPos.y )
    {
    Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
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

