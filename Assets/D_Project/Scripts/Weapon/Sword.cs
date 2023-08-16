using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public ParticleSystem swordParticleSystem;
    public int swordDamage = 10;


    public void OnTriggerStay(Collider other)   
    {
        if (other.CompareTag(Constant.TAG_BOT))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                bot.TakeDamage(swordDamage); 
                swordParticleSystem.Play();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                bot.TakeDamage(0);
                swordParticleSystem.Stop();
            }
        }
    }
}
