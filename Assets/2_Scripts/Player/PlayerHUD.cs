using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("References(Interface)")]
    [SerializeField]
    private Text healthText;

    /// <summary>
    /// Updates HUD health text to player current health
    /// </summary>
    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }

}
