using Leopotam.Ecs;
using UnityEngine;

public class ProjectileSpawnSystem : IEcsRunSystem
{
    private EcsFilter<TrySpawnProjectile> filter;
    private EcsWorld ecsWorld;
    private SceneData sceneData;

    private RaycastHit hit;
    private Vector3 target;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var entity = ref filter.GetEntity(i);
            var dirWithoutSpread = entity.Get<TrySpawnProjectile>().DirWithoutSpread;

            EcsEntity projectileEntity = ecsWorld.NewEntity();
            ref var projectileComponent = ref projectileEntity.Get<ProjectileComponent>();
 
            GameObject projectile = Object.Instantiate(sceneData.configuration.ProjectilePrefab, sceneData.projectileSpawnPosition.transform.position, Quaternion.identity);
            projectile.GetComponent<ProjectileTimerDestroy>().timeLive = sceneData.configuration.ProjectileTimeLive;
            projectile.transform.forward = dirWithoutSpread.normalized;
            projectileComponent.Transform = projectile.transform;
            projectileComponent.RigidBody = projectile.GetComponent<Rigidbody>();
            projectileComponent.Collider = projectile.GetComponent<SphereCollider>();
            projectileComponent.DirWithoutSpread = dirWithoutSpread.normalized;


            entity.Del<TrySpawnProjectile>();
        }
    }
}