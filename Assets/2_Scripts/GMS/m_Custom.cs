using UnityEngine;

public class m_Custom : MonoBehaviour
{
    private static LayerMask _layerMaskBLU = LayerMask.GetMask("Default", "RED");
    private static LayerMask _layerMaskRED = LayerMask.GetMask("Default", "BLU");

    private static LayerMask _layerMaskNOSPE = ~LayerMask.GetMask("NORENDER", "SPE");
    private static LayerMask _layerMaskWTSPE = ~LayerMask.GetMask("NORENDER");

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
