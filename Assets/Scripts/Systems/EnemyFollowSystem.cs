using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    private EcsFilter<PlayerComponent> playerFilter;
    private EcsFilter<EnemyComponent, FollowComponent, AnimatorComponent>.Exclude<MoveComponent> enemyFilter;
    private SceneData sceneData;

    public void Run()
    {
        foreach (var i in enemyFilter)
        {
            ref var enemyComponent = ref enemyFilter.Get1(i);
            ref var animatorComponent = ref enemyFilter.Get3(i);
            NavMeshAgent agent = enemyComponent.NavMeshAgent;
            agent.speed = enemyComponent.Speed;

            enemyComponent.Transform.GetComponent<EnemyView>().colliderBody.enabled = true;
            enemyComponent.Transform.GetComponent<EnemyView>().colliderHead.enabled = true;

            foreach (var j in playerFilter)
            {
                ref var playerComponent = ref playerFilter.Get1(i);

                if (Vector3.Distance(enemyComponent.Transform.position, playerComponent.Transform.position) <= sceneData.configuration.EnemyAttackDistance)
                {
                    enemyComponent.NavMeshAgent.enabled = false;
                    enemyFilter.GetEntity(i).Get<TryEnemyAttack>();
                }
                else
                {
                    if (animatorComponent.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "EnemyAttack")
                    {
                        enemyComponent.NavMeshAgent.enabled = true;
                        animatorComponent.Animator.SetFloat("Move", 2.0f);
                        agent.destination = playerComponent.Transform.position;
                    }
                }

                if (Vector3.Distance(enemyComponent.Transform.position, playerComponent.Transform.position) > sceneData.configuration.EnemyFollowDistance)
                {
                    animatorComponent.Animator.SetFloat("Move", 0.0f);
                    enemyComponent.Speed = enemyComponent.Speed / sceneData.configuration.EnemySpeedUpFollow;
                    enemyComponent.NavMeshAgent.speed = enemyComponent.Speed;
                    agent.destination = enemyComponent.SpawnPosition;
                    enemyFilter.GetEntity(i).Del<FollowComponent>();
                    enemyFilter.GetEntity(i).Get<MoveComponent>();
                }
            }
        }
    }
}