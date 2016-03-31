using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class MatchRequestCustom : CreateMatchRequest
{
    public MatchRequestCustom() { }

    public Dictionary<string, string> matchAttributesCustom { get; set; }
}
