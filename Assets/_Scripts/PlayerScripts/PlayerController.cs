using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0f;
    public Rigidbody2D rigidbody;
    public SpriteRenderer sr;
    public Sprite back_idle;
    public Sprite down_idle;
    public Sprite right_idle;
    public Sprite left_idle;
    public bool canBeHit = true;

    private Animator animator;
    private bool cantMove = false;
    private float cantMoveTimer = 0;
    Vector2 AxisInput;



    // Use this for initialization
    void Start () {
        rigidbody.freezeRotation = true;
        animator = this.GetComponent<Animator>();
        animator.speed = 2;
        rigidbody = GetComponent<Rigidbody2D>();
    }


    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rigidbody.AddForce(movement * speed);
    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetAxis("360_Triggers") != 0)
        {
            if(Input.GetAxis("360_Triggers") >= 0.001)
            {
                Debug.Log("Left Trigger");
            } 
            if(Input.GetAxis("360_Triggers") <= -0.001)
            {
                Debug.Log("Right Trigger");
            }
            
        }
        
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        animator.enabled = false;

        if (cantMove != true)
        {
            if ((verticalAxis > 0 && verticalAxis > horizontalAxis) || Input.GetKey(KeyCode.W))
            {
                animator.enabled = true;
                animator.SetInteger("Direction", 0);
                sr.sprite = back_idle;
            }
            if ((verticalAxis < 0 && verticalAxis < horizontalAxis) || Input.GetKey(KeyCode.S))
            {
                animator.enabled = true;
                animator.SetInteger("Direction", 2);
                sr.sprite = down_idle;
            }
            if ((horizontalAxis < 0 && horizontalAxis < verticalAxis) || Input.GetKey(KeyCode.A))
            {
                animator.enabled = true;
                animator.SetInteger("Direction", 3);
                sr.sprite = left_idle;
            }
            if ((horizontalAxis > 0 && horizontalAxis > verticalAxis) || Input.GetKey(KeyCode.D))
            {
                animator.enabled = true;
                animator.SetInteger("Direction", 1);
                sr.sprite = right_idle;
            }

            Vector2 AxisInput = (new Vector2(horizontalAxis, verticalAxis));

            rigidbody.velocity = AxisInput * speed;
        } else {
            toggleCantMove();
        }


    }

    public void getKnockedBack(Vector2 knockBackDirection)
    {
        cantMove = true;
        canBeHit = false;
        cantMoveTimer = 60;    
            rigidbody.AddForce(knockBackDirection * 20, ForceMode2D.Impulse);  
    }

    void toggleCantMove()
    {
        if (cantMoveTimer > 0 && cantMove == true)
        {
            cantMoveTimer--;
            if (cantMoveTimer < 30) {
                rigidbody.velocity = Vector2.zero;
            }
        } else {
            cantMove = false;
            canBeHit = true;
        }
        
    }

    
}
