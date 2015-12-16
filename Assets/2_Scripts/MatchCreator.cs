using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class MatchCreator : MonoBehaviour
{
    List<MatchDesc> MatchList = new List<MatchDesc>();
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
        CreateMatchRequest MatchRequest = new CreateMatchRequest();
        MatchRequest.name = "Match " + System.Guid.NewGuid().ToString("N");
        MatchRequest.size = 10;
        MatchRequest.advertise = true;
        MatchRequest.password = "";

        MatchRequest.matchAttributes = new Dictionary<string, long>();
        MatchRequest.matchAttributes.Add(MatchProperty.MapName, 1);
        MatchRequest.matchAttributes.Add(MatchProperty.HostName, 1);

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
