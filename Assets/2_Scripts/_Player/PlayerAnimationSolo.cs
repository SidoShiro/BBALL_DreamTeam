using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerAnimationSolo : NetworkBehaviour {

    #region Attributs
    [Header("Attributs")]
    [SerializeField]
    private Rigidbody playerRigidbody;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private NetworkAnimator playerNetworkAnimator;
    #endregion

    #region Animations
    [Header("Animations")]
    [SerializeField]
    private Motion idleMotion;
    [SerializeField]
    private Motion forwardMotion;
    [SerializeField]
    private Motion backwardMotion;
    [SerializeField]
    private Motion leftMotion;
    [SerializeField]
    private Motion rightMotion;

    #endregion

    #region States
    enum State
    {
        IDLE, FORWARD, BACKWARD, LEFT, RIGHT
    }
    #endregion

    private State state;

    private float axis_x;
    private float axis_z;

    #region State Functions
    private void SetToIdle()
    {
        state = State.IDLE;
        
        
    }
    private void SetToForward()
    {
        state = State.FORWARD;
    }
    private void SetToBackward()
    {
        state = State.BACKWARD;
    }
    private void SetToRight()
    {
        state = State.RIGHT;
    }
    private void SetToLeft()
    {
        state = State.LEFT;
    }
    #endregion
    // Use this for initialization
    void Start ()
    {
        state = State.IDLE;

        //axis_x = playerRigidbody.velocity.normalized.x;
        //axis_z = playerRigidbody.velocity.normalized.z;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //axis_x = playerRigidbody.velocity.normalized.x;
        //axis_z = playerRigidbody.velocity.normalized.z;

        //playerAnimator.SetFloat("Axis_X", axis_x);
        //playerAnimator.SetFloat("Axis_Z", axis_z);
        //playerAnimator.SetFloat("Animator_Speed", Mathf.Max(Mathf.Abs(axis_x), Mathf.Abs(axis_z)));

    }
}
