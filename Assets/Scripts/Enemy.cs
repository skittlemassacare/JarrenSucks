using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


public class Enemy : MonoBehaviour {

    
    public LayerMask enemyMask;
    public float speed = 1;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;
    public Animator animator;
    private Rigidbody2D m_Rigidbody2D;
    Rigidbody2D rb;
    private float m_JumpForce = 10f;

    public bool isGrounded;
    /*
    private Transform m_GroundCheck;
    private LayerMask m_WhatIsGround;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;

    public UnityEvent OnLandEvent;
    */

    void Start()
    {
            System.Threading.Thread.Sleep(5000);
            myTrans = this.transform;
            myBody = this.GetComponent<Rigidbody2D>();
            SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
            myWidth = mySprite.bounds.extents.x;
            myHeight = mySprite.bounds.extents.y;
            rb = GetComponent<Rigidbody2D>();

            


    }
    /*
    private void Awake()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }
    */

    private void FixedUpdate()
    {

        StartCoroutine(ZaneIsGay());

        //Use this position to cast the isGrounded/isBlocked lines from
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        //Check to see if there's ground in front of us before moving forward
        //NOTE: Unity 4.6 and below use "- Vector2.up" instead of "+ Vector2.down"
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        //Check to see if there's a wall in front of us before moving forward
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f, enemyMask);

        //If theres no ground, turn around. Or if I hit a wall, turn around
        if (!isGrounded || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        //always move forward jump if stop
        
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
        animator.SetBool("isMoving", true);

        /*
           bool wasGrounded = m_Grounded;
           m_Grounded = false;

           // The slime is grounded if a circlecast to the groundcheck position hits anything designated as ground
           Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
           for (int i = 0; i < colliders.Length; i++)
           {
               if (colliders[i].gameObject != gameObject)
               {
                   m_Grounded = true;
                   if (!wasGrounded)
                       OnLandEvent.Invoke();
               }
           }
           */
        //if slime is touching ground jump and waite 10 seconds

       



    }

    IEnumerator ZaneIsGay()
    {
        if (isGrounded == true)
        {
            //m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            
            rb.AddForce(Vector2.up * m_JumpForce);
            yield return new WaitForSeconds(5);
            //animator.SetBool("theGoodSLimeJUmping", true);

        }
    }
}
