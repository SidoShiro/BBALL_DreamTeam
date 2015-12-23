using UnityEngine;

public class PlayerColliderFixer : MonoBehaviour
{
    public Collider PlayerCollider;

    /// <summary>
    /// Triggers at the end of each frame
    /// </summary>
    void LateUpdate()
    {
        FixCollider();
    }

    /// <summary>
    /// Fixes the collider in world space by resetting its position to identity
    /// </summary>
    void FixCollider()
    {
        PlayerCollider.transform.rotation = Quaternion.identity;
    }
}
