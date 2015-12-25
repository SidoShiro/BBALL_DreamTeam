using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/// <summary>
/// This script is used to bring up a menu when pressing Escape
/// </summary>
public class PlayerMenu : MonoBehaviour
{
    public GameObject playerMenuPanel;      //Panel to show
    public Rigidbody playerRigibody;        //Used to disable velocity when menu is showing
    public Component[] playerInputScripts;  //Scripts containing inputs to disable when showing the menu

    public Button disconnectButton;

    private bool isShowing; //Used to toggle between showing/hidden state

    private NetworkManager networkManager; //Used to toggle between showing/hidden state

    void Awake()
    {
        networkManager = GameObject.Find("NetworkGM").GetComponent<NetworkManager>();
    }
    /// <summary>
    /// Triggered when script is enabled
    /// </summary>
    void Start()
    {
        isShowing = false;
        HidePlayerMenu();
        disconnectButton.onClick.AddListener(delegate { networkManager.StopHost(); });
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

        playerRigibody.velocity = Vector3.zero;
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
        foreach (MonoBehaviour monoscript in playerInputScripts)
        {
            monoscript.enabled = newstate;
        }
    }
}
