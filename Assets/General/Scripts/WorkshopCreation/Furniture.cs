using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Furniture : MonoBehaviour
{
    private float width;
    private float length;

    public Furniture(float width, float length) {
        
        this.width = width;
        this.length = length;
    }
}
