using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class KillFeedInput : NetworkBehaviour
{
    [Header("References(Interface)")]
    #region References(Interface)
    [SerializeField]
    private GameObject KillFeedInfoPrefab;
    #endregion

    public string localPlayerName;

    [ClientRpc]
    public void Rpc_ParseKill(string killerName, string victimName, PlayerStats.Team killerTeam, PlayerStats.Team victimTeam)
    {
        bool isInvolved = killerName == localPlayerName || victimName == localPlayerName;
        GameObject newFeed = Instantiate(KillFeedInfoPrefab);
        newFeed.transform.SetParent(transform);
        newFeed.GetComponent<KillFeedInfoImage>().DisplayKill(killerName, victimName, killerTeam, victimTeam, isInvolved);
        Destroy(newFeed, 5.0f);
    }

}
