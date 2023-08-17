using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretLaser : TurretBase
{
    [SerializeField] LayerMask botLayerMask;

    public int damage;
    public GameObject laserPrefab;
    public Transform bulletSpawnPoint;

    private GameObject currentLaser;
    private bool isDamaging = false;
    private float damageCooldownTimer = 0f;
    private float damageCooldownDuration = 1.5f;


    public override void SetTargetBot(GameObject bot)
    {
        if (bot != null)
        {
            if (currentLaser == null)
            {
                currentLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            }

            LineRenderer lineRenderer = currentLaser.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, bulletSpawnPoint.position);
                lineRenderer.SetPosition(1, bot.transform.position);
            }

            ParticleSystem particleSystem = currentLaser.GetComponent<ParticleSystem>();
            particleSystem.Play();

            Vector3 directionToTarget = bot.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);


            if (Physics.Raycast(bulletSpawnPoint.position, bot.transform.position - bulletSpawnPoint.position + Vector3.up, out RaycastHit hit, float.PositiveInfinity, botLayerMask))
            {
                var thisGameobj = hit.transform.gameObject;

                if (hit.collider.CompareTag(Constant.TAG_BOT))
                {
                    if (!isDamaging)
                    {
                        isDamaging = true;
                        damageCooldownTimer = damageCooldownDuration;
                    }
                }
                else
                {
                    isDamaging = false;
                    damageCooldownTimer = 0f;
                }
                if (isDamaging && damageCooldownTimer <= 0f)
                {
                    // Trừ máu của bot đi damagePerShot
                    Bot botComponent = hit.collider.GetComponent<Bot>();
                    if (botComponent != null)
                    {
                        botComponent.TakeDamage(damage + addMoreDamage);
                    }

                    isDamaging = false;
                    damageCooldownTimer = damageCooldownDuration;
                }
            }

            if (isDamaging)
            {
                damageCooldownTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (currentLaser != null)
            {
                ParticleSystem particleSystem = currentLaser.GetComponent<ParticleSystem>();
                particleSystem.Stop();
                Destroy(currentLaser, particleSystem.main.duration);
            }
        }
    }
}

