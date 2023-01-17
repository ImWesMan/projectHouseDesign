using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureState : MonoBehaviour
{
    [SerializeField]
    public bool isSelected = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected == true)
        {
           gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1); 
        }
    }
}
