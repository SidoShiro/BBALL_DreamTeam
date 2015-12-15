using UnityEngine;
using System.Collections.Generic;

public class MainMenuOverlord : MonoBehaviour {

    public List<GameObject> Panellist;  //Used to store all panels for deactivation

    /// <summary>
    /// Disable all panels on start
    /// </summary>
    void Start()
    {
        SwitchAllPanelsTo(false);
    }

    /// <summary>
    /// Deactive all panels contained in Panellist
    /// </summary>
    void SwitchAllPanelsTo(bool switchto)
    {
        foreach (GameObject obj in Panellist)
        {
            obj.SetActive(switchto);
        }
    }

    /// <summary>
    /// Deactivate all panels and activate given panel
    /// </summary>
    /// <param name="Panel">Panel to activate</param>
    public void SwitchToPanel(GameObject Panel)
    {
        SwitchAllPanelsTo(false);
        Panel.SetActive(true);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

}
