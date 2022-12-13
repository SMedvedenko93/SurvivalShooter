using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, PlayerInputComponent> playerFilter;
    private SoundData soundData;

    private Vector3 move_Direction;
    private float vertical_Velocity;

    public void Run()
    {
        foreach (var i in playerFilter)
        {
            ref var playerComponent = ref playerFilter.Get1(i);
            ref var playerInputComponent = ref playerFilter.Get2(i);

            move_Direction = playerInputComponent.MoveInput;
            move_Direction = playerComponent.Transform.TransformDirection(move_Direction);
            move_Direction *= playerComponent.Speed * Time.deltaTime;

            playerComponent.Controller.Move(move_Direction);
        }
    }
}