using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to detect and trigger scoring on the server
/// It should be on the ScoringZone prefab and nowhere else
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class ScoringZone : NetworkBehaviour
{
    [Header("References(Scene)")]
    [SerializeField]
    private SceneOverlord sceneOverlord;    //Overlord used to transmit scoring to

    [Header("Parameters")]
    [SerializeField]
    private Team ScoringTeam;   //Team that will score in this zone

    /// <summary>
    /// Triggered when a GameObject with a collider enters the zone
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        if (isServer)
        {
            CheckScoring(collider); //Call a scoring check
        }
    }

    /// <summary>
    /// Checks if the object entering the zone is valid for scoring and acts in consequence
    /// </summary>
    /// <param name="collider">Collider entering the zone</param>
    void CheckScoring(Collider collider)
    {
        GameObject hitObject = collider.gameObject;
        if (hitObject.tag == "PlayerCollider")
        {
            if (hitObject.GetComponentInParent<PlayerStats>().playerTeam == ScoringTeam)
            {
                sceneOverlord.Score(ScoringTeam);
            }
        }
    }

}