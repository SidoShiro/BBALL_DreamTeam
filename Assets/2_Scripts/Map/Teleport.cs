using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public Vector3 position;

    void OnCollisionEnter(Collision col)
    {

        col.transform.GetComponent<Rigidbody>().position = position;
    }
}
