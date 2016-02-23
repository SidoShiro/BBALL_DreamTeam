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
        if (isLocalPlayer)
        {
            playerSpawnerPanel.SetActive(true); //Activates spawner UI
            playerSpawner.enabled = true;       //Enables respawn
        }
    }

}
