using UnityEngine;
using UnityEngine.UI;

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

    [Header("Parameters")]
    [SerializeField]
    private Sprite HazardSprite;
    [SerializeField]
    private Sprite ExplosionSprite;

    public void DisplayKill(string killerIdentity, string victimIdentity, Team killerTeam, Team victimTeam, DamageType damagetype, bool isInvolved)
    {
        KillerText.text = killerIdentity;
        VictimText.text = victimIdentity;

        KillerText.color = m_Custom.GetColorFromTeam(killerTeam);
        VictimText.color = m_Custom.GetColorFromTeam(victimTeam);

        switch (damagetype)
        {
            case DamageType.Hazard:
                KillImage.sprite = HazardSprite;
                break;
            case DamageType.Explosion:
                KillImage.sprite = ExplosionSprite;
                break;
        }

        if (isInvolved)
        {
            BGImage.color = new Color32(246, 196, 163, 255);
        }
        else
        {
            BGImage.color = new Color32(76, 90, 105, 255);
        }
    }
}
