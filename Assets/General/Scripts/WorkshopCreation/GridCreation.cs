using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridCreation : MonoBehaviour
{
   float width, height;
   public Slider heightSlide;
   public Slider widthSlide;

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
   }
}
