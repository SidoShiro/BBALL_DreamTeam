using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This class stores player stats to be used in game
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class PlayerStats : NetworkBehaviour
{
    public enum Team { SPE = 0, BLU = 1, RED = 2 }  //Enum for Teams (Used by calling PlayerStats.Team)

    [Header("Stats")]
    [SyncVar]
    public Team playerTeam = Team.BLU;  //Player's current team
    [SyncVar]
    public int playerHealth = 200;      //Player's current health   //TODO : Max health managment
    [SyncVar]
    public bool isCarrying;             //Ball carrying toggle

    [Header("Local")]
    public bool isReceivingInputs;  //Used to disable inputs on Menu/etc ...

}