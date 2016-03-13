﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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
    public int playerHealth;      //Player's current health   //TODO : Max health managment
    public bool isCarrying;             //Ball carrying toggle
   

    [Header("Local")]
    public bool isReceivingInputs;  //Used to disable inputs on Menu/etc ...

    public void ChangeHealth(InputField health)
    {
        string h = health.text;
        playerHealth = int.Parse(h);
        Debug.Log("Health changed");
    }

}