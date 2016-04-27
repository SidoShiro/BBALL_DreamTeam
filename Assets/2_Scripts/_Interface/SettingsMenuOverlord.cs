using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsMenuOverlord : MonoBehaviour
{
    [Header("References(Interface)")]
    [SerializeField]
    private GameObject SettingsPanel;
    [SerializeField]
    private Slider sensivitySlider;
    [SerializeField]
    private Dropdown hitsoundDropDown;
    [SerializeField]
    private Text sensivityText;

    [Header("HitsoundList")]
    [SerializeField]
    public List<AudioClip> hitsoundList;

    /// <summary>
    /// Called once when instantiated
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);    //Used to keep the option menu everywhere
        if (PlayerPrefs.HasKey(PlayerPrefProperties.Sensivity))
        {
            sensivitySlider.value = PlayerPrefs.GetInt(PlayerPrefProperties.Sensivity);
        }
        else
        {
            sensivitySlider.value = 10;
        }
        if (PlayerPrefs.HasKey(PlayerPrefProperties.Hitsound))
        {
            hitsoundDropDown.value = PlayerPrefs.GetInt(PlayerPrefProperties.Hitsound);
        }
        else
        {
            hitsoundDropDown.value = 0;
        }
        WriteSensivity();
        WriteHitsound();
    }

    /// <summary>
    /// Write sensivity to PlayerPrefs
    /// </summary>
    public void WriteSensivity()
    {
        PlayerPrefs.SetInt(PlayerPrefProperties.Sensivity, (int)sensivitySlider.value);
        sensivityText.text = sensivitySlider.value.ToString();
    }

    /// <summary>
    /// Write hitsound to PlayerPrefs
    /// </summary>
    public void WriteHitsound()
    {
        PlayerPrefs.SetInt(PlayerPrefProperties.Hitsound, hitsoundDropDown.value);
    }

    public void OpenPanel()
    {
        SettingsPanel.SetActive(true);
    }

    /// <summary>
    /// Disable options Menu
    /// </summary>
    public void GoBack()
    {
        SettingsPanel.SetActive(false);
    }
}
