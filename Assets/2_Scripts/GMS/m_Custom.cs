using UnityEngine;

public class m_Custom : MonoBehaviour
{
    private static LayerMask _layerMaskBLU = LayerMask.GetMask("Default", "RED");
    private static LayerMask _layerMaskRED = LayerMask.GetMask("Default", "BLU");

    private static LayerMask _layerMaskNOSPE = ~LayerMask.GetMask("NORENDER", "SPE");
    private static LayerMask _layerMaskWTSPE = ~LayerMask.GetMask("NORENDER");

    public static LayerMask GetLayerFromTeam(PlayerStats.Team team)
    {
        switch (team)
        {
            case PlayerStats.Team.BLU:
                return layerMaskBLU;

            case PlayerStats.Team.RED:
                return layerMaskRED;

            default:
                Debug.Log("SHOULD NOT HAVE HAPPENED: Player team not expected in m_Custom/GetLayerFromTeam");
                return 10;
        }
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
    #endregion
}
