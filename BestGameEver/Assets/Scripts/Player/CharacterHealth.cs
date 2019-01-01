using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CharacterHealth : MonoBehaviour//https://unity3d.com/fr/learn/tutorials/projects/survival-shooter/player-health
{
    public int maxHealth = 2;
    public int currentHealth=1;
    
    //public AudioClip deathClip;             //The audio clip to play when the player dies.
    //bool isDammaged=false;                  //je sais pas si on need.
    bool isHealed=false;                    //je sais pas s'il faut le faire public
    bool isDead = false;

    public Sprite heartSprites;              
    public Image healthBar;                  //est assignée à l'image du canvas dans l'inspecteur.
    //Animator anim;                          // Reference to the Animator component.j'ai copié ça je suis pas certain de comment ça marche.
    //AudioSource playerAudio;                // Reference to the AudioSource component.
    PlayerController playerController;          // Reference to the player's movement.
    //PlayerShooting playerShooting;          // Reference to the PlayerShooting script.


    void Start ()
    {
        currentHealth = maxHealth;
        heartSprites = Resources.Load("Health/" + "Health" + maxHealth + "_" + currentHealth, typeof(Sprite)) as Sprite; // permet d'aller chercher le sprite voulu dans le dossier "Resources".
        healthBar.sprite = heartSprites;
        playerController = this.GetComponent<PlayerController>();

    }

    void Updtae()
    {
        // le probleme c'est que je n'ai rien qui update ma vie encore.
        //heartSprites = Resources.Load("Health" + maxHealth + "_" + currentHealth, typeof(Sprite)) as Sprite; // permet d'aller chercher le sprite voulu dans le dossier "Resources".
        //healthBar.sprite = heartSprites;

    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        heartSprites = Resources.Load("Health/" + "Health" + maxHealth + "_" + currentHealth, typeof(Sprite)) as Sprite; // permet d'aller chercher le sprite voulu dans le dossier "Resources".
        healthBar.sprite = heartSprites;

        // Play the hurt sound effect.
        //playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void GetHealed()
    {
        if (currentHealth < maxHealth && isHealed)
        {
            Heal();

        }
	}

    void Death()
    {
        isDead = true; //pour ne pas re-entrer dans le if du deçus.

        currentHealth = 0;

        Debug.Log("Je Suis Dead");

        // Tell the animator that the player is dead.
        //anim.setTrigger("Die");// bon on peut le call isDying, ça dépend de tes triggers d'animation ;)

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        // Turn off playerControler and playerAttack scripts.
        playerController.enabled = false; //est-ce que j'ai écrit ça juste?
        //playerAttack.enabled = false; //ce script n'existe pas encore.          

    }

    void Heal ()
    {
        isHealed = false; //pour ne pas retourner dans le if.

        currentHealth = maxHealth;
        heartSprites = Resources.Load("Health/" + "Health" + maxHealth + "_" + currentHealth, typeof(Sprite)) as Sprite; // permet d'aller chercher le sprite voulu dans le dossier "Resources".
        healthBar.sprite = heartSprites;

        //anim.setTrigger("Drinking"); //bref faut deja creer l'animation.

        // Turn off playerControler and playerAttack scripts.
        //playerController.enabled = false; //On ne veut pas vraiment desactivé le playerController pour toujours, seulement le temps qu'il se soigne.
        //playerAttack.enabled = false; //ce script n'existe pas encore.  

    }
}
