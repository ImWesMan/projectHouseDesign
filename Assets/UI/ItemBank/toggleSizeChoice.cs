using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class toggleSizeChoice : MonoBehaviour
{
    [SerializeField]
    GameObject gameObject;
    // Start is called before the first frame update
   public void setState()
   {
    gameObject.SetActive(!gameObject.activeInHierarchy);
   }
}
