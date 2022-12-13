using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    private EcsFilter<TimerComponent> timerFilter;

    public void Run()
    {
        foreach (var i in timerFilter)
        {
            ref var timerComponent = ref timerFilter.Get1(i);
            timerComponent.Time -= Time.deltaTime;
            if (timerComponent.Time < 0)
            {
                timerComponent.Time += timerComponent.Interval;
            }
        }

    }
}