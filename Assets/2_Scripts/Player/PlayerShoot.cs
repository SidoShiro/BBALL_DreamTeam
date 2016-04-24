using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : MonoBehaviour
{
    [Header("References(Player)")]
    [SerializeField]
    private NetworkIdentity playerIdentity;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerCall playerCall;
    [SerializeField]
    private GameObject playerCollider;

    [Header("Settings")]
    [SerializeField]
    private int maxammo;
    [SerializeField]
    private float recoiltime;
    [SerializeField]
    private float reloadtime;

    //Local
    private float nextshottime;
    private int currentammo;
    private float nextreloadtime;

    #region DEBUG
    [Header("DEBUG")]
    public Transform playerFireOutputTransform;
    public Camera playerCamera;
    public bool DBG_aim = true;
    #endregion

    /// <summary>
    /// Called once when enabled
    /// </summary>
    void Start()
    {
        nextshottime = Time.time;
        currentammo = maxammo;
        playerCall.Call_UpdateAmmo(currentammo);
    }

    /// <summary>
    /// Called once every frame
    /// </summary>
    void Update()
    {
        if (playerStats.isReceivingInputs)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                TryShootRocket();
            }
        }

        ConstantReload();

        #region DEBUG
        if (DBG_aim)
        {
            LayerMask layerMask = ~LayerMask.GetMask("BLU", "RED", "SPE");
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out hit, 1000.0f, layerMask, QueryTriggerInteraction.Ignore))
            {
                Debug.DrawLine(ray.origin, hit.point, new Color32(52, 152, 219, 255), 0.0f, true);
                Debug.DrawLine(playerFireOutputTransform.position, hit.point, new Color32(231, 76, 60, 255), 0.0f, true);
            }
            else
            {
                Vector3 targetpos = ray.origin + ray.direction * 1000;
                Debug.DrawLine(ray.origin, targetpos, new Color32(52, 152, 219, 255), 0.0f, true);
                Debug.DrawLine(playerFireOutputTransform.position, targetpos, new Color32(231, 76, 60, 255), 0.0f, true);
            }
        }
        #endregion
    }

    private void ConstantReload()
    {
        float time = Time.time;
        if (currentammo < maxammo && nextreloadtime < time)
        {
            ++currentammo;
            nextreloadtime = time + reloadtime;
            playerCall.Call_UpdateAmmo(currentammo);
        }
    }

    /// <summary>
    /// Tries to shoot a rocket
    /// </summary>
    private void TryShootRocket()
    {
        float time = Time.time;
        if (nextshottime <= time && currentammo > 0)
        {
            ShootRocket();
            nextshottime = time + recoiltime;
            --currentammo;
            nextreloadtime = time + reloadtime;
            playerCall.Call_UpdateAmmo(currentammo);
        }
    }

    /// <summary>
    /// Instantiante a rocket at gun position with correct rotation in order to reach player aiming point
    /// </summary>
    private void ShootRocket()
    {
        LayerMask layerMask = ~LayerMask.GetMask("BLU", "RED", "SPE");
        RaycastHit hit;                                                                                 //Used to store raycast hit data
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));   //Define ray as player aiming point
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 relativepos = hit.point - playerFireOutputTransform.position;   //Get the vector to parcour
            Quaternion targetrotation = Quaternion.LookRotation(relativepos);       //Get the needed rotation of the rocket to reach that point
            playerCall.Call_ShootRocket(playerFireOutputTransform.position, targetrotation, playerStats.playerTeam, playerIdentity);
        }
        else
        {
            Vector3 targetpos = ray.origin + ray.direction * 1000;
            Vector3 relativepos = targetpos - playerFireOutputTransform.position;   //Get the vector to parcour
            Quaternion targetrotation = Quaternion.LookRotation(relativepos);       //Get the needed rotation of the rocket to reach that point
            playerCall.Call_ShootRocket(playerFireOutputTransform.position, targetrotation, playerStats.playerTeam, playerIdentity);
        }
    }

}
