using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to enable client specific components that are disabled by default server wise:
/// We only want to control our player not others
/// We only want one camera active: our player's camera
/// etc...
/// </summary>
public class PlayerEnabler : NetworkBehaviour
{
    [Header("ENABLE ON START")]
    public Component[] scripts;

    [Header("OTHERS")]
    public GameObject playerModel;      //Player Model for layering
    public Camera playerCamera;

    /// <summary>
    /// Triggered when script is enabled
    /// </summary>
    void Start()
    {
        if (isLocalPlayer)
        {
            //Enable client side scripts
            foreach(MonoBehaviour mono in scripts)
            {
                mono.enabled = true;
            }

            //Enable client side objects
            playerModel.gameObject.layer = 10;      //Place PlayerModel on "Mine" layer to disable rendering for this client
            playerCamera.enabled = true;
        }
    }
}
