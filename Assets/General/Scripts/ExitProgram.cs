using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitProgram : MonoBehaviour
{
    public GameObject exitConfirmation;
    public GameObject exampleUI;
    public GameObject WorkspaceManager;
    public void showConfirmation()
    {
        exampleUI.SetActive(false);
        exitConfirmation.SetActive(true);
        WorkspaceManager.SetActive(false);
    }
    public void hideConfirmation()
    {
        exampleUI.SetActive(true);
        exitConfirmation.SetActive(false);
        WorkspaceManager.SetActive(true);
    }
    public void exitProgram()
    {
        Application.Quit();
    }
}
