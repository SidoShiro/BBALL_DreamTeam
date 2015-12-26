using UnityEngine;

/// <summary>
/// This class stores player stats to be used in game
/// </summary>
public class PlayerStats : MonoBehaviour
{
    public enum Team { SPE = 0, BLU = 1, RED = 2}   //Enum for team
    
    [Header("STATS")]
    public Team playerTeam = Team.BLU;  //Player current team
    public int playerHealth = 200;      //Player current health
    public bool isCarrying;             //Is the player carrying the ball ?

    void Start()
    {
        playerHealth = 200;
    }
}