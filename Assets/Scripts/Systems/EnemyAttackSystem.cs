using Leopotam.Ecs;
using UnityEngine;

public class EnemyAttackSystem : IEcsRunSystem
{
    private SceneData sceneData;
    private EcsWorld ecsWorld;
    private EcsFilter<EnemyComponent, TryEnemyAttack, AnimatorComponent> enemyFilter;
    private EcsFilter<PlayerComponent> playerFilter;


    public void Run()
    {
        foreach (var i in enemyFilter)
        {
            ref var enemyComponent = ref enemyFilter.Get1(i);
            ref var animatorComponent = ref enemyFilter.Get3(i);

            animatorComponent.Animator.SetBool("Attack", true);

            if (Time.time >= enemyComponent.NextTimeToAttack)
            {
                enemyComponent.NextTimeToAttack = Time.time + sceneData.configuration.EnemyAttackRate;

                foreach (var j in playerFilter)
                {
                    ref var tryDamage = ref ecsWorld.NewEntity().Get<TryDamage>();
                    tryDamage.Delay = Time.time + sceneData.configuration.enemyAttackDelayHit;
                    tryDamage.Target = playerFilter.GetEntity(j);
                    tryDamage.Attacker = enemyFilter.GetEntity(j);
                    tryDamage.Value = sceneData.configuration.EnemyDamage;
                    enemyFilter.GetEntity(i).Del<TryEnemyAttack>();
                }
            }   
        }
    }

}