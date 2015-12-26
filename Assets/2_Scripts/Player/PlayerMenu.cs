using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/// <summary>
/// This script is used to bring up a menu when pressing Escape
/// </summary>
public class PlayerMenu : MonoBehaviour
{
    public GameObject playerMenuPanel;      //Panel to show/hide
    public GameObject player;               //Player
    public Rigidbody playerRigidbody;       //Used to disable velocity when menu is showing
    public Component[] playerInputScripts;  //Scripts containing inputs to disable when showing the menu

    private NetworkManager networkManager;  //Used to toggle between showing/hidden state
    private bool isShowing;                 //Used to toggle between showing/hidden state

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        networkManager = GameObject.Find("NetworkGM").GetComponent<NetworkManager>();
    }

    /// <summary>
    /// Triggered when script is enabled
    /// </summary>
    void Start()
    {
        isShowing = true;
        ShowPlayerMenu();
    }

    /// <summary>
    /// Triggered once every frame
    /// </summary>
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
    /// Makes player join team
    /// </summary>
    /// <param name="teamtojoin">ID of team to join (0 = SPE; 1 = BLU; 2 = RED)</param>
    public void JoinTeam(int teamtojoin)
    {
        Debug.Log("Tried to join team: " + teamtojoin);
        if (!player.activeSelf)
        {
            player.GetComponent<PlayerStats>().playerTeam = (PlayerStats.Team)teamtojoin;
            player.transform.position = Vector3.up;
            player.SetActive(true);
            HidePlayerMenu();
        }
        else
        {
            Debug.Log("Tried to spawn already active player");
        }
    }

    /// <summary>
    /// Disconnects to main menu
    /// </summary>
    public void DisconnectToMainMenu()
    {
        networkManager.StopHost();
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

        playerRigidbody.velocity = Vector3.zero;
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
