using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to bring up a menu when pressing Escape
/// </summary>
public class PlayerMenu : NetworkBehaviour
{
    public GameObject playerMenuPanel;      //Panel to show/hide
    public GameObject playerRigidBody;

    private SceneOverlord sceneOverlord;
    private NetworkOverlord networkOverlord;
    private bool isShowing;                 //Used to toggle between showing/hidden state

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        sceneOverlord = GameObject.Find("SceneGM").GetComponent<SceneOverlord>();
        networkOverlord = GameObject.Find("NetworkGM").GetComponent<NetworkOverlord>();
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
    /// Disables all input scripts, unlock and show cursor and show player menu
    /// </summary>
    void ShowPlayerMenu()
    {
        sceneOverlord.isReceivingInputs = false;
        playerMenuPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Enables all inputs scripts, lock and hides cursor and hides player menu
    /// </summary>
    void HidePlayerMenu()
    {
        sceneOverlord.isReceivingInputs = true;
        playerMenuPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Disconnects to main menu
    /// </summary>
    public void DisconnectToMainMenu()
    {
        networkOverlord.StopHost();
    }

    public void JoinTeam(int teamID)
    {
        switch (teamID)
        {
            case 1:
                networkOverlord.playerTeam = PlayerStats.Team.BLU;
                break;
            case 2:
                networkOverlord.playerTeam = PlayerStats.Team.RED;
                break;
            default:
                networkOverlord.playerTeam = PlayerStats.Team.SPE;
                break;
        }

        CmdSpawn();
        HidePlayerMenu();
    }

    [Command]
    public void CmdSpawn()
    {
        if (ClientScene.localPlayers.Count != 0)
            ClientScene.RemovePlayer(0);
        ClientScene.AddPlayer(0);
    }
}
