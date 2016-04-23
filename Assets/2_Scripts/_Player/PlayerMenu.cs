using UnityEngine;

/// <summary>
/// This script is used to bring up a menu when pressing Escape
/// </summary>
public class PlayerMenu : MonoBehaviour
{
    [Header("References(Player)")]
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerCall playerCall;

    [Header("References(Interface)")]
    [SerializeField]
    private GameObject playerMenuPanel;          //Panel to show/hide
    [SerializeField]
    private GameObject playerCrosshairPanel;

    //Local
    private NetworkOverlord networkOverlord;    //Used to cache current NetworkOverlord
    private bool isShowing;                     //Used to toggle between showing/hidden state

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        networkOverlord = GameObject.Find("NetworkGM").GetComponent<NetworkOverlord>();
    }

    /// <summary>
    /// Triggered when script is enabled
    /// </summary>
    void Start()
    {
        if (playerStats.playerTeam != Team.SPE)
        {
            playerCrosshairPanel.SetActive(true);
        }
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
        playerStats.isReceivingInputs = false;
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
        if (!playerStats.isFrozen)  //We don't want to allow input if frozen 
        {
            playerStats.isReceivingInputs = true;
        }
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
        playerCall.Call_ChangePlayerTeam(Team.BLU);
        HidePlayerMenu();
    }

    public void JoinRED()
    {
        playerCall.Call_ChangePlayerTeam(Team.RED);
        HidePlayerMenu();
    }

    public void JoinSPE()
    {
        playerCall.Call_ChangePlayerTeam(Team.SPE);
        HidePlayerMenu();
    }

}
