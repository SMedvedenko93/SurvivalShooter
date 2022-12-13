using UnityEngine;

public struct ProjectileComponent
{
    public Transform Transform;
    public Rigidbody RigidBody;
    public float Speed;
    public Vector3 DirWithoutSpread;
    public Collider Collider;
}