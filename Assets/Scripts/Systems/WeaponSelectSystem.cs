using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, PlayerInputComponent, WeaponComponent, AnimatorComponent> playerFilter;
    private EcsFilter<TryWeaponSelect> tryWeaponSelectFilter;

    public void Run()
    {
        foreach (var i in tryWeaponSelectFilter)
        {
            var entity = tryWeaponSelectFilter.GetEntity(i);
            var type = entity.Get<TryWeaponSelect>().Type;
            tryWeaponSelectFilter.GetEntity(i).Del<TryWeaponSelect>();

            foreach (var j in playerFilter)
            {
                ref var playerComponent = ref playerFilter.Get1(j);
                ref var playerInputComponent = ref playerFilter.Get2(j);
                ref var weaponComponent = ref playerFilter.Get3(j);
                ref var animatorComponent = ref playerFilter.Get4(j);


                if (type == "ToMellee")
                {
                    if (weaponComponent.MelleWeapon != true)
                    {
                        weaponComponent.MelleWeapon = true;
                        animatorComponent.Animator.SetBool("Swap", true);
                        animatorComponent.Animator.SetFloat("Range", 0);
                    }
                }
                else
                {
                    if (weaponComponent.MelleWeapon == true)
                    {
                        weaponComponent.MelleWeapon = false;
                        animatorComponent.Animator.SetBool("Swap", true);
                        animatorComponent.Animator.SetFloat("Range", 1);
                    }
                }
            }
        }
    }
}