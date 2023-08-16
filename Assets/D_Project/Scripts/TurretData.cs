using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurretData : ScriptableObject
{
    public TurretType TurretType;

    public Sprite Sprite;

    public TurretBase Prefab;

    public int Coin;

}
