using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScannerSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    private EcsFilter<EnemyComponent, ScannerComponent, MoveComponent, AnimatorComponent> enemyFilter;
    private EcsFilter<PlayerComponent> playerFilter;
    private SceneData sceneData;

    public bool canSeePlayer = false;
    public float distanceToTarget;

    public LayerMask targetMask = LayerMask.GetMask("Player");
    public LayerMask obstructionMask;

    public void Run()
    {
        foreach (var i in enemyFilter)
        {
            ref var enemyComponent = ref enemyFilter.Get1(i);
            ref var scannerComponent = ref enemyFilter.Get2(i);
            ref var animatorComponent = ref enemyFilter.Get4(i);

            Collider[] rangeChecks = Physics.OverlapSphere(enemyComponent.Transform.position, sceneData.configuration.EnemyScannerRadius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - enemyComponent.Transform.position).normalized;

                if (Vector3.Angle(enemyComponent.Transform.forward, directionToTarget) < sceneData.configuration.EnemyScannerAngle / 2)
                {
                    distanceToTarget = Vector3.Distance(enemyComponent.Transform.position, target.position);

                    if (!Physics.Raycast(enemyComponent.Transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        canSeePlayer = true;
                    else
                        canSeePlayer = false;
                }
                else
                    canSeePlayer = false;
            }
            else if (canSeePlayer)
                canSeePlayer = false;

            if (canSeePlayer)
            {
                animatorComponent.Animator.SetFloat("Move", 1.0f);
                enemyComponent.Speed = enemyComponent.Speed * sceneData.configuration.EnemySpeedUpFollow;
                enemyComponent.NavMeshAgent.speed = enemyComponent.Speed;
                enemyFilter.GetEntity(i).Get<FollowComponent>();
                enemyFilter.GetEntity(i).Del<MoveComponent>();
            }
        }
    }
}