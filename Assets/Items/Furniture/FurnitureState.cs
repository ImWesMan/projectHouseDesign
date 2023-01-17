using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureState : MonoBehaviour
{
    [SerializeField]
    public bool isSelected;
    [SerializeField]
    public bool isFirstCreated = true;
    [SerializeField]
    public Vector2 position;
    [SerializeField]
    public float posx;
    [SerializeField]
    public float posy;
    float gridHeight;
    float gridWidth;
    public float furnitureWidth;
    public float furnitureHeight;
    GameObject gridManager;
    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.FindWithTag("GridManager");
        gridWidth = gridManager.GetComponent<GridCreation>().width;
        Debug.Log(gridWidth);
        gridHeight = gridManager.GetComponent<GridCreation>().height;
        Debug.Log(gridHeight);
        isFirstCreated = true;
        furnitureWidth = gameObject.transform.localScale.x;
        furnitureHeight = gameObject.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected == true || isFirstCreated == true)
        {
           gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            posx = gameObject.transform.position.x;
            posy = gameObject.transform.position.y;
            position = new Vector2(posx, posy);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1); 
            if(posx + furnitureWidth/4 > gridWidth - 1 || posy + (furnitureHeight/4) > gridHeight - 1 || posx - furnitureWidth/4 < 0 || posy - (furnitureHeight/4) < 0 && isSelected == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
