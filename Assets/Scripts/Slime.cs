using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Boo.Lang.Environments;
using UnityEngine;
using UnityEngine.Serialization;

public class Slime: MonoBehaviour
{
    //floats
    public float jumpHeight = 7f;
    public bool isGrounded;
    [FormerlySerializedAs("facingLef")] 
    public bool facingLeft = true;
    
 
    //ints
    public int health;
    
    //vars
    public Rigidbody2D rb;
    public Animator animator;
    
    public GameObject bloodEffect;

    public AudioSource slimeNoise;

    [FormerlySerializedAs("_dead")] [SerializeField]
    private bool slimeDead;
    
    //----------------------------------------------------------------------------\\
    
    private void Awake()
    {
      
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    
    //jump in random directoin if grounded


    private void Update()
    {

        if (health > 0)
        {
            int randomNumber = Random.Range(1, 100);

            if (isGrounded)
            {

                if (randomNumber <= 25)
                {

                    transform.Rotate(new Vector3(0, 180, 0));
                    facingLeft = !facingLeft;

                    if (randomNumber <= 50)
                    {
                        //Debug.Log("zane big gay");
                        //jump method

                        rb.velocity = new Vector3(0.15f * (facingLeft ? -1 : 1), 1, 0) * jumpHeight;
                        //change directions
                    }
                }
            }
        }
        else if(!slimeDead)
        {
            slimeDead = true;

            

            StartCoroutine(Example());
        }

       
        
    }

    IEnumerator Example()
    {
        animator.SetBool("IsDead", true);
        print(Time.time);
        yield return new WaitForSeconds(2);
        print(Time.time);
                    
        Destroy(gameObject);
    }
    //check to see if grounded
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!slimeDead)
        {

            if (col.gameObject.tag == "Ground")
            {
                animator.SetBool("isJumping", false);
                isGrounded = true;
                Debug.Log("**Grounded!!!**");
                //Play audio when slime hits ground
                slimeNoise.Play();

            }

            if (col.gameObject.tag == "greenSlime")
            {
                rb.velocity = new Vector3(0.15f * (facingLeft ? -1 : 1), 1, 0) * jumpHeight;
            }

        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (!slimeDead)
        {

            if (other.gameObject.tag == "Ground")
            {
                animator.SetBool("isJumping", true);
                isGrounded = false;
                Debug.Log("**NOT Grounded!!!**");

            }
        }
    }

    public void TakeDamage(int damage)
    {
       // Instantiate(bloodEffect, transform.position, Quaternion.identity);
        rb.AddForce(new Vector2(1000f, 1000f));
        health -= damage;
        Debug.Log("damage TAKEN !");
    }
    
    

}
