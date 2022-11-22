using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureCreation : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Slider widthSlider;
    [SerializeField] Slider lengthSlider;

    public void createFurniture() {
        
        float width = Mathf.Floor(widthSlider.value);
        float length = Mathf.Floor(lengthSlider.value);

        var furniture = Instantiate(prefab, new Vector3(width / 2.0f - 0.5f, length / 2.0f - 0.5f, 1.0f), Quaternion.identity);
        furniture.name = "Furniture";

        furniture.transform.localScale = new Vector3(width, length, 1.0f);
    }
}
