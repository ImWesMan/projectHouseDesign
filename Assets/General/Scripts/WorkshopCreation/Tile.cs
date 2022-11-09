using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] public Color normalColor;
   [SerializeField] public Color offsetColor;
   [SerializeField] public SpriteRenderer renderer;

   public void init(bool isOffset)
   {
    renderer.color = isOffset ? offsetColor : normalColor;
   }
}
