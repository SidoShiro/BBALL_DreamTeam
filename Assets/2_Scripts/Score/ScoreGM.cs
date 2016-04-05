using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class ScoreGM : NetworkBehaviour
{

    // Scores
    [SyncVar]
    public int score_blue;
    [SyncVar]
    public int score_red;

    public void TeamScored(Team team)
    {
        Debug.Log(team + " scored!");
        switch (team)
        {
            case Team.BLU:
                score_blue++;
                break;

            case Team.RED:
                score_red++;
                break;

            default:
                Debug.Log("ScoreGM error : Scoring player team not expected.");
                break;

        }

        GameObject[] player_list = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in player_list)
        {
            player.GetComponent<PlayerCommand>().Rpc_UpdateScore();
        }

    }
}
