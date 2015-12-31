using UnityEngine;
using UnityEngine.Networking;

public class RocketMove : NetworkBehaviour
{
    [Header("STATS")]
    public float moveSpeed = 20.0f;
    public GameObject rocketModel;
    public GameObject rocketTrail;
    public GameObject rocketExplosion;

    [Header("LAYERMASKS")]
    public LayerMask layerMaskBLU;
    public LayerMask layerMaskRED;

    private Transform rocketTransform;

    #region DEBUG
    [Header("DEBUG")]
    public bool DBG_Trail = false;
    public bool DBG_Explosion = false;
    public float DBG_time = 1.0f;
    #endregion

    /// <summary>
    /// Called once when script is enabled
    /// </summary>
    void Start()
    {
        rocketTransform = gameObject.transform;
        Destroy(gameObject, 10.0f);
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Tries to move rocket forward, explodes if it fails
    /// </summary>
    void Move()
    {
        RaycastHit hit;
        Vector3 movediff = rocketTransform.forward * moveSpeed * Time.deltaTime;

        LayerMask layerMask = gameObject.layer;
        if (gameObject.layer == 11)
        {
            layerMask = layerMaskBLU;
        }
        else
        {
            layerMask = layerMaskRED;
        }

        if (Physics.Linecast(rocketTransform.position, rocketTransform.position + movediff, out hit, layerMask))
        {
            Explode(hit.point);
        }
        else
        {
            rocketTransform.position += movediff;
        }

        #region DEBUG
        if (DBG_Trail)
        {
            DebugExtension.DebugArrow(rocketTransform.position, movediff, new Color32(28, 188, 156, 255), DBG_time, true);
        }
        #endregion
    }

    /// <summary>
    /// Makes the rocket explode
    /// </summary>
    /// <param name="explosionpos">Position of the explosion</param>
    void Explode(Vector3 explosionpos)
    {
        if (isServer)
        {
            rocketTrail.transform.parent = null;    //Remove the smoketrail from being child of the rocket to prevent deletion
            Destroy(gameObject);                    //Destroys the rocket and everything still attached to it
            //Explosion
            ParticleSystem.EmissionModule em = rocketTrail.GetComponent<ParticleSystem>().emission;
            em.enabled = false;                     //Stops the trail from emitting more particles
            Destroy(rocketTrail, 1.0f);             //Destroys the trail (Once every particle disapeared)
            NetworkServer.Destroy(gameObject);
        }

        #region DEBUG
        if (DBG_Explosion)
        {
            DebugExtension.DebugWireSphere(explosionpos, Color.blue, 0.05f, DBG_time * 10.0f, true);
            DebugExtension.DebugPoint(explosionpos, Color.red, 0.05f, DBG_time * 10.0f, true);
        }
        #endregion
    }
}
