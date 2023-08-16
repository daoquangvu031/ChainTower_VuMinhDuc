using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretMachineGun : TurretBase
{
    public GameObject targetBot;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float attackInterval = 1.0f;
    public ParticleSystem swordParticleSystem;

    private float attackTimer = 0.0f;


    private void Update()
    {
        if (targetBot != null && targetBot.activeSelf)
        {
            if (attackTimer <= 0)
            {
                AttackBots(targetBot);   
                attackTimer = attackInterval;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
            transform.LookAt(targetBot.transform.position);
        }

    }
    public void AttackBots(GameObject bot)
    {
        if (bot != null && targetBot != null) // Kiểm tra cả bot và targetBot có khác null
        {
            
            swordParticleSystem.Play();

            if (bulletPrefab != null && bulletSpawnPoint != null)
            {
                GameObject bullet = Pool.GetBulletFromPool(); // Lấy đạn từ Bullet Pool
                if (bullet != null)
                {
                    bullet.transform.position = bulletSpawnPoint.position;
                    bullet.SetActive(true);

                    BulletMachineGun bulletMachineGun = bullet.GetComponent<BulletMachineGun>();
                    if (bulletMachineGun != null)
                    {
                        Vector3 direction = (bot.transform.position - bulletSpawnPoint.position).normalized;
                        bulletMachineGun.SetDirection(targetBot.transform);
                    }
                }
            }
        }
    }

    public override void SetTargetBot(GameObject bot)
    {
        targetBot = bot;
    }

    public void AttackBotsInZone(AttackZone attackZone)
    {
        foreach (GameObject bot in attackZone.botsInRange)
        {
            AttackBots(bot);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(Constant.TAG_TURRET))
    //    {
    //        TurretBase turret = other.GetComponent<TurretBase>();
    //        if (turret != null)
    //        {
    //            AddTurretToZone(turret);
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag(Constant.TAG_TURRET))
    //    {
    //        TurretBase turret = other.GetComponent<TurretBase>();
    //        if (turret != null)
    //        {
    //            RemoveTurretFromZone(turret);
    //        }
    //    }
    //}
}
