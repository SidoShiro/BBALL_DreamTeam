using UnityEngine;
using UnityEngine.Networking;

public class KillFeedInput : NetworkBehaviour
{
    [Header("References(Interface)")]
    #region References(Interface)
    [SerializeField]
    private GameObject KillFeedInfoPrefab;
    #endregion

    public void ParseKill(NetworkIdentity killerIdentity, NetworkIdentity victimIdentity, bool isInvolved)
    {
        GameObject newFeed = Instantiate(KillFeedInfoPrefab);
        newFeed.transform.SetParent(transform);
        newFeed.GetComponent<KillFeedInfoImage>().DisplayKill(killerIdentity, victimIdentity, isInvolved);
        Destroy(newFeed, 5.0f);
    }

}
