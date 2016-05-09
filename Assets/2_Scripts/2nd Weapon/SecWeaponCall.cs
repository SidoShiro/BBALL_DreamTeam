using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


/// <summary>
/// Ce script gere tous les calls destines aux armes secondaires. 
/// </summary>
public class SecWeaponCall : NetworkBehaviour {

	public enum SecWeapon { None, secTest, secSnipar, secGrenadeLauncher};


    [Header("References : Secondary Weapons")]
    #region References : Secondary Weapons
    [SerializeField]
    private SecWeapon_Test secWeapon_Test;
    #endregion

    /// <summary>
    /// Arme secondaire actuelle.
    /// </summary>
    [SyncVar]
	private SecWeapon currentSecWeapon;

	// Use this for initialization
	void Start () 
	{
		currentSecWeapon = SecWeapon.None;
	}
	

    #region Fonctions privees

    private void KillCurrentSecWeapon()
    {
        if (currentSecWeapon == SecWeapon.secTest)
        {
            secWeapon_Test.OnKill();
        }
    }

    private void SpawnNewSecWeapon()
    {
        if (currentSecWeapon == SecWeapon.secTest)
        {
            secWeapon_Test.OnSpawn();
        }
    }

    #endregion

    #region Calls
    /// <summary>
    /// Permet de remplacer l'arme secondaire actuelle par une autre.
    /// </summary>
    /// <param name="sec_weapon_type"></param>
    public void SecCall_SwitchCurrentWeapon(SecWeapon sec_weapon_type) {

		if (sec_weapon_type != currentSecWeapon)
        {
            KillCurrentSecWeapon();
            currentSecWeapon = sec_weapon_type;
            SpawnNewSecWeapon();
		}
	}

    /// <summary>
    /// Appelle un coup de feu de l'arme secondaire actuelle.
    /// </summary>
    public void SecCall_Fire()
    {
        if (currentSecWeapon == SecWeapon.secTest)
        {
            secWeapon_Test.CallFire();
        }
    }
    
    /// <summary>
    /// Appelle un rechargement de l'arme secondaire actuelle.
    /// </summary>
    public void SecCall_Reload()
    {
        if (currentSecWeapon == SecWeapon.secTest)
        {
            secWeapon_Test.CallReload();
        }
    }
    #endregion


}
