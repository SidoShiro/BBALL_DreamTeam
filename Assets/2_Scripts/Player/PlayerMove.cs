using UnityEngine;

/// <summary>
/// Player movement
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{

    [Header("References(Player)")]
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Rigidbody playerRigidBody;
    [SerializeField]
    private PlayerStats playerStats;

    [Header("MoveStats")]
    [SerializeField]
    private float groundspeed = 3.0f;		    //Normal moving speed multiplier
    [SerializeField]
    private float airspeed = 1.0f;			    //Air acceleration multiplier
    [SerializeField]
    private float maxairacceleration = 1.0f;    //Maximal acceleration in the air
    [SerializeField]
    private float jumpheight = 1.2f;			//Jump height
    [SerializeField]
    private float smoothfactor = 1.0f;          //Movement smoothing factor
    [SerializeField]
    private int PossibleJumps = 1;              //Number of jumps

    //Private variables
    private bool isGrounded;    //Stores grounded state
    private int jumpsleft;      //Stores jumps left

    #region DEBUG
    [Header("DEBUG")]
    public bool DBG_movements = false;
    public float DBG_time = 0.2f;
    #endregion

    public void SetGrounded(bool b)
    {
        isGrounded = b;
    }

/// <summary>
/// Called once every frame
/// </summary>
void Update()
    {
        Move();
    }

    /// <summary>
    /// Get player inputs and move accordingly (SPE have different movement)
    /// </summary>
    void Move()
    {
        Vector3 playerInputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        bool isControllable = playerStats.isReceivingInputs;

        if (playerStats.playerTeam != PlayerStats.Team.SPE)
        {
            if (isControllable)
            {
                if (Input.GetButtonDown("Jump"))
                    Jump(playerInputs);

                SourceMove(playerInputs);
            }
            else
            {
                SourceMove(Vector3.zero);
            }
        }
        else
        {
            if (isControllable)
            {
                SPEMove(playerInputs);
            }
        }
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
    /// Make the player jump if possible
    /// </summary>
    void Jump(Vector3 inputsvector)
    {
        if (jumpsleft != 0 && isGrounded)
        {
            inputsvector = transform.TransformDirection(inputsvector);  //Change orientation of jumpdirection to local
            inputsvector.y = Mathf.Sqrt(2.0f * jumpheight * 9.81f);     //Calculate jump power needed to reach height
            inputsvector.x *= groundspeed;                              //Default ground speed
            inputsvector.z *= groundspeed;                              //Default ground speed
            playerRigidBody.velocity = inputsvector;                    //Jump according to inputs
            jumpsleft--;                                                //Remove one jump from stock
        }
    }

    /// <summary>
    /// Moves like Jagger
    /// </summary>
    void SourceMove(Vector3 inputsvector)
    {
        Vector3 velocity = playerRigidBody.velocity;    //Current velocity of the player
        Vector3 position = playerTransform.position;    //Current position of the player
        Vector3 targetvelocity;                         //Velocity the player will try to reach on the ground
        Vector3 acceleration;                           //Acceleration the player is trying to add in the air
        Vector3 velocitychange;                         //Acceleration needed to reach targetvelocity
        Vector3 projection;                             //Projection needed to rule air movement

        //INPUTS
        targetvelocity = inputsvector;

        targetvelocity = playerTransform.TransformDirection(targetvelocity);    //Change orientation of targetvelocity to local
        acceleration = targetvelocity;                                          //Save orientation and inputs for air movement

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
            playerRigidBody.AddForce(velocitychange, ForceMode.VelocityChange); //Move according to change needed

            #region DEBUG
            if (DBG_movements)
            {
                Debug.DrawLine(position, position + targetvelocity, Color.green, DBG_time, false);
                Debug.DrawLine(position + velocity, position + velocity + velocitychange, Color.red, DBG_time, false);
            }
            #endregion

            //AIR MOVEMENT
        }
        else
        {

            projection = Vector3.Project(velocity, acceleration);                       //Get projection of velocity/acceleration
            float projectionAngle = Vector3.Angle(velocity, acceleration);              //Get angle between velocity & acceleration
            if (projection.magnitude <= maxairacceleration || projectionAngle > 90.0f)  //If normal is not over the limit or backwards
                playerRigidBody.AddForce(acceleration, ForceMode.VelocityChange);       //Move according to acceleration

            #region DEBUG
            if (DBG_movements)
            {
                Debug.DrawLine(position, position + acceleration, Color.red, DBG_time, false);
            }
            #endregion
        }

        #region DEBUG
        if (DBG_movements)
        {
            Debug.DrawLine(position, position + velocity, Color.magenta, DBG_time, false);
            Debug.DrawLine(position + transform.up * 0.5f, position + transform.up * 0.5f + transform.forward, Color.yellow, DBG_time, false);
        }
        #endregion
    }

    void SPEMove(Vector3 inputsvector)
    {
        if (Input.GetButton("Jump") && !Input.GetButton("Crouch"))
            playerTransform.position += Vector3.up * Time.deltaTime * 5.0f;

        if (Input.GetButton("Crouch") && !Input.GetButton("Jump"))
            playerTransform.position -= Vector3.up * Time.deltaTime * 5.0f;

        playerTransform.position += inputsvector.x * Time.deltaTime * 5.0f * playerTransform.right;
        playerTransform.position += inputsvector.z * Time.deltaTime * 5.0f * playerTransform.forward;
    }
}
