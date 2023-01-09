using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FurnitureCreation : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    

    public void createFurniture() {

         float width = gameObject.GetComponent<AssignLabelAndSize>().width;
         Debug.Log(width);
         float length = gameObject.GetComponent<AssignLabelAndSize>().length;
         Debug.Log(length);
        var furniture = Instantiate(prefab, new Vector3(width / 2.0f - 0.5f, length / 2.0f - 0.5f, 1.0f), Quaternion.identity);
        furniture.GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f,0.5f);
        furniture.name = gameObject.GetComponent<AssignLabelAndSize>().label;
        
        

        TMP_Text text = (TMP_Text) furniture.transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = furniture.name;

        furniture.transform.localScale = new Vector3(width, length, 1.0f);

        if(length >= width) {
             text.transform.localScale = new Vector3((length/width) * (1.0f/length), 1.0f * (1.0f/length), 1.0f);
        }
        else if(width > length) {
             text.transform.localScale = new Vector3(1.0f * (1.0f/width), (width/length) * (1.0f/width), 1.0f);
        }
        
        
        
    } 
    
}
