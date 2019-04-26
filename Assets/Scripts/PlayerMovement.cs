using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour {

[SerializeField] private TrailRenderer m_DisableTrail;
public CharacterController2D Controller;
public float runSpeed = 40f;
float horizontalMove = 0f;
bool jump = false;
public Animator animator;
bool crouch = false;
bool sprint = false;
bool attack = false;

public Transform attackPos;
public LayerMask whatIsEnemies;
public float attackRange;
public int damage;
    public AudioClip jumpSound;
    AudioSource audioSource;


// Use this for initialization
void Start () {

}

// Update is called once per frame
void Update () {
horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
if (Input.GetButtonDown("Jump"))
{
//audioSource.PlayOneShot(jumpSound, 1f);
jump = true;
animator.SetBool("IsJumping", true);
}
if (Input.GetButtonDown("Fire1"))  
{
attack = true;
animator.SetBool("Attack", true);

Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
for (int i = 0; i < enemiesToDamage.Length; i++)
{
    enemiesToDamage[i].GetComponent<Slime>().TakeDamage(damage);
}

}
if (Input.GetButtonUp("Fire1"))
{
attack = false;
animator.SetBool("Attack", false);
}

if (Input.GetButtonDown("Crouch"))
{
crouch = true;
}else if  (Input.GetButtonUp("Crouch"))
{
crouch = false;
}
if (Input.GetButtonDown("Sprint"))
{
sprint = true;

}
else sprint = false;
} 
public void OnLanding()
{
m_DisableTrail.enabled = false;
animator.SetBool("IsJumping", false);
}

private void FixedUpdate()
{
Controller.Move(horizontalMove*Time.fixedDeltaTime, crouch, jump);
jump = false;
}

void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(attackPos.position, attackRange);
}

}
