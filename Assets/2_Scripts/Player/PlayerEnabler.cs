using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to enable client specific components that are disabled by default server wise:
/// We only want to control our player not others
/// We only want one camera active: our player's camera
/// etc...
/// 
/// This script is also used to initialize player compponents depending on stats:
/// Matches the player model material to team
/// etc...
/// </summary>
public class PlayerEnabler : NetworkBehaviour
{
    [Header("Client side")]
    public GameObject playerModel;      //Player Model
    public GameObject playerHead;       //Player Empty containing camera
    public Rigidbody playerRigidBody;   //Player RigidBody

    [Header("Team specific")]
    public Material SPETeamMaterial;    //Material to use when on SPE team
    public Material BLUTeamMaterial;    //Material to use when on BLU team
    public Material REDTeamMaterial;    //Material to use when on RED team

    void Start()
    {
        if (isLocalPlayer)
        {
            //Enable client side scripts
            GetComponent<PlayerMovement>().enabled = true;          //Enables movement for this player
            GetComponent<PlayerMouseLookCustom>().enabled = true;   //Enables mouselook for this layer

            //Enable client side objects
            playerModel.gameObject.layer = 10;      //Place PlayerModel on "Mine" layer to disable rendering
            playerHead.SetActive(true);             //Activate personnal camera
            playerRigidBody.isKinematic = false;    //Activate personnal movement

            //Set material according to team
            switch (GetComponent<PlayerStats>().playerTeam)
            {
                case PlayerStats.Team.BLU:
                    playerModel.GetComponent<Renderer>().material = BLUTeamMaterial;
                    break;

                case PlayerStats.Team.RED:
                    playerModel.GetComponent<Renderer>().material = REDTeamMaterial;
                    break;

                default:
                    playerModel.GetComponent<Renderer>().material = SPETeamMaterial;
                    break;
            }
        }
    }
}
