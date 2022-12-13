using Leopotam.Ecs;
using UnityEngine;

public class ProjectileMoveSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileComponent> filter;
    private EcsWorld ecsWorld;
    private SceneData sceneData;


    public void Run()
    {
        foreach (var i in filter)
        {
            ref var entity = ref filter.GetEntity(i);
            ref var projectileComponent = ref filter.Get1(i);

            projectileComponent.RigidBody.AddForce(projectileComponent.DirWithoutSpread * sceneData.configuration.ProjectileSpeed, ForceMode.Impulse);

            entity.Del<ProjectileComponent>();
        }
    }
}