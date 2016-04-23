using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// <summary>
/// Used to store global scene variables
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class SceneOverlord : NetworkBehaviour
{
    //TODO : Felix, comment your shit
    [Header("Global settings")]
    [SyncVar]
    public bool isFrozen;
    [SyncVar]
    public float respawntime;

    [Header("Scoring")]
    [SyncVar]
    public int scoreBLU = 0;
    [SyncVar]
    public int scoreRED = 0;

    [Server]
    public void Score(Team team)
    {
        switch (team)
        {
            case Team.BLU:
                scoreBLU++;
                StartNewRound(Team.RED);
                break;

            case Team.RED:
                scoreRED++;
                StartNewRound(Team.BLU);
                break;

            default:
                Debug.Log("Grats! You broke everything: this should not have happened!");
                break;
        }
    }

    [Server]
    void StartNewRound(Team losingteam)
    {
        respawntime = 0.0f;
        KillAllPlayers();
        isFrozen = true;
        FreezeAllPlayerUpdate();
        StartCoroutine(ReadyGO(3.0f));
    }

    IEnumerator ReadyGO(float time)
    {
        yield return new WaitForSeconds(time);
        respawntime = 2.0f;
        isFrozen = false;
        FreezeAllPlayerUpdate();
    }

    void FreezeAllPlayerUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerStats>().playerTeam != Team.SPE)
            {
                player.GetComponent<PlayerCall>().Call_FreezePlayerUpdate();
            }
        }
    }

    void KillAllPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerStats>().playerTeam != Team.SPE)
            {
                player.GetComponent<PlayerCall>().Call_KillPlayer();
            }
        }
    }
}
