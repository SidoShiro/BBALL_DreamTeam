using UnityEngine;
using System.Collections;

public class ExplosionCall : MonoBehaviour {

    ParticleSystem particle;

    private ParticleSystem GetChild()
    {
        return gameObject.GetComponentInChildren<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {

        particle = GetChild();
	}
	

    public void Call_ParticlePlay()
    {
        particle.Play();
    }

}
