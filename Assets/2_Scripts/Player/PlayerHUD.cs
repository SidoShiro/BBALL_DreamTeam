using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour
{
    [Header("References(Interface)")]
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text scoreBluText;
    [SerializeField]
    private Text scoreRedText;
    [SerializeField]
    public Slider healthBar;

    [Header("Parameters")]
    [SerializeField]
    private GameObject hitMarker;
    [SerializeField]
    private float displayTime;

    /// <summary>
    /// Updates HUD health text to player current health
    /// </summary>
    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
        healthBar.value = health;

    }

    /// <summary>
    /// Update HUD score text to player current score
    /// </summary>
    public void UpdateScore(int scoreBlu, int scoreRed)
    {
        scoreBluText.text = scoreBlu.ToString();
        scoreRedText.text = scoreRed.ToString();
    }

    /// <summary>
    /// Enables hit marker
    /// </summary>
    public void ToggleHitMarker(float magnitude)
    {
        StartCoroutine(HitMark());
    }

    IEnumerator HitMark()
    {
        yield return 500;
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        hitMarker.SetActive(false);
    }

}
