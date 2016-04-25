using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO : Comment
public class MatchBrowser : MonoBehaviour
{
    [Header("References(Scene)")]
    private NetworkOverlord networkOverlord;
    [SerializeField]
    private GameObject Content;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject MatchButton;

    private List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        networkOverlord = GameObject.Find("NetworkGM").GetComponent<NetworkOverlord>();
        networkOverlord.StartMatchMaker();
        StopAllCoroutines();
        StartCoroutine(AutoRefresh());
    }

    public void StopMatchMaker()
    {
        networkOverlord.StopMatchMaker();
    }

    IEnumerator AutoRefresh()
    {
        while (true)
        {
            RefreshBrowser();
            yield return new WaitForSeconds(3.0f);
        }
    }

    public void RefreshBrowser()
    {
        if(networkOverlord.matchMaker != null)
        {
            networkOverlord.matchMaker.ListMatches(0, 20, "", networkOverlord.OnMatchList);
        }        
        //*/
        foreach (GameObject go in buttons)
        {
            Destroy(go);
        }
        buttons.Clear();
        //*/
        if (networkOverlord.matches == null)
        {
            Debug.Log("Refreshed with no match");
        }
        else
        {
            foreach (var match in networkOverlord.matches)
            {
                GameObject newMatchButton = Instantiate(MatchButton);
                newMatchButton.transform.SetParent(Content.transform);
                string name = match.name;
                string server = name.Substring(0, name.IndexOf("|") - 1);
                string map = name.Substring(name.IndexOf("|") + 1, name.LastIndexOf("|") - 1 - name.IndexOf("|") - 1);
                string host = name.Substring(name.LastIndexOf("|") + 1);
                newMatchButton.GetComponent<MatchButtonEnabler>().Initiliaze(server, map, host, match.currentSize.ToString() + "/" + match.maxSize.ToString());
                newMatchButton.GetComponent<MatchButtonEnabler>().corMatch = match;
                buttons.Add(newMatchButton);
            }
        }
    }
}
