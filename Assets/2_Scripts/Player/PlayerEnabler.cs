using UnityEngine;
using UnityEngine.Networking;

public class PlayerEnabler : NetworkBehaviour
{
    [Header("Client side")]
    public GameObject PlayerModel;
    public GameObject PlayerHead;
    public Rigidbody PlayerRigidBody;

    [Header("Team specific")]
    public Material SPETeamMaterial;
    public Material BLUTeamMaterial;
    public Material REDTeamMaterial;

    void Start()
    {
        if (isLocalPlayer)
        {
            //Enable client side scripts
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<PlayerMouseLookCustom>().enabled = true;

            //Enable client side objects
            PlayerModel.gameObject.layer = 10;
            PlayerHead.SetActive(true);
            PlayerRigidBody.isKinematic = false;

            //Set material according to team
            switch (GetComponent<PlayerStats>().PlayerTeam)
            {

                case PlayerStats.Team.BLU:
                    PlayerModel.GetComponent<Renderer>().material = BLUTeamMaterial;
                    break;

                case PlayerStats.Team.RED:
                    PlayerModel.GetComponent<Renderer>().material = REDTeamMaterial;
                    break;

                default:
                    PlayerModel.GetComponent<Renderer>().material = SPETeamMaterial;
                    break;
            }
        }
    }
}
