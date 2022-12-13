using UnityEngine;
using UnityEngine.AI;

public struct EnemyComponent
{
    public Transform Transform;

    public float DefaultSpeed;
    public float Speed;
    public int Damage;
    public float AttackDistance;

    public Animator EnemyAnimator;
    public NavMeshAgent NavMeshAgent;
    public AudioSource AudioSource;

    public Vector3 SpawnPosition;
    public Vector3[] Route;

    public float NextTimeToAttack;
}
