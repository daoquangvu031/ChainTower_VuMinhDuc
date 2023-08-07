using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TurretRocket : TurretBase
{
    public GameObject rocketPrefab;
    public Transform rocketSpawnPoint;
    public float attackInterval = 2.0f;

    public GameObject targetBot;
    private float attackTimer = 0.0f;

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
        if (bot != null && targetBot != null)
        {
            if (rocketPrefab != null && rocketSpawnPoint != null)
            {
                GameObject rocket = Pool.GetBulletFromPool();
                if (rocket != null)
                {
                    rocket.transform.position = rocketSpawnPoint.position;
                    rocket.SetActive(true);

                    BulletRocketTower rocketComponent = rocket.GetComponent<BulletRocketTower>();
                    if (rocketComponent != null)
                    {
                        // Thiết lập thông số cho rocket, chẳng hạn như hướng bay và mục tiêu
                        rocketComponent.SetTarget(targetBot.transform);
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
            AttackBot(bot);
        }
    }
}
