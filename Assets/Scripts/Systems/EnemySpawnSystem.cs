using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    private SceneData sceneData;

    private float nextActionTime = 0.0f;

    public void Run()
    {
        if (Time.time > nextActionTime)
        {
            EcsEntity enemyEntity = ecsWorld.NewEntity();

            ref var enemyComponent = ref enemyEntity.Get<EnemyComponent>();
            ref var moveComponent = ref enemyEntity.Get<MoveComponent>();
            ref var animatorComponent = ref enemyEntity.Get<AnimatorComponent>();
            ref var health = ref enemyEntity.Get<Health>();
            ref var scanner = ref enemyEntity.Get<ScannerComponent>();

            var positionSpawnIndex = Random.Range(0, sceneData.configuration.EnemySpawnPosition.Length);
            var positionIndex = Random.Range(0, sceneData.configuration.EnemyMoveStartPosition.Length);

            GameObject enemy = Object.Instantiate(sceneData.configuration.EnemyPrefab, sceneData.configuration.EnemySpawnPosition[positionSpawnIndex], Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            enemy.GetComponent<EnemyView>().entity = enemyEntity;
            enemy.GetComponent<EnemyView>().sceneData = sceneData;

            enemyComponent.Transform = enemy.transform;
            enemyComponent.DefaultSpeed = sceneData.configuration.EnemySpeedWalk;
            enemyComponent.Speed = sceneData.configuration.EnemySpeedWalk;
            enemyComponent.NavMeshAgent = enemy.GetComponent<NavMeshAgent>();
            enemyComponent.EnemyAnimator = enemy.GetComponent<Animator>();

            enemyComponent.Route = new Vector3[3];

            enemyComponent.Route[0] = sceneData.configuration.EnemyMoveStartPosition[positionIndex];
            enemyComponent.Route[1] = sceneData.configuration.EnemyMoveMiddlePosition[positionIndex];
            enemyComponent.Route[2] = sceneData.configuration.EnemyMoveEndPosition[positionIndex];

            health.Value = sceneData.configuration.EnemyStartHealth;
            animatorComponent.Animator = enemy.transform.GetComponent<Animator>();

            nextActionTime += sceneData.configuration.EnemySpawnInterval;
        }
    }
}