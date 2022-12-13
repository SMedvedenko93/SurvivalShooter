using Leopotam.Ecs;
using UnityEngine;

public struct TryDamage
{ 
    public EcsEntity Attacker;
    public EcsEntity Target;
    public int Value;
    public float Delay;
}