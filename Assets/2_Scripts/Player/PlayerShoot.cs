using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("REFERENCES")]
    public PlayerStats playerStats;
    public PlayerCall playerCall;
    public GameObject playerCollider;

    private SceneOverlord sceneOverlord;

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
        sceneOverlord = GameObject.Find("SceneGM").GetComponent<SceneOverlord>();
    }

    /// <summary>
    /// Called once every frame
    /// </summary>
    void Update()
    {
        if (sceneOverlord.isReceivingInputs)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ShootRocket();
            }
        }

        #region DEBUG
        if (DBG_aim)
        {
            LayerMask layerMask = ~LayerMask.GetMask("BLU", "RED", "SPE");
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Physics.Raycast(ray, out hit, 1000.0f, layerMask, QueryTriggerInteraction.Ignore);
            Debug.DrawLine(ray.origin, hit.point, new Color32(52, 152, 219, 255), 0.0f, true);
            Debug.DrawLine(playerFireOutputTransform.position, hit.point, new Color32(231, 76, 60, 255), 0.0f, true);
        }
        #endregion
    }

    /// <summary>
    /// Instantiante a rocket at gun position with correct rotation in order to reach player aiming point
    /// </summary>
    void ShootRocket()
    {
        LayerMask layerMask = ~LayerMask.GetMask("BLU", "RED", "SPE");
        RaycastHit hit;                                                                                 //Used to store raycast hit data
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));   //Define ray as player aiming point
        Physics.Raycast(ray, out hit, 1000.0f, layerMask, QueryTriggerInteraction.Ignore);              //Casts the ray
        Vector3 relativepos = hit.point - playerFireOutputTransform.position;                           //Get the vector to parcour
        Quaternion targetrotation = Quaternion.LookRotation(relativepos);                               //Get the needed rotation of the rocket to reach that point
        playerCall.Call_ShootRocket(playerFireOutputTransform.position, targetrotation, playerStats.playerTeam);
    }

}
