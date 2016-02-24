using UnityEngine;
using UnityEngine.Networking;

public class PlayerSpawnerEnabler : NetworkBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject playerSpawnerPanel;
    [SerializeField]
    private PlayerSpawner playerSpawner;

    /// <summary>
    /// Called when loaded
    /// </summary>
    void Start()
    {
        ToggleSpawner();
    }

    private void ToggleSpawner()
    {
        if (isLocalPlayer)
        {
            playerSpawnerPanel.SetActive(true);
            playerSpawner.enabled = true;
        }
        else
        {
            playerSpawnerPanel.SetActive(false);
            playerSpawner.enabled = false;
        }
    }

}
