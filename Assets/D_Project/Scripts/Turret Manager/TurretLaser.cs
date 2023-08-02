using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : TurretBase
{
    [SerializeField] LayerMask botLayerMask;

    public int damage = 20;
    public GameObject laserPrefab;
    public Transform bulletSpawnPoint;

    private GameObject currentLaser;
    private bool isDamaging = false;
    private float damageCooldownTimer = 0f;
    private float damageCooldownDuration = 0.5f;


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

            transform.LookAt(bot.transform.position);


            if (Physics.Raycast(bulletSpawnPoint.position, bot.transform.position - bulletSpawnPoint.position + Vector3.up, out RaycastHit hit, float.PositiveInfinity, botLayerMask))
            {
                var thisGameobj = hit.transform.gameObject;

                if (hit.collider.CompareTag(Constant.TAG_BOT))
                {
                    if (!isDamaging)
                    {
                        // Bắt đầu đếm ngược trước khi trừ máu
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
                        botComponent.TakeDamage(damage);
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

