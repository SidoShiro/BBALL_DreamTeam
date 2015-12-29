using UnityEngine;
using UnityEngine.Networking;

public class PlayerOverlord : NetworkBehaviour
{
    public GameObject playerRigidBody;
    public GameObject playerUI;
    
    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {

    }

    void Start()
    {
        if(isLocalPlayer)
            CmdSpawnPlayerUI();
    }

    //*
    [Command]
    private void CmdSpawnPlayerUI()
    {
        ClientScene.AddPlayer(1);
    }
    //*/
}
