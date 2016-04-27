using UnityEngine;

public class SettingsMenuButton : MonoBehaviour
{
    public void OpenSettings()
    {
        GameObject.FindGameObjectWithTag("SettingsUI").GetComponent<SettingsMenuOverlord>().OpenPanel();
    }
}
