using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;


public class MatchInfoDebug : MonoBehaviour
{
    public GameObject NetworkGM;
    public Text infotext;

    private NetworkManager networkManager;

    public void Awake()
    {
        networkManager = NetworkGM.GetComponent<NetworkManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            networkManager.StartMatchMaker();
            networkManager.matchMaker.ListMatches(0, 20, "", OnMatchList); //logs correctly
        }
    }

    public virtual void OnMatchList(ListMatchResponse listmatchResponse)
    {
        Debug.Log("OnMatchList");
        infotext.text = listmatchResponse.matches.Count.ToString();
        networkManager.StopMatchMaker();
    }
}
