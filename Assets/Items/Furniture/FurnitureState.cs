using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureState : MonoBehaviour
{
    [SerializeField]
    public bool isSelected;
    [SerializeField]
    public bool isMoving = true;
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

        if(isMoving == false && isSelected == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if(isMoving == true || isFirstCreated == true)
        {
           posx = gameObject.transform.position.x;
            posy = gameObject.transform.position.y;
           if(posx + (gameObject.transform.localScale.x / 2.0f) > gridWidth || posy + (gameObject.transform.localScale.y / 2.0f) > gridHeight || 
               posx - (gameObject.transform.localScale.x / 2.0f) < -1 || posy - (gameObject.transform.localScale.y / 2.0f) < -1) {

                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
           else {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
           }

           

        }
        else
        {
            posx = gameObject.transform.position.x;
            posy = gameObject.transform.position.y;
            position = new Vector2(posx, posy);
            if(isSelected == false)
            {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
            } 
            if(posx + (gameObject.transform.localScale.x / 2.0f) > gridWidth || posy + (gameObject.transform.localScale.y / 2.0f) > gridHeight || 
               posx - (gameObject.transform.localScale.x / 2.0f) < -1 || posy - (gameObject.transform.localScale.y / 2.0f) < -1 && isMoving == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
