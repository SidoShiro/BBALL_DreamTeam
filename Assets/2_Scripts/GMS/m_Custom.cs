using UnityEngine;

public class m_Custom : MonoBehaviour
{
    public static LayerMask layerMaskBLU = LayerMask.GetMask("Default", "RED");
    public static LayerMask layerMaskRED = LayerMask.GetMask("Default", "BLU");
}
