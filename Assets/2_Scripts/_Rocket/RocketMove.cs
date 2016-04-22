using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class RocketMove : NetworkBehaviour
{
    [Header("References(Rocket)")]
    [SerializeField]
    private float moveSpeed = 20.0f;
    [SerializeField]
    private GameObject rocketModel;
    [SerializeField]
    private GameObject rocketExplosion;
    [SerializeField]
    private GameObject rocketBits;
    [SerializeField]
    private ParticleSystem rocketBitsPs;
    [SerializeField]
    private GameObject rocketSmoke;
    [SerializeField]
    private ParticleSystem rocketSmokePs;

    //Properties
    [SyncVar]
    public NetworkIdentity ownerIdentity;// { get; set; }
    [SyncVar]
    public Team rocketTeam;
    [SyncVar]
    public Quaternion rocketRotation;

    //Local
    [SerializeField]
    private Transform rocketTransform;
    private ParticleSystem.EmissionModule em1;
    private ParticleSystem.EmissionModule em2;
    private LayerMask layerMask;

    #region DEBUG
    [Header("DEBUG")]
    public bool DBG_Trail = false;
    public bool DBG_Explosion = false;
    public float DBG_time_trail = 1.0f;
    public float DBG_time_explosion = 1.0f;
    #endregion

    /// <summary>
    /// Called once when script is enabled
    /// </summary>
    void Start()
    {
        em1 = rocketBitsPs.emission;
        em2 = rocketSmokePs.emission;
        rocketTransform.rotation = rocketRotation;  //hack Fix Client bug
        Destroy(gameObject, 10.0f);
        layerMask = m_Custom.layerMaskRocket;
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
        if (isServer && Physics.Linecast(rocketTransform.position, rocketTransform.position + movediff, out hit, layerMask, QueryTriggerInteraction.Ignore))
        {
            Rpc_Explode(hit.point);
        }
        rocketTransform.position += movediff;

        #region DEBUG
        if (DBG_Trail)
        {
            DebugExtension.DebugArrow(rocketTransform.position, movediff, new Color32(126, 52, 157, 255), DBG_time_trail, true);
        }
        #endregion
    }

    [ClientRpc]
    void Rpc_Explode(Vector3 explosionpos)
    {
        //Explosion
        GameObject explosion = (GameObject)Instantiate(rocketExplosion, explosionpos, Quaternion.identity);
        Destroy(explosion, 1.0f);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerCall>().Call_ExplosionDamage(explosionpos, rocketTeam, ownerIdentity);
        }

        //Trail
        rocketBits.transform.parent = null;
        rocketSmoke.transform.parent = null;
        em1.enabled = false;
        em2.enabled = false;
        Destroy(rocketBits, 0.4f);
        Destroy(rocketSmoke, 1.0f);

        #region DEBUG
        if (DBG_Explosion)
        {
            DebugExtension.DebugWireSphere(explosionpos, Color.blue, 0.05f, DBG_time_explosion, true);
            DebugExtension.DebugPoint(explosionpos, Color.red, 0.05f, DBG_time_explosion, true);
            DebugExtension.DebugWireSphere(explosionpos, Color.green, 2.0f, DBG_time_explosion, true);
        }
        #endregion

        Destroy(gameObject);    //Destroys the rocket and everything still attached to it
    }
}
