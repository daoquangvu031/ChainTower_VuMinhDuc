using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(Constant.TAG_BOT))
    //    {
    //        // Kiểm tra xem collider có tag "Bot" hay không
    //        Player player = GetComponentInParent<Player>();
    //        if (player != null)
    //        {
    //            player.TakeDamage(swordDamage);
    //            swordParticleSystem.Play();
    //        }
    //    }
    //}
    public ParticleSystem swordParticleSystem;
    public int swordDamage = 10; // Damage kiếm

    // Khi kiếm va chạm với bot
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                bot.TakeDamage(swordDamage); // Gây sát thương liên tục dựa vào thời gian trong mỗi frame
                swordParticleSystem.Play();
            }
        }
    }

    // Nếu kiếm không còn va chạm với bot, thì dừng sát thương
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                // Truyền 0 vào hàm TakeDamage để ngừng sát thương
                bot.TakeDamage(0);
                swordParticleSystem.Stop();
            }
        }
    }
}
