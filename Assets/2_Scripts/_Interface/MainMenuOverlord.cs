﻿using UnityEngine;
using System.Collections.Generic;

public class MainMenuOverlord : MonoBehaviour {

    public List<GameObject> Panellist;  //Used to store all panels for mass deactivation

    //TODO : Comment
    [SerializeField]
    private GameObject MainPanel;
    [SerializeField]
    private GameObject BackButton;

    /// <summary>
    /// Triggers once when script loads
    /// </summary>
    void Start()
    {
        //Disables all panels on startup
        //BackToMain();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void BackToMain()
    {
        SwitchToPanel(MainPanel);
        BackButton.SetActive(false);
    }

    /// <summary>
    /// Deactivate/Activate all panels in the list according to given bool
    /// </summary>
    /// <param name="switchto">State to switch to</param>
    void SwitchAllPanelsTo(bool switchto)
    {
        foreach (GameObject obj in Panellist)
        {
            obj.SetActive(switchto);
        }
    }

    /// <summary>
    /// Used to switch between panels in the menu
    /// Deactivate all panels and reactivate given panel
    /// </summary>
    /// <param name="Panel">Panel to activate</param>
    public void SwitchToPanel(GameObject Panel)
    {
        SwitchAllPanelsTo(false);
        Panel.SetActive(true);
        BackButton.SetActive(true);
    }

    /// <summary>
    /// Exits the game if in game window
    /// </summary>
    public void ExitTheGame()
    {
        Application.Quit();
    }
}
