using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddCustomItem : MonoBehaviour
{
    
    [SerializeField] GameObject FurnitureBank;
    [SerializeField] GameObject WallBank;
    [SerializeField] TMP_Text label;
    [SerializeField] TMP_Text width;
    [SerializeField] TMP_Text length;
    [SerializeField] TMP_InputField inputLabel;
    [SerializeField] TMP_InputField inputWidth;
    [SerializeField] TMP_InputField inputLength;
    
    public void addItem() {

        if(FurnitureBank.activeSelf){
            create(FurnitureBank);
        }else if (WallBank.activeSelf){
            create(WallBank);
        }
        
        clearFields();

    }

    public void create(GameObject bank) {
        
        Transform container = RecursiveFindChild (bank.transform, "Custom Panel");
        GameObject copy = container.Find("PlaceHolder").gameObject;

        GameObject newObject = Instantiate(copy, container);
        newObject.transform.Find("Label").gameObject.GetComponentInChildren<TMP_Text>().SetText(label.text);

        string dimensions = "(" + width.text + "x" + length.text + ")";
        newObject.transform.Find("Dimensions").gameObject.GetComponentInChildren<TMP_Text>().SetText(dimensions);
        newObject.SetActive(true);

    }

    public void clearFields() {
        inputLabel.text = null;
        inputWidth.text = null;
        inputLength.text = null;
    }

    Transform RecursiveFindChild(Transform parent, string childName) {

        foreach (Transform child in parent) {
            if(child.name == childName) {
                return child;
            }
            else {
                Transform found = RecursiveFindChild(child, childName);
                if (found != null) {
                        return found;
                }
            }
        }
        
        return null;
    }

}

