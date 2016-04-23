using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Used to receive kills from local player and parse them towards every online client TODO : Place on SceneUI
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class KillFeedInput : NetworkBehaviour
{
    [Header("References(Interface)")]
    #region References(Interface)
    [SerializeField]
    private GameObject KillFeedInfoPrefab;
    #endregion

    public string localPlayerName;  //Name of the local player

    /// <summary>
    /// Remote procedure call sent to clients containing information on the kill
    /// </summary>
    /// <param name="killerName">Name of the eventual killer (Empty for hazards)</param>
    /// <param name="victimName">Name of the victim</param>
    /// <param name="killerTeam">Team of the killer (used to set color accordingly)</param>
    /// <param name="victimTeam">Team of the victim (used to set color accordingly)</param>
    /// <param name="damagetype">Type of damage (used to change icon)</param>
    [ClientRpc]
    public void Rpc_ParseKill(string killerName, string victimName, Team killerTeam, Team victimTeam, DamageType damageType)
    {
        bool isInvolved = killerName == localPlayerName || victimName == localPlayerName;
        GameObject newFeed = Instantiate(KillFeedInfoPrefab);
        newFeed.transform.SetParent(transform);
        newFeed.GetComponent<KillFeedInfoImage>().DisplayKill(killerName, victimName, killerTeam, victimTeam, damageType, isInvolved);
        Destroy(newFeed, 5.0f);
    }
}
