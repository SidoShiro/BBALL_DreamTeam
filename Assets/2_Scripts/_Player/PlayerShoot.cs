using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    [Header("References(Player)")]
    #region References(Player)
    [SerializeField]
    private NetworkIdentity playerIdentity;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerCall playerCall;
    [SerializeField]
    private GameObject playerCollider;
    #endregion

    [Header("References(Interface)")]
    #region References(Interface)
    [SerializeField]
    private Image PrimaryReloadImage;
    [SerializeField]
    private Image PrimaryReloadBGImage;
    #endregion

    [Header("Settings")]
    #region Settings
    [SerializeField]
    private float f_reloadtime;
    #endregion

    //Locals
    private bool isAbleToShootPrimary;
    private bool isAbleToShootSecondary;
    private string secondary = "None";

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
        isAbleToShootPrimary = true;
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
                TryShootPrimary();
            }
        }

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

    /// <summary>
    /// Tries to shoot a rocket
    /// </summary>
    private void TryShootPrimary()
    {
        if (isAbleToShootPrimary)
        {
            StartCoroutine(ReloadPrimary());
            ShootPrimary();
        }
    }

    private void TryShootSecondary()
    {
        if (isAbleToShootSecondary)
        {
            if (secondary == "Sniper")
            {
                ShootSniper();
            }
            else if (secondary == "Rifle")
            {
                //ShootRifle()
            }
            else
            {
                //ShootGrenade()
            }
        }
    }

    /// <summary>
    /// Prevent shooting and triggers reload animations
    /// </summary>
    /// <returns></returns>
    IEnumerator ReloadPrimary()
    {
        //Starts reload animation and prevent shooting
        isAbleToShootPrimary = false;
        IEnumerator animation = ReloadPrimaryAnimate();
        PrimaryReloadImage.fillAmount = 1.0f;
        PrimaryReloadBGImage.fillAmount = 1.0f;
        StartCoroutine(animation);

        yield return new WaitForSeconds(f_reloadtime);

        //Ends animation and allow shooting
        isAbleToShootPrimary = true;
        PrimaryReloadImage.fillAmount = 0.0f;
        PrimaryReloadBGImage.fillAmount = 0.0f;
        StopCoroutine(animation);
    }

    /// <summary>
    /// Animate reload images
    /// </summary>
    /// <returns></returns>
    IEnumerator ReloadPrimaryAnimate()
    {
        while (!isAbleToShootPrimary)
        {
            PrimaryReloadImage.fillAmount -= 1.0f / f_reloadtime * Time.deltaTime;
            PrimaryReloadBGImage.fillAmount -= 1.0f / f_reloadtime * Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Instantiante a rocket at gun position with correct rotation in order to reach player aiming point
    /// </summary>
    private void ShootPrimary()
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

    private void ShootSniper()
    {
        LayerMask layerMask = ~LayerMask.GetMask("BLU", "RED", "SPE");
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hit, layerMask) && hit.GetType() == typeof(Player))
        {
            //IF hit.Layer == ennemy_layer
            //APPLY DMGS
            //Display Hitmarker
        }
        isAbleToShootSecondary = false;
        secondary = "None";

    }

}
