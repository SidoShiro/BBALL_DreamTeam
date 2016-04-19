using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// This class stores player stats to be used in game
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class PlayerStats : NetworkBehaviour
{
    //Enum for Teams (Used by calling Team)

    [Header("Stats")]
    [SyncVar]
    public string playerName;
    [SyncVar]
    public Team playerTeam = Team.BLU;  //Player's current team
    public int playerHealth;            //Player's current health


    [Header("Local")]
    public bool isReceivingInputs;  //Used to disable inputs on Menu/etc ...

    /// <summary>
    /// Donne le droit au createur d'une partie de choisir la vie max
    /// </summary>
    public void ChangeHealth(InputField health)
    {
        string h = health.text;
        if (int.Parse(h) > 999)
        {
            Debug.Log("There is too much health for your player ! (must be under 999)");
            playerHealth = 300;
        }
        else
        {
            playerHealth = int.Parse(h);
            Debug.Log("Health changed");
        }
    }
}