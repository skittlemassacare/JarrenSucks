using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class PlayerHealth : MonoBehaviour {
public int startingHealth;

public int currentHealth;

public Slider healthSlider;
public Image damageImage;
public float flashSpeed = 5f;
public Color flashColor = new Color(1f,0f,0f,0.1f);
private PlayerMovement playerMovement;
public AudioClip deathClip;



private Animator anim;
private AudioSource playerAudio;
private bool isDead;
private bool damaged;

private void Start()
{

}


void Awake()
{
    anim = GetComponent<Animator>();
    playerAudio = GetComponent<AudioSource>();
    playerMovement = GetComponent<PlayerMovement>();

    currentHealth = startingHealth;
}

// Update is called once per frame
void Update () {
    if (Input.GetKeyDown(KeyCode.F))
    {
        TakeDamage(5);
    }



if (damaged)
{
damageImage.color = flashColor;
}
else
{
//damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
}

damaged = false;
}

public void TakeDamage(int amount)
{
damaged = true;

currentHealth -= amount;

healthSlider.value = currentHealth;

playerAudio.Play();

if (currentHealth <= 0 && !isDead)
{
Death();
}
}


void Death()
{
isDead = true;

anim.SetTrigger("Die");

playerAudio.clip = deathClip;
playerAudio.Play();

playerMovement.enabled = false;
//Thread.Sleep(5000);
   
  SceneManager.LoadScene("Main menu");


}

}
