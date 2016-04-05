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

    public string localPlayerIdentity;

    [ClientRpc]
    public void Rpc_ParseKill(string killerIdentity, string victimIdentity, Team killerTeam, Team victimTeam, DamageType damageType)
    {
        bool isInvolved = killerIdentity == localPlayerIdentity || victimIdentity == localPlayerIdentity;
        GameObject newFeed = Instantiate(KillFeedInfoPrefab);
        newFeed.transform.SetParent(transform);
        newFeed.GetComponent<KillFeedInfoImage>().DisplayKill(killerIdentity, victimIdentity, killerTeam, victimTeam, damageType, isInvolved);
        Destroy(newFeed, 5.0f);
    }
}
