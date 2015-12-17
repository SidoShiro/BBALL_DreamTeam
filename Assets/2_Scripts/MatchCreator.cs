using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class MatchCreator : MonoBehaviour
{
    NetworkMatch networkMatch;

    void Awake()
    {
        networkMatch = gameObject.AddComponent<NetworkMatch>();
    }

    void Update()
    {

    }

    public void CreateMatch()
    {
        MatchRequestCustom MatchRequest = new MatchRequestCustom();
        MatchRequest.name = "Match " + System.Guid.NewGuid().ToString("N");
        MatchRequest.size = 10;
        MatchRequest.advertise = true;
        MatchRequest.password = "";

        MatchRequest.matchAttributesCustom = new Dictionary<string, string>();
        MatchRequest.matchAttributesCustom.Add(MatchProperty.DisplayName, "<DISPLAYNAME>");
        MatchRequest.matchAttributesCustom.Add(MatchProperty.HostName, "<HOSTNAME>");
        MatchRequest.matchAttributesCustom.Add(MatchProperty.MapName, "<MAPNAME>");

        networkMatch.CreateMatch(MatchRequest, OnMatchCreate);
    }

    public void OnMatchCreate(CreateMatchResponse matchResponse)
    {
        if (matchResponse.success)
            Debug.Log("Create match succeeded");
        else
            Debug.LogError("Create match failed");
    }
}
