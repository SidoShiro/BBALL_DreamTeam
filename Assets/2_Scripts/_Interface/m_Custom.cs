using UnityEngine;
using System;


public enum Team
{
    SPE = 0,
    BLU = 1,
    RED = 2
}

public enum DamageType
{
    Hazard,
    Explosion
}

public class m_Custom : MonoBehaviour
{
    private static LayerMask _layerMaskBLU = LayerMask.GetMask("Default", "RED");
    private static LayerMask _layerMaskRED = LayerMask.GetMask("Default", "BLU");

    private static LayerMask _layerMaskNOSPE = ~LayerMask.GetMask("NORENDER", "SPE");
    private static LayerMask _layerMaskWTSPE = ~LayerMask.GetMask("NORENDER");

    private static LayerMask _layerMaskRocket = LayerMask.GetMask("Default", "BLU", "RED");

    public static Color GetColorFromTeam(int team)
    {
        switch (team)
        {
            case 1:
                return new Color32(52, 152, 219, 255);

            case 2:
                return new Color32(231, 76, 60, 255);

            default:
                return new Color32(155, 89, 182, 255);
        }
    }

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
