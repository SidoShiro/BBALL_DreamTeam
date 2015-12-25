using UnityEngine;

/// <summary>
/// This script is used to bring up a menu when pressing Escape
/// </summary>
public class PlayerMenu : MonoBehaviour
{
    public Component[] playerInputScripts;  //Scripts containing inputs to disable when showing the menu
    
    private GameObject playerMenuPanel; //Panel to show
    private bool isShowing;             //Used to toggles between showing/hidden state

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        playerMenuPanel = GameObject.Find("PlayerMenuPanel");
        isShowing = false;
        HidePlayerMenu();
    }

    /// <summary>
    /// Triggered when script is enabled
    /// </summary>
    void Start()
    {
        isShowing = false;
        HidePlayerMenu();
    }

    void Update()
    {
        //Toggles menu state
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            if (isShowing)
            {
                ShowPlayerMenu();
            }
            else
            {
                HidePlayerMenu();
            }
            
        }
    }

    /// <summary>
    /// Disables all input scripts, unlock and show cursor and show player menu
    /// </summary>
    void ShowPlayerMenu()
    {
        ChangeScriptsState(false);
        playerMenuPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    /// <summary>
    /// Enables all inputs scripts, lock and hides cursor and hides player menu
    /// </summary>
    void HidePlayerMenu()
    {
        ChangeScriptsState(true);
        playerMenuPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Change script enabled state to given boolean
    /// </summary>
    /// <param name="newstate">New scripts enabled state</param>
    void ChangeScriptsState(bool newstate)
    {
        foreach(MonoBehaviour monoscript in playerInputScripts)
        {
            monoscript.enabled = newstate;
        }
    }
}
