using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerAnimation : NetworkBehaviour {

    #region Attributs
    [SerializeField]
    private Rigidbody playerRigidbody;
    [SerializeField]
    private Animator playerAnimator;
    #endregion

    private float axis_x;
    private float axis_z;

    // Use this for initialization
    void Start ()
    {
        axis_x = playerRigidbody.velocity.normalized.x;
        axis_z = playerRigidbody.velocity.normalized.z;
	}
	
	// Update is called once per frame
	void Update ()
    {
        axis_x = playerRigidbody.velocity.normalized.x;
        axis_z = playerRigidbody.velocity.normalized.z;

        playerAnimator.SetFloat("Axis_X", axis_x);
        playerAnimator.SetFloat("Axis_Z", axis_z);
        playerAnimator.SetFloat("Animator_Speed", Mathf.Max(Mathf.Abs(axis_x), Mathf.Abs(axis_z)));
    }
}
