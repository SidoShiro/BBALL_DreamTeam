using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Text healthText;

    [Header("Modular")]
    public PlayerStats playerStats; //Used to track player health, defined by playerCall

    /// <summary>
    /// Called whn enabled
    /// </summary>
    void Start()
    {
        if(playerStats.playerTeam == PlayerStats.Team.SPE)
        {
            enabled = false;
        }
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    void Update()
    {
        UpdateHealth(); //TODO : Only update on change
    }

    /// <summary>
    /// Updates HUD health text to player current health
    /// </summary>
    private void UpdateHealth()
    {
        healthText.text = playerStats.playerHealth.ToString();
    }


}
