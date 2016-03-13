using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("References(Interface)")]
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Text scoreBluText;
    [SerializeField]
    private Text scoreRedText;

    /// <summary>
    /// Updates HUD health text to player current health
    /// </summary>
    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }

    /// <summary>
    /// Updates HUD ammo text to player current ammo
    /// </summary>
    public void UpdateAmmo(int ammo)
    {
        ammoText.text = ammo.ToString();
    }

    /// <summary>
    /// Update HUD score text to player current score
    /// </summary>
    public void UpdateScore(int scoreBlu, int scoreRed)
    {
        scoreBluText.text = scoreBlu.ToString();
        scoreRedText.text = scoreRed.ToString();
    }
}
