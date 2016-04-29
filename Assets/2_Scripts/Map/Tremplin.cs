using UnityEngine;
using System.Collections;

public class Tremplin : MonoBehaviour {

    public int force;

    void OnCollisionEnter(Collision col)
    {

        col.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.VelocityChange);
        col.transform.GetComponent<Rigidbody>().AddForce(Vector3.back * force * 10, ForceMode.VelocityChange);
    }
}
