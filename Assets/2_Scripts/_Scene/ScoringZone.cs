using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// <summary>
/// This script is used to detect and trigger scoring on the server
/// It should be on the ScoringZone prefab and nowhere else
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class ScoringZone : NetworkBehaviour
{
    //TODO : Felix, comment your shit
    [Header("References(Scene)")]
    [SerializeField]
    private SceneOverlord sceneOverlord;    //Overlord used to transmit scoring to

    [Header("Parameters")]
    [SerializeField]
    private Team ScoringTeam;   //Team that will score in this zone

    private bool shouldCheck = true;

    /// <summary>
    /// Triggered each frame a GameObject with a collider enters the zone
    /// </summary>
    /// <param name="collider"></param>
    [Server]
    void OnTriggerStay(Collider collider)
    {
        if (shouldCheck)
        {
            CheckScoring(collider);         //Call a scoring check
            StartCoroutine(CheckReload());  //Prevents checking for some time (To prevent sending multiple RPC)
        }
    }

    [Server]
    IEnumerator CheckReload()
    {
        shouldCheck = false;
        yield return new WaitForSeconds(0.5f);
        shouldCheck = true;
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
                if (hitObject.GetComponentInParent<PlayerBallHandle>().isCarrying)
                {
                    sceneOverlord.Score(ScoringTeam);
                }
            }
        }
    }

}