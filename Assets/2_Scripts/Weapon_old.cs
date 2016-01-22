using UnityEngine;
using System.Collections;

public class Weapon_old : MonoBehaviour {
    public Transform Eject;
    public float FireRate = 0.5f;
    public int EjectSpeed = 100;
    private float Nextfire = 0.0f;
    private bool Fullauto = false;
    public Object Ammo;
	//*
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && Time.time > Nextfire)
        {
            Nextfire = Time.time + FireRate;
            GameObject bullet = (GameObject)Instantiate(Ammo, Eject.position, Eject.rotation);
            Destroy(bullet, 1.0f);
        }
        if (Input.GetKeyDown("v"))
        {
            Fullauto = !Fullauto;
        }
        if (Fullauto)
        {
            FireRate = 0.10f;
        }
        else
        {
            FireRate = 1f;
        }

	
	}
    //*/
}
