using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour
{
    [Header("References(Player)")]
    [SerializeField]
    private AudioSource hitsound;

    [Header("References(Interface)")]
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text scoreBluText;
    [SerializeField]
    private Text scoreRedText;
    [SerializeField]
    private float barLenght;
    [SerializeField]
    private float percentOfHp;

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
        //healthBar.gameObject.SetActive(true); //pas opti a changer
        healthText.text = health.ToString();
        //Set lenght of Healthbar
        //healthBar.value = health;
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
        float scale = 1.0f + magnitude * 3.0f; //TODO : Animate hitmarker ?
        hitMarker.transform.localScale = new Vector3(scale, scale, scale);
        StartCoroutine(HitMark());
        hitsound.pitch = 2.0f - magnitude;
        hitsound.Play();
    }

    IEnumerator HitMark()
    {
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        hitMarker.SetActive(false);
    }

}
