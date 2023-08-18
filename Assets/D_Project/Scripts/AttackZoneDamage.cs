using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneDamage : MonoBehaviour
{
    public List<TurretBase> turretsInRange = new List<TurretBase>();
    public ParticleSystem upParticleSystem;

    private bool isEffectRunning = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_TURRET))
        {
            TurretBase turretBase = other.GetComponent<TurretBase>();
            if (turretBase != null)
            {
                AddTurretInRange(turretBase);

                if (!isEffectRunning)
                {
                    //StartCoroutine(ShowEffectCoroutine());
                    ShowEffect();
                }
            }
        }
    }

    //private IEnumerator ShowEffectCoroutine()
    //{
    //    while (true)
    //    {
    //        ResetEffect();
    //        ShowEffect();
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //private void ResetEffect()
    //{
    //    if (upParticleSystem != null)
    //    {
    //        upParticleSystem.Stop();
    //        upParticleSystem.Clear();
    //        upParticleSystem.gameObject.SetActive(false);
    //    }
    //}

    private void ShowEffect()
    {
        if (upParticleSystem != null)
        {
            if (!upParticleSystem.isPlaying)
            {
                upParticleSystem.gameObject.SetActive(true);
                upParticleSystem.Play();
            }
        }
    }

    public void AddTurretInRange(TurretBase turret)
    {
        if (!turretsInRange.Contains(turret))
        {
            turretsInRange.Add(turret);

            if (turret is TurretLaser)
            {
                turret.addMoreDamage = 15;
            }
            else if (turret is TurretMachineGun)
            {
                turret.addMoreDamage = 5;
            }
            else if (turret is TurretRocket)
            {
                turret.addMoreDamage = 11;
            }
        }
    }
}
