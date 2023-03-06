using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class AddCustomItem : MonoBehaviour
{
    [SerializeField] Transform CustomFurniturePanel;
    [SerializeField] TMP_InputField InputLabel;
    [SerializeField] TMP_InputField WidthInputField;
    [SerializeField] TMP_InputField LengthInputField;
    public GameObject placeHolder;
    [SerializeField] Camera MainCamera;

    private int _originalCullingMask;

    public void Start()
    {
        _originalCullingMask = MainCamera.cullingMask;
        WidthInputField.onValidateInput += delegate(string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
        LengthInputField.onValidateInput += delegate(string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
    }

    private char MyValidate(char charToValidate)
    {
        if (!Char.IsDigit(charToValidate))
        {
            charToValidate = '\0';
        }
        return charToValidate;
    }

    public void TogglePopUp(bool isDisplayed){

        
        if(isDisplayed){
            MainCamera.cullingMask = (1 << LayerMask.NameToLayer("PopUp"));
        }else{
            MainCamera.cullingMask = _originalCullingMask;
        }
    }
    public void AddItem() {
        Create();
        ClearFields();
    }

    private void Create() {
        String width = WidthInputField.text;
        String length = LengthInputField.text;

        GameObject newObject = Instantiate(placeHolder, CustomFurniturePanel);

        newObject.transform.Find("Label").gameObject.GetComponentInChildren<TMP_Text>().SetText(InputLabel.text);
        string dimensions = $"({width}x{length})";
        newObject.transform.Find("Dimensions").gameObject.GetComponentInChildren<TMP_Text>().SetText(dimensions);
        newObject.SetActive(true);

        TogglePopUp(false);
    }

    public void Create(String name, String dimensions)
    {
        GameObject newObject = Instantiate(placeHolder, CustomFurniturePanel);
        newObject.transform.Find("Label").gameObject.GetComponentInChildren<TMP_Text>().SetText(name);
        newObject.transform.Find("Dimensions").gameObject.GetComponentInChildren<TMP_Text>().SetText(dimensions);
        newObject.SetActive(true); 
    }

    private void ClearFields() {
        InputLabel.text = null;
        WidthInputField.text = null;
        LengthInputField.text = null;
    }


    public void deleteCustomItem(GameObject triggeringButton)
    {
        Destroy(triggeringButton);
    }

}

