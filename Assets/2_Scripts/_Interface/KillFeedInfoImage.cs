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

    public void DisplayKill(NetworkIdentity killerIdentity, NetworkIdentity victimIdentity, bool isInvolved)
    {
        KillerText.text = killerIdentity.gameObject.name;
        VictimText.text = victimIdentity.gameObject.name;

        KillerText.color = m_Custom.GetColorFromTeam(killerIdentity.gameObject.GetComponent<PlayerStats>().playerTeam);
        VictimText.color = m_Custom.GetColorFromTeam(victimIdentity.gameObject.GetComponent<PlayerStats>().playerTeam);

        if (isInvolved)
        {
            BGImage.color = new Color32(255, 146, 107, 100);
        }
        else
        {
            BGImage.color = new Color32(53, 69, 70, 100);
        }

        foreach (GameObject go in toActivate)
        {
            go.SetActive(true);
        }
    }
}
