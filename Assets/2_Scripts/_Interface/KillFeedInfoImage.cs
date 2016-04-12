using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used on the KillFeedInfoImage prefab to modify text, image and colors accoring to input
/// </summary>
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
    #region Parameters
    [SerializeField]
    private Sprite HazardSprite;
    [SerializeField]
    private Sprite ExplosionSprite;
    #endregion

    /// <summary>
    /// Updates components according to inputs
    /// </summary>
    /// <param name="killerName">Name of the eventual killer (Empty for hazards)</param>
    /// <param name="victimName">Name of the victim</param>
    /// <param name="killerTeam">Team of the killer (used to set color accordingly)</param>
    /// <param name="victimTeam">Team of the victim (used to set color accordingly)</param>
    /// <param name="damagetype">Type of damage (used to change icon)</param>
    /// <param name="isInvolved">Is the local player involved in the kill (killer or victim)</param>
    public void DisplayKill(string killerName, string victimName, Team killerTeam, Team victimTeam, DamageType damagetype, bool isInvolved)
    {
        //Set player names
        KillerText.text = killerName;
        VictimText.text = victimName;

        //Define player names color according to team
        KillerText.color = m_Custom.GetColorFromTeam(killerTeam);
        VictimText.color = m_Custom.GetColorFromTeam(victimTeam);

        //Define icon according to damage
        switch (damagetype)
        {
            case DamageType.Hazard:
                KillImage.sprite = HazardSprite;
                break;
            case DamageType.Explosion:
                KillImage.sprite = ExplosionSprite;
                break;
        }

        //Set background color according on player involvment in the kill
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
