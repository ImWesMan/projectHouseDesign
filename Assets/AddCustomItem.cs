using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCustomItem : MonoBehaviour
{
    [SerializeField] GameObject FurnitureBank;
    [SerializeField] GameObject WallBank;
    

    public void addItem() {
        if(FurnitureBank.activeSelf){
            Debug.Log("Hello: " + FurnitureBank.name);
            Button example = FurnitureBank.GetComponentInChildren<Button>();
            Debug.Log("Hello: " + example.name);
        }else if (WallBank.activeSelf){
            Debug.Log("Hello: " + WallBank.name);
        }
        
        
    }
}
