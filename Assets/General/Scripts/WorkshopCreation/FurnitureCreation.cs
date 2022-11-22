using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FurnitureCreation : MonoBehaviour
{
    float width, height;
    public Slider heightSlide;
    public Slider widthSlide;
    
    [SerializeField] GameObject tilePrefab;

    
    public void GenerateFurniture() {

        float length = Mathf.Floor(heightSlide.value);
        float width = Mathf.Floor(widthSlide.value);
        Debug.Log(length);
        Debug.Log(width);

        Furniture furniture = new Furniture(width, length, tilePrefab);
        var spawnedTile = Instantiate(tilePrefab, new Vector3(this.width / 2.0f - 0.5f, this.length / 2.0f - 0.5f), Quaternion.identity);
        spawnedTile.name = "Furniture";

        spawnedTile.transform.localScale = new Vector3(width, length, 1.0f);
    }
}

