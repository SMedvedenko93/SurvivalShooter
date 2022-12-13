using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, PlayerInputComponent, WeaponComponent, AnimatorComponent> playerFilter;
    private SoundData soundData;

    public void Run()
    {
        foreach (var i in playerFilter)
        {
            ref PlayerComponent playerComponent = ref playerFilter.Get1(i);
            ref PlayerInputComponent playerInputComponent = ref playerFilter.Get2(i);
            ref WeaponComponent weaponComponent = ref playerFilter.Get3(i);
            ref AnimatorComponent animatorComponent = ref playerFilter.Get4(i);

            if (playerInputComponent.MoveInput != Vector3.zero)
            {
                animatorComponent.Animator.SetFloat("Action", 1.0f); // idle for melle
            }
            else
            {
                animatorComponent.Animator.SetFloat("Action", 0.0f); // walk for range
            }
        }

    }
}