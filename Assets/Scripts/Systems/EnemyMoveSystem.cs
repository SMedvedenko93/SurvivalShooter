using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData sceneData;
    private EcsFilter<EnemyComponent, MoveComponent, AnimatorComponent>.Exclude<FollowComponent> enemyFilter;
    private Vector3 endPosition;
    private int j;
    private string direction;

    public void Init()
    {
        j = 0;
        direction = "Forward";
    }

    public void Run()
    {
        foreach (var i in enemyFilter)
        {
            ref var enemyComponent = ref enemyFilter.Get1(i);
            ref var animatorComponent = ref enemyFilter.Get3(i);

            var currentClip = animatorComponent.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (currentClip == "EnemySpawn")
            {
                continue;
            }

            enemyComponent.Transform.GetComponent<EnemyView>().colliderBody.enabled = true;
            enemyComponent.Transform.GetComponent<EnemyView>().colliderHead.enabled = true;
            NavMeshAgent agent = enemyComponent.NavMeshAgent;
            agent.speed = enemyComponent.Speed;

            if(Vector3.Distance(enemyComponent.Route[j], enemyComponent.Transform.position) < 1.0f){
                if (direction == "Forward")
                {
                    if (j + 1 == enemyComponent.Route.Length)
                    {
                        direction = "Backward";
                        j--;
                    }
                    else
                    {
                        j++;
                    }
                    
                }
                else
                {
                    if (j - 1 < 0)
                    {
                        direction = "Forward";
                        j++;
                    }
                    else
                    {
                        j--;
                    }
                }
                
            }


            agent.destination = enemyComponent.Route[j];
        }
    }
}
