using UnityEngine;
using UnityEngine.Networking;

public class PlayerCommand : NetworkBehaviour
{
    [Header("SPAWN PLAYER")]
    public GameObject playerRigidBody;
    public GameObject playerUI;

    [Header("SHOOT ROCKET")]
    public GameObject rocketBody;
    public GameObject playerCollider;
    public Camera playerCamera;
    public Transform playerFireOutputTransform;
    public LayerMask layerMaskBLU;
    public LayerMask layerMaskRED;

    private GameObject previousUI;

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Start()
    {
        if (isLocalPlayer)
        {
            CreateUI();
        }
    }

    /// <summary>
    /// Creates new PlayerUI and destroys previous one
    /// </summary>
    void CreateUI()
    {
        playerUI.GetComponent<PlayerMenu>().playerCommand = this;
        previousUI = Instantiate(playerUI);
    }

    /// <summary>
    /// Call for a player respawn in specified team
    /// </summary>
    /// <param name="newteam"></param>
    public void Call_RespawnPlayer(PlayerStats.Team newteam)
    {
        Destroy(previousUI);
        Cmd_RespawnPlayer(newteam);
    }

    /// <summary>
    /// Call for this player to shoot a rocket
    /// </summary>
    /// <param name="rocketBody">Rocket prefab</param>
    /// <param name="playerFireOutputTransform">Transform to spawn the rocket from rocket</param>
    /// <param name="targetrotation">Rotation of the rocket when spawned</param>
    public void Call_ShootRocket()
    {
        Cmd_ShootRocket();
    }

    [Command]
    void Cmd_RespawnPlayer(PlayerStats.Team newteam)
    {
        GameObject[] spawnTab;
        switch (newteam)
        {
            case PlayerStats.Team.BLU:
                spawnTab = GameObject.FindGameObjectsWithTag("BLUSpawn");
                break;

            case PlayerStats.Team.RED:
                spawnTab = GameObject.FindGameObjectsWithTag("REDSpawn");
                break;

            default:
                spawnTab = GameObject.FindGameObjectsWithTag("SPESpawn");
                break;
        }
        Transform spawnPoint = spawnTab[Random.Range(0, (spawnTab.Length - 1))].transform;
        GameObject playerNew = (GameObject)Instantiate(playerRigidBody, spawnPoint.position, spawnPoint.rotation);
        playerNew.GetComponent<PlayerStats>().playerTeam = newteam;
        playerNew.name = "PlayerRigidBody(Clone)";
        NetworkServer.DestroyPlayersForConnection(connectionToClient);
        NetworkServer.AddPlayerForConnection(connectionToClient, playerNew, playerControllerId);
    }

    [Command]
    void Cmd_ShootRocket()
    {
        LayerMask layerMask = gameObject.layer;
        if (gameObject.layer == 11)
        {
            layerMask = layerMaskBLU;
        }
        else
        {
            layerMask = layerMaskRED;
        }

        RaycastHit hit;                                                                                                 //Used to store raycast hit data
        //Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));                   //Define ray as player aiming point
        Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward, out hit, 1000.0f,layerMask,QueryTriggerInteraction.Ignore);                                //Casts the ray
        Vector3 relativepos = hit.point - playerFireOutputTransform.position;                                           //Get the vector to parcour
        Quaternion targetrotation = Quaternion.LookRotation(relativepos);                                               //Get the needed rotation of the rocket to reach that point
        GameObject rocket = (GameObject)Instantiate(rocketBody, playerFireOutputTransform.position, targetrotation);    //Spawns rocket at gunpoint with needed rotation
        rocket.layer = playerCollider.layer;                                                                            //Give rocket same layer as player
        NetworkServer.Spawn(rocket);
    }
}
