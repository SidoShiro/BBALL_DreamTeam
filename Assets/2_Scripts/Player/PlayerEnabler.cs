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
    [Header("SCRIPTS")]
    public Component[] scriptsToEnable;

    [Header("OTHERS")]
    public GameObject playerModel;      //Player Model
    public GameObject playerHead;       //Player Empty containing camera
    public Rigidbody playerRigidBody;   //Player RigidBody

    /// <summary>
    /// Triggered when script is enabled
    /// </summary>
    void Start()
    {
        if (isLocalPlayer)
        {
            //Enable client side scripts
            foreach(MonoBehaviour monoscript in scriptsToEnable)
            {
                monoscript.enabled = true;
            }

            //Enable client side objects

            playerModel.gameObject.layer = 10;      //Place PlayerModel on "Mine" layer to disable rendering for this client
            playerHead.SetActive(true);             //Activate personal camera
            playerRigidBody.isKinematic = false;    //Other players are kinematic by default to prevent pushing
        }
    }
}
