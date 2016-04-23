using UnityEngine;
using System;

/// <summary>
/// Enum used for team affectation
/// </summary>
public enum Team
{
    SPE = 0,
    BLU = 1,
    RED = 2
}

/// <summary>
/// Enum used for damage affectation
/// </summary>
public enum DamageType
{
    Hazard = 0,
    Explosion = 1
}

/// <summary>
/// This static class is used to define global types and methods in the project
/// Its uses are but not limited to :
/// Collision layering
/// Render layering
/// Custom enumerations (see above)
/// ...
/// </summary>
public class m_Custom : MonoBehaviour
{
    private static LayerMask _layerMaskBLU = LayerMask.GetMask("Default", "RED");               //Collision layer of BLU team
    private static LayerMask _layerMaskRED = LayerMask.GetMask("Default", "BLU");               //Collision layer of RED team
    private static LayerMask _layerMaskRocket = LayerMask.GetMask("Default", "BLU", "RED");     //Collision layer of rockets

    private static LayerMask _layerMaskNOSPE = ~LayerMask.GetMask("NORENDER", "SPE");           //Rendering layer of RED team
    private static LayerMask _layerMaskWTSPE = ~LayerMask.GetMask("NORENDER");                  //Rendering layer of RED team
    
    /// <summary>
    /// Used to get the team color according to team (Currently hardcoded)
    /// </summary>
    /// <param name="team">Team to get the color from</param>
    /// <returns>Color of the team</returns>
    public static Color GetColorFromTeam(Team team)
    {
        switch (team)
        {
            case Team.BLU:
                return new Color32(52, 152, 219,255);

            case Team.RED:
                return new Color32(231, 76, 60,255);

            default:
                return new Color32(155, 89, 182,255);
        }
    }

    /// <summary>
    /// Get collision layer according to team
    /// </summary>
    /// <param name="team">Team to get the layer from</param>
    /// <returns>Collision layer of team</returns>
    public static LayerMask GetLayerFromTeam(Team team)
    {
        switch (team)
        {
            case Team.BLU:
                return layerMaskBLU;

            case Team.RED:
                return layerMaskRED;

            default:
                Debug.Log("SHOULD NOT HAVE HAPPENED: Player team not expected in m_Custom/GetLayerFromTeam");
                return 10;
        }
    }

    public static string GetBallSpawnTagFromTeam(Team team)
    {
        switch (team)
        {
            case Team.BLU:
                return "BLUBallRespawn";

            case Team.RED:
                return "REDBallRespawn";

            default:
                return "SPEBallRespawn";
        }
    }

    /// <summary>
    /// Used to generate random player names
    /// </summary>
    /// <param name="maxSize">Size of the guid</param>
    /// <returns>Random string of size maxSize</returns>
    public static string RandomGUID(int maxSize)
    {
        return Guid.NewGuid().ToString().Substring(0, maxSize);
    }

    #region GET/SET
    public static LayerMask layerMaskBLU
    {
        get
        {
            return _layerMaskBLU;
        }
    }
    public static LayerMask layerMaskRED
    {
        get
        {
            return _layerMaskRED;
        }
    }
    public static LayerMask layerMaskNOSPE
    {
        get
        {
            return _layerMaskNOSPE;
        }
    }
    public static LayerMask layerMaskWTSPE
    {
        get
        {
            return _layerMaskWTSPE;
        }
    }
    public static LayerMask layerMaskRocket
    {
        get
        {
            return _layerMaskRocket;
        }
    }
    #endregion
}
