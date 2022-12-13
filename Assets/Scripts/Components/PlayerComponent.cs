using UnityEngine;

public struct PlayerComponent
{
    public Transform Transform;
    public CharacterController Controller;
    public float Speed;
    public Transform PlayerBody;
    public Transform ProjectileSpawnPosition;
    public float NextTimeToFire;
}
