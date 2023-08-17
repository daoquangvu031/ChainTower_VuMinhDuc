using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneDamage : MonoBehaviour
{
    public List<TurretBase> turretsInRange = new List<TurretBase>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_TURRET))
        {
            TurretBase turretBase = other.GetComponent<TurretBase>();
            if (turretBase != null)
            {
                AddTurretInRange(turretBase);
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
            else if(turret is TurretRocket)
            {
                turret.addMoreDamage = 11;
            }
        }
    }
}
