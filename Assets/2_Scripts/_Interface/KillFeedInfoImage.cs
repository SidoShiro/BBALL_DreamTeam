using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

public class KillFeedInfoImage : MonoBehaviour
{
    [Header("References(Interface)")]
    #region References(Interface)
    [SerializeField]
    private List<GameObject> toActivate;
    [SerializeField]
    private Image BGImage;
    [SerializeField]
    private Text KillerText;
    [SerializeField]
    private Image KillImage;
    [SerializeField]
    private Text VictimText;
    #endregion

    public void DisplayKill(string killerName, string victimName, PlayerStats.Team killerTeam, PlayerStats.Team victimTeam, bool isInvolved)
    {
        KillerText.text = killerName;
        VictimText.text = victimName;

        KillerText.color = m_Custom.GetColorFromTeam(killerTeam);
        VictimText.color = m_Custom.GetColorFromTeam(victimTeam);

        if (isInvolved)
        {
            BGImage.color = new Color32(255, 162, 123, 255);
        }
        else
        {
            BGImage.color = new Color32(53, 69, 70, 255);
        }

        foreach (GameObject go in toActivate)
        {
            go.SetActive(true);
        }
    }
}
