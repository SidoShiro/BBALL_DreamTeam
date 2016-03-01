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

    [SyncVar]
    public PlayerStats.Team rocketTeam;

    [SyncVar]
    public Quaternion rocketRotation;

    [SerializeField]
    private Transform rocketTransform;

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
        rocketTransform.rotation = rocketRotation;  //hack Fix Client bug
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

        if (Physics.Linecast(rocketTransform.position, rocketTransform.position + movediff, out hit, layerMask, QueryTriggerInteraction.Ignore))
        {
            Explode(hit.point);
        }
        else
        {
            rocketTransform.position += movediff;
            //trailrend.material.SetTextureOffset("_MainTex", new Vector2(trailrend.material.mainTextureOffset.x - movediff.magnitude / 10, 0.0f));
        }

        #region DEBUG
        if (DBG_Trail)
        {
            DebugExtension.DebugArrow(rocketTransform.position, movediff, new Color32(126, 52, 157, 255), DBG_time_trail, true);
        }
        #endregion
    }

    /// <summary>
    /// Makes the rocket explode
    /// </summary>
    /// <param name="explosionpos">Position of the explosion</param>
    void Explode(Vector3 explosionpos)
    {
        Destroy(gameObject);                    //Destroys the rocket and everything still attached to it
        GameObject explosion = (GameObject)Instantiate(rocketExplosion, explosionpos, Quaternion.identity);
        Destroy(explosion, 1.0f);

        if (isServer)
        {
            NetworkServer.Destroy(gameObject);
        }

        if (isClient)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerCall>().Call_AddExplosionForce(explosionpos);
                player.GetComponent<PlayerCall>().Call_ExplosionDamage(explosionpos, rocketTeam);
            }
        }

        #region DEBUG
        if (DBG_Explosion)
        {
            DebugExtension.DebugWireSphere(explosionpos, Color.blue, 0.05f, DBG_time_explosion * 10.0f, true);
            DebugExtension.DebugPoint(explosionpos, Color.red, 0.05f, DBG_time_explosion * 10.0f, true);
            DebugExtension.DebugWireSphere(explosionpos, Color.green, 2.0f, DBG_time_explosion * 10.0f, true);
        }
        #endregion
    }
}
