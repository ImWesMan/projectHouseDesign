using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridCreation : MonoBehaviour
{
   float width, height;
   public Slider heightSlide;
   public Slider widthSlide;
    [SerializeField] Transform cam;
    [SerializeField] Camera camera;
    [SerializeField] Tile tilePrefab;
   public void GenerateGrid()
   {
    height = heightSlide.value;
    width = widthSlide.value;
    Debug.Log(height);
    Debug.Log(width);
    for(int x = 0; x < width; x++)
    {
        for(int y = 0; y<height; y++)
        {
            var spawnedTile = Instantiate(tilePrefab, new Vector3(x,y), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y}";

            var isOffset = (x + y) % 2 == 1;
             spawnedTile.init(isOffset);
        }
    }
    float maximum = (float)Mathf.Max(width, height);
    camera.orthographicSize = maximum / 2.0f + maximum/100;
    float cameraHeight = 2.0f * camera.orthographicSize;
    float cameraWidth = cameraHeight * camera.aspect;

    cam.transform.position = new Vector3((float)width/2 + cameraWidth / 6.0f , (float)height / 2 - 0.5f, -10);

   }
}
