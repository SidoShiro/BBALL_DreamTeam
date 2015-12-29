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
        if (isLocalPlayer)
        {
            CreateUI();
        }   
    }

    void CreateUI()
    {
        //playerUI.GetComponent<PlayerMenu>().playerCommand = this;
        Instantiate(playerUI);
    }

    public void TrySHit()
    {
        CmdSpawnPlayerRigidBody();
    }

    //*
    [Command]
    private void CmdSpawnPlayerRigidBody()
    {
        ClientScene.RemovePlayer(1);
        ClientScene.AddPlayer(1);
    }
    //*/
}
