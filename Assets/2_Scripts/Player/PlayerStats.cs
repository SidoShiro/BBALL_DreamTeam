using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum Team { SPE = 0, BLU = 1, RED = 2}
    public Team PlayerTeam = Team.BLU;
    public int PlayerHealth = 200;
}
