using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class KillFeedInfoImage : MonoBehaviour
{
    [Header("References(Interface)")]
    #region References(Interface)
    [SerializeField]
    private Image BGImage;
    [SerializeField]
    private Text KillerText;
    [SerializeField]
    private Image KillImage;
    [SerializeField]
    private Text VictimText;
    #endregion

    public void DisplayKill(string killerIdentity, string victimIdentity, int killerTeam, int victimTeam, bool isInvolved)
    {
        KillerText.text = killerIdentity;
        VictimText.text = victimIdentity;

        KillerText.color = m_Custom.GetColorFromTeam(killerTeam);
        VictimText.color = m_Custom.GetColorFromTeam(victimTeam);

        if (isInvolved)
        {
            BGImage.color = new Color32(246, 196, 163, 150);
        }
        else
        {
            BGImage.color = new Color32(53, 69, 70, 150);
        }
    }
}
