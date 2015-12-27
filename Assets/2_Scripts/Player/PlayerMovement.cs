using UnityEngine;

/// <summary>
/// TODO: Comment that shit because its outdated
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("STATS")]
    public float groundspeed = 3.0f;		//Normal moving speed multiplier
    public float airspeed = 1.0f;			//Air acceleration multiplier
    public float maxairacceleration = 1.0f;	//Maximal acceleration in the air
    public float jumpheight = 1.2f;			//Jump height
    public float smoothfactor = 1.0f;       //Movement smoothing factor
    public int PossibleJumps = 1;           //Number of jumps

    #region DEBUG
    [Header("DEBUG")]
    public bool DBG_movements = false;
    public float DBG_time = 0.2f;
    #endregion

    //Private variables
    private bool isGrounded;    //Stores grounded state
    private int jumpsleft;		//Stores jumps left
    private Rigidbody PlayerRigidBody;

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        PlayerRigidBody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Called every x ms
    /// </summary>
    void FixedUpdate()
    {
        //PlayerRigidBody.AddForce(Physics.gravity);
    }

    /// <summary>
    /// Called once every frame
    /// </summary>
	void Update()
    {
        //Jump trigger
        if (Input.GetButtonDown("Jump"))
            Jump();

        SourceMove(new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")));   //Get inputs as vector);
    }

    /// <summary>
    /// Called every time the player enters collision
    /// </summary>
    /// <param name="col">Collision data</param>
    void OnCollisionEnter(Collision col)
    {
        ContactPoint contact = col.contacts[0];             //Get collision points
        if (Vector3.Dot(contact.normal, Vector3.up) > 0.1f) //Detect points position according to normal
        {
            jumpsleft = PossibleJumps;	//Gives jumps back
            isGrounded = true;          //Ground player
        }
    }

    /// <summary>
    /// Called every frame the player is in collision
    /// </summary>
    /// <param name="col">Collision data</param>
    void OnCollisionStay(Collision col)
    {
        ContactPoint contact = col.contacts[0];             //Get collision points
        if (Vector3.Dot(contact.normal, Vector3.up) > 0.1f) //Detect points position according to normal
        {
            isGrounded = true;  //Ground player
        }
    }

    /// <summary>
    /// Called every time the player exits collision
    /// </summary>
    /// <param name="col">Collision data</param>
    void OnCollisionExit(Collision col)
    {
        isGrounded = false; //Unground player
    }

    /// <summary>
    /// Moves like Jagger
    /// </summary>
    void SourceMove(Vector3 inputsvector)
    {
        Vector3 targetvelocity;                         //Velocity the player will try to reach on the ground
        Vector3 acceleration;                           //Acceleration the player is trying to add in the air
        Vector3 velocity = PlayerRigidBody.velocity;    //Current velocity of the player
        Vector3 velocitychange;                         //Acceleration needed to reach targetvelocity
        Vector3 projection;                             //Projection needed to rule air movement

        //INPUTS
        targetvelocity = inputsvector;

        targetvelocity = transform.TransformDirection(targetvelocity);  //Change orientation of targetvelocity to local
        acceleration = targetvelocity;                                  //Save orientation and inputs for air movement

        //SPEED MODIFIERS
        targetvelocity *= groundspeed;                                          //Ground speed modifiers applied
        targetvelocity = Vector3.ClampMagnitude(targetvelocity, groundspeed);   //Clamp to prevent "Crossing the square" on ground
        acceleration *= airspeed;                                               //Air speed modifiers applied
        acceleration = Vector3.ClampMagnitude(acceleration, airspeed);          //Clamp to prevent "Crossing the square" in air

        //GROUND MOVEMENT
        if (isGrounded)
        {

            velocitychange = (targetvelocity - velocity) / smoothfactor;        //Calculate change needed and smooths it
            velocitychange.y = 0.0f;                                            //No vertical change needed (Jumping does it)	
            PlayerRigidBody.AddForce(velocitychange, ForceMode.VelocityChange); //Move according to change needed

            #region DEBUG
            if (DBG_movements)
            {
                Debug.DrawLine(transform.position, transform.position + targetvelocity, Color.green, DBG_time, false);
                Debug.DrawLine(transform.position + velocity, transform.position + velocity + velocitychange, Color.red, DBG_time, false);
            }
            #endregion

            //AIR MOVEMENT
        }
        else
        {

            projection = Vector3.Project(velocity, acceleration);                   //Get projection of velocity/acceleration
            float proangle = Vector3.Angle(velocity, acceleration);                 //Get angle between velocity & acceleration
            if (projection.magnitude <= maxairacceleration || proangle > 90.0f)     //If normal is not over the limit or backwards
                PlayerRigidBody.AddForce(acceleration, ForceMode.VelocityChange);   //Move according to acceleration

            #region DEBUG
            if (DBG_movements)
            {
                Debug.DrawLine(transform.position, transform.position + acceleration, Color.red, DBG_time, false);
            }
            #endregion
        }

        #region DEBUG
        if (DBG_movements)
        {
            Debug.DrawLine(transform.position, transform.position + velocity, Color.magenta, DBG_time, false);
            Debug.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward, Color.yellow, DBG_time, false);
        }
        #endregion
    }

    /// <summary>
    /// Make the player jump if possible
    /// </summary>
    void Jump()
    {
        if (jumpsleft != 0)
        {
            Vector3 jumpdirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));    //Get inputs as vector
            jumpdirection = transform.TransformDirection(jumpdirection);    //Change orientation of jumpdirection to local
            jumpdirection.y = Mathf.Sqrt(2.0f * jumpheight * 9.81f);        //Calculate jump power needed to reach height
            jumpdirection.x *= groundspeed;                                 //Default ground speed
            jumpdirection.z *= groundspeed;                                 //Default ground speed
            PlayerRigidBody.velocity = jumpdirection;                       //Jump according to inputs
            jumpsleft--;                                                    //Remove one jump from stock
        }
    }

    
}