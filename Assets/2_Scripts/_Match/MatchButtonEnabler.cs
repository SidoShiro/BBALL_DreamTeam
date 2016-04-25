using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

//TODO : Comment
public class MatchButtonEnabler : MonoBehaviour
{
    private NetworkOverlord networkOverlord;
    [Header("References(Interface)")]
    [SerializeField]
    private Text ServerNameText;
    [SerializeField]
    private Text MapNameText;
    [SerializeField]
    private Text HostNameText;
    [SerializeField]
    private Text PlayerCountText;
    [Header("Match")]
    public MatchDesc corMatch;

    void Start()
    {
        networkOverlord = GameObject.Find("NetworkGM").GetComponent<NetworkOverlord>();
    }

    public void Initiliaze(string servername, string mapname, string hostname, string playercount)
    {
        ServerNameText.text = servername;
        MapNameText.text = mapname;
        HostNameText.text = hostname;
        PlayerCountText.text = playercount;
    }

    public void Join()
    {
        networkOverlord.matchName = corMatch.name;
        networkOverlord.matchSize = (uint)corMatch.currentSize;
        networkOverlord.matchMaker.JoinMatch(corMatch.networkId, "", networkOverlord.OnMatchJoined);
    }
}
