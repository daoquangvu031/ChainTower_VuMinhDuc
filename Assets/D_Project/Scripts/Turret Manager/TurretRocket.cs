﻿using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            Vector3 directionToTarget = targetBot.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
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

                    BulletRocketTower bulletRocket = rocket.GetComponent<BulletRocketTower>();
                    if (bulletRocket != null)
                    {
                        bulletRocket.SetTarget(targetBot.transform);
                        bulletRocket.baseDamage += addMoreDamage;

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
