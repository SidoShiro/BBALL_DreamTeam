using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is attached to the ball and is used to detect player picking up the ball
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class BallPickUp : NetworkBehaviour
{
    [Header("Prefabs")]
    #region prefabs
    [SerializeField]
    private GameObject ballBody;
    #endregion

    /// <summary>
    /// Called when something enters trigger
    /// </summary>
    /// <param name="col">Collision information</param>
    void OnTriggerEnter(Collider col)
    {
        if (isServer)   //We only want to check triggers on the server
        {
            GameObject colParent = col.transform.parent.gameObject;         //Find the parent of the collider
            if (colParent.tag == "Player")
            {
                colParent.GetComponent<PlayerBallHandle>().Rpc_PickBall();  //Tell the player he picked the ball on all clients
                Rpc_DestroyBall();                                          //Destroy the ball on all clients
            }

            if (col.gameObject.GetComponent<KillZone>() != null)
            {
                Vector3 spawnpos = Vector3.zero;
                GameObject defaultspawn = GameObject.FindGameObjectWithTag("SPEBallRespawn");
                if (defaultspawn != null)
                {
                    spawnpos = defaultspawn.transform.position;
                }
                else
                {
                    Debug.Log("Could not find ball spawn in scene, make sure there is one in the scene");
                }
                GameObject ball = (GameObject)Instantiate(ballBody, spawnpos, Quaternion.identity);     //Creates new ball
                NetworkServer.Spawn(ball);                                                              //Instantiate it on all clients
                Rpc_DestroyBall();
            }
        }
    }
    
    /// <summary>
    /// Called to destroy the ball on all clients
    /// </summary>
    [ClientRpc]
    public void Rpc_DestroyBall()
    {
        Destroy(gameObject);
    }
}