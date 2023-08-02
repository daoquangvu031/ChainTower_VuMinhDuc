using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurretBase: MonoBehaviour
{

    public AttackZone attackZone;

    private GameObject currentTarget;
    
    
    public virtual void SetTargetBot(GameObject bot)
    {
        if (currentTarget != bot)
        {
            currentTarget = bot;
        }
    }
}
