using UnityEngine;
using UnityEngine.Networking;

public class PlayerEnabler : NetworkBehaviour
{
    public GameObject PlayerModel;
    public GameObject PlayerHead;
    public Rigidbody PlayerRigidBody;
    public Collider PlayerCollider;


    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<MouseLookCustom>().enabled = true;
            PlayerModel.gameObject.layer = 10;
            PlayerHead.SetActive(true);
            PlayerRigidBody.isKinematic = false;
        }
    }

    void LateUpdate()
    {
        PlayerCollider.transform.rotation = Quaternion.identity;    //Fix collider in space
    }
}
