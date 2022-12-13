using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageSystem : IEcsRunSystem
{
    private SceneData sceneData;
    private EcsWorld ecsWorld;
    private EcsFilter<TryDamage> damageFilter;

    public void Run()
    {
        foreach (var i in damageFilter)
        {
            ref var damageComponent = ref damageFilter.Get1(i);
            ref var healthComponent = ref damageComponent.Target.Get<Health>();
            ref var armorComponent = ref damageComponent.Target.Get<Armor>();

            if (Time.time >= damageComponent.Delay)
            {
                if (damageComponent.Attacker.Has<EnemyComponent>())
                {
                    if (Vector3.Distance(damageComponent.Attacker.Get<EnemyComponent>().Transform.position, damageComponent.Target.Get<PlayerComponent>().Transform.position) >= sceneData.configuration.EnemyAttackDistance)
                    {
                        damageFilter.GetEntity(i).Destroy();
                        continue;
                    }
                }


                if (damageComponent.Target.Has<EnemyComponent>())
                {
                    sceneData.soundData.HitReact("Enemy");
                    damageComponent.Target.Get<AnimatorComponent>().Animator.SetBool("Hit", true);
                } else
                {
                    sceneData.soundData.HitReact("Player");
                }
 

                if (armorComponent.Value == 0)
                {
                    healthComponent.Value -= damageComponent.Value;
                    if (damageComponent.Target.Has<PlayerComponent>())
                    {
                        sceneData.uiData.SetHealthValue(healthComponent.Value);
                    }
                }
                else
                {
                    if (armorComponent.Value - damageComponent.Value < 0)
                    {
                        armorComponent.Value = 0;
                    }
                    else
                    {
                        armorComponent.Value -= damageComponent.Value;
                    }
                    if (damageComponent.Target.Has<PlayerComponent>())
                    {
                        sceneData.uiData.SetArmorValue(armorComponent.Value);
                    }
                }



                if (healthComponent.Value <= 0)
                {
                    if (damageComponent.Target.Has<EnemyComponent>())
                    {
                        damageComponent.Target.Get<TryEnemyDeath>();
                    }

                    if (damageComponent.Target.Has<PlayerComponent>())
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        continue;
                    }
                }

                damageFilter.GetEntity(i).Destroy();
            } 
        }
    }
}