using UnityEngine;

public class m_Custom : MonoBehaviour
{
    public static LayerMask layerMaskBLU = LayerMask.GetMask("Default", "RED");
    public static LayerMask layerMaskRED = LayerMask.GetMask("Default", "BLU");

    public LayerMask GetLayerMask(PlayerStats.Team team)
    {
        switch (team)
        {
            case PlayerStats.Team.BLU:
                return layerMaskBLU;

            case PlayerStats.Team.RED:
                return layerMaskRED;

            default:
                return 10;
        }
    }
}
