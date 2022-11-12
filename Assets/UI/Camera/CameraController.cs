using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float camSpeed = 20f;
    public float panBorder = 10f;
    public Vector2 mapLimits;
    public float scrollSpeed = 20f;
    public float minY = 5f;
    public float maxY = 12.51f;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        
        if(Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorder)
        {
            pos.z += camSpeed * Time.deltaTime;
        }
         if(Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorder)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }
         if(Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorder)
        {
            pos.x += camSpeed * Time.deltaTime;
        }
         if(Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorder)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -mapLimits.x, mapLimits.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -mapLimits.y, mapLimits.y);
        transform.position = pos;    
    }
}

