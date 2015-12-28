using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This class stores player stats to be used in game
/// </summary>
public class PlayerStats : NetworkBehaviour
{
    public enum Team { SPE = 0, BLU = 1, RED = 2 }  //Enum for Teams (Used by calling PlayerStats.Team)

    [Header("STATS")]
    [SyncVar]
    public Team playerTeam = Team.BLU;  //Player's current team

    [SyncVar]
    public int playerHealth = 200;      //Player's current health

    [SyncVar]
    public bool isCarrying;             //Is the player carrying the ball ?
}