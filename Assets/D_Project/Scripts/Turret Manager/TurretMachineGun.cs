using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretMachineGun : TurretBase
{
    private BulletPool bulletPool;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float attackInterval = 1.0f;
    public ParticleSystem swordParticleSystem;

    public GameObject targetBot;
    private float attackTimer = 0.0f;


    public void Start()
    {
        bulletPool = FindObjectOfType<BulletPool>();
    }

    private void Update()
    {
        if (targetBot != null && targetBot.activeSelf)
        {
            if (attackTimer <= 0)
            {
                AttackBot(targetBot);
                attackTimer = attackInterval;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
            transform.LookAt(targetBot.transform.position);
        }

    }
    public void AttackBot(GameObject bot)
    {
        if (bot != null && targetBot != null) // Kiểm tra cả bot và targetBot có khác null
        {
            
            swordParticleSystem.Play();

            if (bulletPrefab != null && bulletSpawnPoint != null)
            {
                GameObject bullet = bulletPool.GetBulletFromPool(); // Lấy đạn từ Bullet Pool
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
        // Gọi hàm tấn công cho các bot trong danh sách
        foreach (GameObject bot in attackZone.botsInRange)
        {
            AttackBot(bot);
        }
    }
}
