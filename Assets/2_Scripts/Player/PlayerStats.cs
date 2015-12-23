using UnityEngine;

/// <summary>
/// This class stores player stats to be used in game
/// </summary>
public class PlayerStats : MonoBehaviour
{
    //Enumerations
    public enum Team { SPE = 0, BLU = 1, RED = 2}

    //Default values
    [Header("STATS")]
    public Team playerTeam = Team.BLU;
    public int playerHealth = 200;
    public bool isCarrying;
}