using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class PlayerAnimation : NetworkBehaviour {

    #region Attributs
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private NetworkAnimator playerNetworkAnimator;
    #endregion

    private float axis_x;
    private float axis_z;

    // Use this for initialization
    void Start ()
    {
        axis_x = Input.GetAxisRaw("Horizontal");
        axis_z = Input.GetAxisRaw("Vertical");

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isLocalPlayer)
        {
            axis_x = Input.GetAxisRaw("Horizontal");
            axis_z = Input.GetAxisRaw("Vertical");

            playerAnimator.SetFloat("Axis_X", axis_x);
            playerAnimator.SetFloat("Axis_Z", axis_z);
            playerAnimator.SetFloat("Animator_Speed", Mathf.Max(Mathf.Abs(axis_x), Mathf.Abs(axis_z)));
        }
        

    }
}
