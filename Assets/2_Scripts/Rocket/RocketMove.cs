using UnityEngine;
using UnityEngine.Networking;

public class RocketMove : NetworkBehaviour
{
    [Header("STATS")]
    [SerializeField]
    private float moveSpeed = 20.0f;
    [SerializeField]
    private GameObject rocketModel;
    [SerializeField]
    private GameObject rocketTrail;
    [SerializeField]
    private GameObject rocketExplosion;

    [SyncVar]
    public PlayerStats.Team rocketTeam;

    [SyncVar]
    public Quaternion rocketRotation;

    [SerializeField]
    private Transform rocketTransform;

    [SerializeField]
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;

    #region DEBUG
    [Header("DEBUG")]
    public bool DBG_Trail = false;
    public bool DBG_Explosion = false;
    public float DBG_time = 1.0f;
    #endregion

    /// <summary>
    /// Called once when instantiated
    /// </summary>
    void Awake()
    {
        em = ps.emission;
        rocketTrail.SetActive(false);
    }

    /// <summary>
    /// Called once when script is enabled
    /// </summary>
    void Start()
    {
        rocketTransform.rotation = rocketRotation;  //TODO: Fix Client bug
        rocketTrail.SetActive(true);
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

        LayerMask layerMask = 10;
        switch (rocketTeam)
        {
            case PlayerStats.Team.BLU:
                layerMask = m_Custom.layerMaskBLU;
                break;

            case PlayerStats.Team.RED:
                layerMask = m_Custom.layerMaskRED;
                break;

            default:
                Debug.Log("SHOULD NOT HAVE HAPPENED: Player team not expected in RocketMove/Move");
                break;
        }

        if (Physics.Linecast(rocketTransform.position, rocketTransform.position + movediff, out hit, layerMask,QueryTriggerInteraction.Ignore))
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
        rocketTrail.transform.parent = null;    //Remove the smoketrail from being child of the rocket to prevent deletion
        Destroy(gameObject);                    //Destroys the rocket and everything still attached to it
        em.enabled = false;                     //Stops the trail from emitting more particles
        Destroy(rocketTrail, 1.0f);             //Destroys the trail (Once every particle disapeared)
        GameObject explosion = (GameObject)Instantiate(rocketExplosion, explosionpos, Quaternion.identity);
        Destroy(explosion, 1.0f);

        if (isServer)
        {
            NetworkServer.Destroy(gameObject);
        }

        if (isClient)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players)
            {
                player.GetComponent<PlayerCall>().Call_AddExplosionForce(explosionpos);
            }
        }

        #region DEBUG
        if (DBG_Explosion)
        {
            DebugExtension.DebugWireSphere(explosionpos, Color.blue, 0.05f, DBG_time * 10.0f, true);
            DebugExtension.DebugPoint(explosionpos, Color.red, 0.05f, DBG_time * 10.0f, true);
            DebugExtension.DebugWireSphere(explosionpos, Color.green, 2.0f, 10.0f, true);
        }
        #endregion
    }
}
