using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : IEcsRunSystem
{
    private SceneData sceneData;
    private EcsWorld ecsWorld;
    private EcsFilter<PlayerComponent, PlayerInputComponent, WeaponComponent, AnimatorComponent> playerFilter;

    private RaycastHit hit;
    private Vector3 target;

    public void Run()
    {
        foreach (var i in playerFilter)
        {
            ref var playerComponent = ref playerFilter.Get1(i);
            ref var playerInputComponent = ref playerFilter.Get2(i);
            ref var weaponComponent = ref playerFilter.Get3(i);
            ref var animatorComponent = ref playerFilter.Get4(i);

            if (playerInputComponent.Attack && Time.time >= playerComponent.NextTimeToFire)
            {
                playerComponent.NextTimeToFire = Time.time + sceneData.configuration.fireRate;
                var currentClip = animatorComponent.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                if (currentClip == "PlayerReload" || currentClip == "PlayerSwapRange" || currentClip == "PlayerSwapMellee")
                {
                }
                else
                {
                    if (weaponComponent.MelleWeapon == true)
                    {
                        sceneData.soundData.Attack("Mellee");
                        animatorComponent.Animator.SetBool("Attack", true);
                        
                        if (Physics.Raycast(sceneData.mainCamera.transform.position, sceneData.mainCamera.transform.forward, out hit, sceneData.configuration.weaponMelleeRange))
                        {
                            target = hit.point;
                            if (hit.transform.root.gameObject.TryGetComponent(out EnemyView enemyView))
                            {
                                TryDamage(enemyView, playerFilter.GetEntity(i));
                            }
                        }
                    }
                    else
                    {
                        if (weaponComponent.CurrentInMagazine > 0)
                        {
                            sceneData.soundData.Attack("Range");
                            animatorComponent.Animator.SetBool("Attack", true);
                            if (Physics.Raycast(sceneData.mainCamera.transform.position, sceneData.mainCamera.transform.forward, out hit, sceneData.configuration.weaponRange))
                            {
                                target = hit.point;
                                if (hit.transform.root.gameObject.TryGetComponent(out EnemyView enemyView))
                                {
                                    TryDamage(enemyView, playerFilter.GetEntity(i));
                                }
                            }
                            else
                            {
                                target = sceneData.mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).GetPoint(sceneData.configuration.weaponRange);
                            }

                            Vector3 dirWithoutSpread = target - sceneData.projectileSpawnPosition.transform.position;

                            var TrySpawnProjectile = ecsWorld.NewEntity();
                            TrySpawnProjectile.Get<TrySpawnProjectile>().DirWithoutSpread = dirWithoutSpread;

                            weaponComponent.CurrentInMagazine--;
                            sceneData.uiData.SetCurrentInMagazineValue(weaponComponent.CurrentInMagazine);
                        }
                        else
                        {
                            sceneData.soundData.Empty();
                            // no weapon
                        }
                    }
                }
            }
        }
    }

    public void TryDamage(EnemyView enemyView, EcsEntity playerEntity)
    {
        ref var tryDamage = ref ecsWorld.NewEntity().Get<TryDamage>();
        tryDamage.Delay = Time.time + sceneData.configuration.weaponMelleDelayHit;
        tryDamage.Target = enemyView.entity;
        tryDamage.Attacker = playerEntity;
        tryDamage.Value = sceneData.configuration.weaponRangeDamage;

        enemyView.entity.Get<AnimatorComponent>().Animator.SetFloat("Move", 1.0f);
        if (enemyView.entity.Get<EnemyComponent>().Speed <= enemyView.entity.Get<EnemyComponent>().DefaultSpeed)
        {
            enemyView.entity.Get<EnemyComponent>().Speed = enemyView.entity.Get<EnemyComponent>().Speed * sceneData.configuration.EnemySpeedUpFollow;
        }
        enemyView.entity.Get<EnemyComponent>().NavMeshAgent.speed = enemyView.entity.Get<EnemyComponent>().Speed;

        enemyView.entity.Get<FollowComponent>();
        enemyView.entity.Del<MoveComponent>();
    }
}