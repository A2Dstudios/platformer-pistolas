using System;
using UnityEngine;

public enum BombType
{
    GRENADE
}

[Serializable]
public class BombEntry
{
    public BombType type;
    public GameObject prefab;
}
