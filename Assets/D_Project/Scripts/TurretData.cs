using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurretData : ScriptableObject
{
    public TurrretType TurrretType;

    public Sprite Sprite;

    public TurretBase Prefab;
}
