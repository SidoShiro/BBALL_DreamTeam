using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to enable client specific components that are disabled by default server wise:
/// We only want to control our player not others
/// We only want one camera active: our player's camera
/// etc...
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class PlayerEnabler : NetworkBehaviour
{
    [Header("References(Player)")]
    [SerializeField]
    private NetworkIdentity playerIdentity; //Reference to acces player team
    [SerializeField]
    private PlayerStats playerStats;        //Reference to acces player team
    [SerializeField]
    private PlayerCall playerCall;          //Reference to access player calls

    [Header("References(Interface)")]
    [SerializeField]
    private PlayerHUD playerHUD;

    [Header("Modify on client")]
    [SerializeField]
    public Component[] scripts;        //List of scripts (Mainly inputs scripts) to enable client side
    [SerializeField]
    private GameObject playerModel;    //Used to place model on NORENDER layer on client
    [SerializeField]
    private Rigidbody playerRigidBody; //Used to enable isKinematic depending on team
    [SerializeField]
    private Camera playerCamera;       //Used to enable camera and define render layer according to team
    [SerializeField]
    private AudioListener playerAudio; //Used to enable camera and define render layer according to team
    [SerializeField]
    private GameObject playerUI;       //Used to Enable UI if client

    [Header("Team specific")]
    [SerializeField]
    private PlayerShoot playerShoot;        //This script will be disabled if on SPE team
    [SerializeField]
    private Renderer playerModelRenderer;   //Used to set material according to team
    [SerializeField]
    private GameObject playerCollider;      //Used to place collider on team layer

    [Header("Objects")]
    [SerializeField]
    private Material SPEMaterial;    //Material to use when on SPE team
    [SerializeField]
    private Material BLUMaterial;    //Material to use when on BLU team
    [SerializeField]
    private Material REDMaterial;    //Material to use when on RED team

    /// <summary>
    /// Called when script is enabled
    /// </summary>
    void Start()
    {
        EnablePlayer();
    }

    /// <summary>
    /// Initialize player taking in account client side specifications
    /// </summary>
    private void EnablePlayer()
    {
        //Always called for each player on every client/server
        /*
        - Set collider layer according to team
        - Set model layer according to team
        - Set model material according to team
        */
        switch (playerStats.playerTeam)
        {
            case Team.BLU:
                playerCollider.gameObject.layer = 11;
                playerModel.gameObject.layer = 11;
                playerModelRenderer.material = BLUMaterial;
                break;

            case Team.RED:
                playerCollider.gameObject.layer = 12;
                playerModel.gameObject.layer = 12;
                playerModelRenderer.material = REDMaterial;
                break;

            case Team.SPE:
                playerCollider.gameObject.layer = 10;
                playerModel.gameObject.layer = 10;
                playerModelRenderer.material = SPEMaterial;
                break;

            default:
                Debug.Log("SHOULD NOT HAPPEND: Player team was not found in PlayerEnabler/Global");
                break;
        }

        name = playerStats.playerName;

        //Called only when the player spawned is owned by the client
        if (isLocalPlayer)
        {
            //Enable client side scripts (So you only control this player and not others)
            foreach (MonoBehaviour mono in scripts)
            {
                mono.enabled = true;
            }

            playerModel.gameObject.layer = 9;   //Place PlayerModel on "NORENDER" layer to disable rendering for this client
            playerCamera.enabled = true;        //Enables the camera component of this player
            playerAudio.enabled = true;         //Enables the audio component of this player
            playerUI.SetActive(true);           //Enables player UI (Crosshair, HUD, Menu, etc...)
            GameObject.FindGameObjectWithTag("KillFeedPanel").GetComponent<KillFeedInput>().localPlayerIdentity = playerIdentity.name;

            playerCall.Call_UpdateScore();

            if (playerStats.playerTeam == Team.SPE)
            {
                playerHUD.enabled = false;
            }
            else
            {
                playerCall.Call_UpdateHealth();
            }

            /*
            - Disable shooting if spectator
            - Disable rigidbody if spectator
            - Defines camera culling mask according to team
            */
            switch (playerStats.playerTeam)
            {
                case Team.SPE:
                    playerShoot.enabled = false;
                    playerRigidBody.isKinematic = true;
                    playerCamera.cullingMask = m_Custom.layerMaskWTSPE;
                    break;

                case Team.BLU:
                case Team.RED:
                    playerShoot.enabled = true;
                    playerRigidBody.isKinematic = false;
                    playerCamera.cullingMask = m_Custom.layerMaskNOSPE;
                    break;

                default:
                    Debug.Log("SHOULD NOT HAVE HAPPENED: Player team not expected in PlayerEnabler/LocalPlayer");
                    break;
            }
        }
        else
        {
            //SAFEGUARDS
            foreach (MonoBehaviour mono in scripts)
            {
                mono.enabled = false;
            }

            playerModel.gameObject.layer = 0;   //Place PlayerModel on default layer to enable rendering for this client
            playerCamera.enabled = false;       //Disable the camera component of this player
            playerAudio.enabled = false;        //Disable the audio component of this player
            playerUI.SetActive(false);          //Disable player UI (Crosshair, HUD, Menu, etc...)

            /*
            - Disable shooting if spectator
            - Disable rigidbody if spectator
            - Defines camera culling mask according to team
            */
            switch (playerStats.playerTeam)
            {
                case Team.SPE:
                    playerShoot.enabled = false;
                    playerRigidBody.isKinematic = true;
                    playerCamera.cullingMask = m_Custom.layerMaskWTSPE;
                    break;

                case Team.BLU:
                case Team.RED:
                    playerShoot.enabled = false;
                    playerRigidBody.isKinematic = true;
                    playerCamera.cullingMask = m_Custom.layerMaskNOSPE;
                    break;

                default:
                    Debug.Log("SHOULD NOT HAVE HAPPENED: Player team not expected in PlayerEnabler/LocalPlayer");
                    break;
            }
        }
    }
}
