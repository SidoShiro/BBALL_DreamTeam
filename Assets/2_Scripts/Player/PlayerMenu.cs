using UnityEngine;

/// <summary>
/// This script is used to bring up a menu when pressing Escape
/// </summary>
public class PlayerMenu : MonoBehaviour
{
    public GameObject playerMenuPanel;          //Panel to show/hide
    public GameObject playerRigidBody;          //Panel to show/hide
    public PlayerCommand playerCommand;

    private SceneOverlord sceneOverlord;
    private NetworkOverlord networkOverlord;
    private bool isShowing;                     //Used to toggle between showing/hidden state

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
        isShowing = false;
        HidePlayerMenu();
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

        isShowing = true;
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

        isShowing = false;
    }

    /// <summary>
    /// Disconnects to main menu
    /// </summary>
    public void DisconnectToMainMenu()
    {
        networkOverlord.StopHost();
    }

    public void JoinBLU()
    {
        playerCommand.CallRespawnPlayer(PlayerStats.Team.BLU);
        HidePlayerMenu();
    }

    public void JoinRED()
    {
        playerCommand.CallRespawnPlayer(PlayerStats.Team.RED);
        HidePlayerMenu();
    }

}
