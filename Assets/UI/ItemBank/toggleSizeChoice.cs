using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class toggleSizeChoice : MonoBehaviour
{
    [SerializeField]
    GameObject gameObject;
    [SerializeField]
    TMP_Text statusText;
    bool enabled;
    GameObject[] buttonsBelow;

   IEnumerator Start()
   {
    bool enabled = true;
    setState();
    yield return new WaitForSeconds(0.5f);
    setState();
   }
    // Start is called before the first frame update
   public void setState()
   {
    enabled = !enabled;
    gameObject.SetActive(!gameObject.activeInHierarchy);
    if(enabled == true)
    {
        buttonsBelow = GameObject.FindGameObjectsWithTag("ItemCategory");
        foreach (GameObject button in buttonsBelow)
        {
            if(gameObject.transform.position.y > button.transform.position.y)
            {
                Debug.Log(Mathf.Ceil((gameObject.transform.childCount/3.0f)));
                 RectTransform transform = button.GetComponent<RectTransform>();
                 transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, transform.anchoredPosition.y + Mathf.Ceil((gameObject.transform.childCount/3.0f) * 140));
            }
            
        }
        statusText.text = "+";
    }
    if(enabled == false)
    {
        buttonsBelow = GameObject.FindGameObjectsWithTag("ItemCategory");
        foreach (GameObject button in buttonsBelow)
        {
            if(gameObject.transform.position.y > button.transform.position.y)
            {
                Debug.Log(Mathf.Ceil((gameObject.transform.childCount/3.0f)));
                 RectTransform transform = button.GetComponent<RectTransform>();
                 transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, transform.anchoredPosition.y - Mathf.Ceil((gameObject.transform.childCount/3.0f) * 135));
            }
            
        }
         statusText.text = "-";
    }
   }
}
