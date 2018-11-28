using UnityEngine;




[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private readonly string HorizontalAxisName = "Horizontal";
    [SerializeField] private readonly string VerticalAxisName = "Vertical";

    /// <summary>
    /// Current speed is the base speed + any outer influence like slow or speed boost
    /// </summary>
    [SerializeField] private float currentSpeed;
    /// <summary>
    /// Base speed determins the characters speed status. So if a slow or boost effect wears off we know what value to reset to. 
    /// </summary>
    [SerializeField] private float baseSpeed;
    /// <summary>
    /// The Transform which handels/has graphics 
    /// </summary>
	[SerializeField] private Transform CharacterGraphics;

    private  Animator heroAnimator;
    private Rigidbody2D playerRigBdy;

    public bool canPlayerMove;
    bool facingRigth;
    


    public float BaseSpeed
    {
        get
        {
            return baseSpeed;
        }
    }

    public float CurrentSpeed
    {
        get
        {

            return currentSpeed;
        }
        set
        {
            currentSpeed = value;
        }
    }

    public Vector2 Direction { get; set; }

    // Use this for initialization
    void Awake()
    {
        canPlayerMove = true;
        CurrentSpeed = BaseSpeed;
       
        playerRigBdy = GetComponent<Rigidbody2D>();
        heroAnimator = GetComponent<Animator>();
       
    }


    void FixedUpdate()
    {
        if (canPlayerMove)
        {
            WASDMovementPhysics(); // NOTE maybe not use Physics ? --> player is sliding alot 
            PlayRunAnim();
        }

        if (Direction.x > 0 && !facingRigth)
        {
            Flip();
        }
        else if (Direction.x < 0 && facingRigth)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlayerMove)
        {
            
            Direction = new Vector2(Input.GetAxisRaw(HorizontalAxisName), Input.GetAxisRaw(VerticalAxisName));
        }
    }

    public void Flip() // TODO update Flip() Method to use the sprite flip insted of scale *-1
    {
        facingRigth = !facingRigth;
        Vector3 theScale = CharacterGraphics.localScale;
        theScale.x *= -1;
        CharacterGraphics.localScale = theScale;
    }

    /// <summary>
    ///  WASDMovementPhysics() Moving player based on physics -> rigbdy2d.addforce. 
    /// </summary>
    private void WASDMovementPhysics()
    {
        playerRigBdy.AddForce(new Vector2(Direction.x * CurrentSpeed, Direction.y * CurrentSpeed));
    }


    private void PlayRunAnim()
    {
        if (Mathf.Abs(Direction.x) > 0 || Mathf.Abs(Direction.y) > 0)
        {
            heroAnimator.SetBool("Running", true);
        }
        else
        {
            heroAnimator.SetBool("Running", false);
        }

    }

}
