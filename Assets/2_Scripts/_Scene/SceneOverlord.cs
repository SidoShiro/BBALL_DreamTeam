using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Used to store global scene variables
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class SceneOverlord : NetworkBehaviour
{
    [Header("Scoring")]
    [SyncVar]
    public int scoreBLU = 0;
    [SyncVar]
    public int scoreRED = 0;

    public void Score(Team team)
    {
        switch (team)
        {
            case Team.BLU:
                scoreBLU++;
                break;

            case Team.RED:
                scoreRED++;
                break;

            default:
                Debug.Log("This should not have happened!");
                break;
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            //player.GetComponent<PlayerCommand>().Rpc_UpdateScore();
            player.GetComponent<PlayerCall>().Call_KillPlayer("");
        }
    }
}
