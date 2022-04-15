using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : BaseInput
{
    public static Player2 Instance;
    public Transform[] aimTarget; // the target where we aim to land the ball
    public float speed = 30f; // move speed
    float force = 13; // ball impact force
    public Transform player3D;

    public float h, v;
    protected Joystick joystick;

    bool hitting; // boolean to know if we are hitting the ball or not 

    public Transform ball; // the ball 
    Animator animator;

    Vector3 aimTargetInitialPosition; // initial position of the aiming gameObject which is the center of the opposite court

    ShotManager shotManager; // reference to the shotmanager component
    Shot currentShot; // the current shot we are playing to acces it's attributes

    public Button aimBtn_Left;
    public Button aimBtn_Mid;
    public Button aimBtn_Right;
    int aimNumber;
    public bool isReady = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<Player2>();
        }
    }

    private void Start()
    {
        
        animator = GetComponent<Animator>(); // referennce out animator
        //aimTargetInitialPosition = aimTarget[aimNumber].position; // initialise the aim position to the center( where we placed it in the editor )
        shotManager = GetComponent<ShotManager>(); // accesing our shot manager component 
        currentShot = shotManager.topSpin; // defaulting our current shot as topspin
        Button btnLeft = aimBtn_Left.GetComponent<Button>();
        Button btnMid = aimBtn_Mid.GetComponent<Button>();
        Button btnRight = aimBtn_Right.GetComponent<Button>();
        btnLeft.onClick.AddListener(OnclickBtnLeft);
        btnMid.onClick.AddListener(OnclickBtnMid);
        btnRight.onClick.AddListener(OnclickBtnRight);
        joystick = FindObjectOfType<Joystick>();
    }

    public override Vector2 GenerateInput()
    {
        if (isReady == true)
        {
            Debug.Log("adasdasdasd");
            return new Vector2
            {
                x = Input.GetAxis("Horizontal"),
                y = Input.GetAxis("Vertical")
                
            };
        }
        return new Vector2 { };

    }
    void Update()
    {
         h = Input.GetAxisRaw("Horizontal"); // get the horizontal axis of the keyboard
         v = Input.GetAxisRaw("Vertical"); // get the vertical axis of the keyboard

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(joystick.Horizontal * speed,rigidbody.velocity.y, joystick.Vertical * speed);

        if (Input.GetKeyDown(KeyCode.F))
        {
            hitting = true; // we are trying to hit the ball and aim where to make it land
            currentShot = shotManager.topSpin; // set our current shot to top spin
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false; // we let go of the key so we are not hitting anymore and this 
        }                    // is used to alternate between moving the aim target and ourself

        if (Input.GetKeyDown(KeyCode.E))
        {
            hitting = true; // we are trying to hit the ball and aim where to make it land
            currentShot = shotManager.flat; // set our current shot to top spin
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            hitting = false;
        }



        if (hitting)  // if we are trying to hit the ball
        {
            aimTarget[aimNumber].Translate(new Vector3(h, 0, 0) * speed * 2 * Time.deltaTime); //translate the aiming gameObject on the court horizontallly
        }


        if ((h != 0 || v != 0) && !hitting) // if we want to move and we are not hitting the ball
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); // move on the court
        }



    }

    public void  OnclickBtnLeft()
    {
        aimNumber = 0;
        Debug.Log("0000000000");
    }
    public void  OnclickBtnMid()
    {
        aimNumber = 1;
        Debug.Log("11111111111");
    }
    public void  OnclickBtnRight()
    {
        aimNumber = 2;
        Debug.Log("22222222222");
    }

    //Vector3 getAimBtnLeft()
    //{
    //    return aimTarget[0].position; // return the chosen target
    //}
    //Vector3 getAimBtnMid()
    //{
    //    return aimTarget[1].position;
    //}
    //Vector3 getAimBtnRight()
    //{
    //    return aimTarget[2].position;
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // if we collide with the ball 
        {
            Vector3 dir = aimTarget[aimNumber].position - transform.position; // get the direction to where we want to send the ball
            Debug.Log("aimTarget[aimNumber].position: " + aimTarget[aimNumber].position);
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            //add force to the ball plus some upward force according to the shot being played

            Vector3 ballDir = ball.position - transform.position; // get the direction of the ball compared to us to know if it is
            if (ballDir.x >= 0)                                   // on out right or left side 
            {
                animator.Play("forehand");                        // play a forhand animation if the ball is on our right
            }
            else                                                  // otherwise play a backhand animation 
            {
                animator.Play("backhand");
            }

            //aimTarget[aimNumber].position = aimTargetInitialPosition; // reset the position of the aiming gameObject to it's original position ( center)

        }
    }
}
