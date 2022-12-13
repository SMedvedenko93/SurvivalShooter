using Leopotam.Ecs;
using UnityEngine;

public class Startup : MonoBehaviour
{
    private EcsWorld ecsWorld;

    private EcsSystems initSystems;
    private EcsSystems updateSystems;
    private EcsSystems fixedUpdateSystems;

    public Configuration configuration;
    public SceneData sceneData;
    public UIData uiData;
    public SoundData soundData;

    // Инициализация окружения.
    private void Start()
    {
        ecsWorld = new EcsWorld();

        initSystems = new EcsSystems(ecsWorld)
            .Add(new PlayerInitSystem())
            .Inject(sceneData);

        updateSystems = new EcsSystems(ecsWorld)
            .Add(new PlayerInputSystem())
            .Add(new PlayerAnimationSystem())
            .Add(new PlayerAttackSystem())
            .Add(new EnemySpawnSystem())
            .Add(new EnemyMoveSystem())
            .Add(new EnemyScannerSystem())
            .Add(new EnemyFollowSystem())
            .Add(new EnemyAttackSystem())
            .Add(new CaseSpawnSystem())
            .Add(new WeaponSelectSystem())
            .Add(new ProjectileSpawnSystem())
            .Add(new ProjectileMoveSystem())
            .Add(new WeaponReloadingSystem())
            .Add(new DamageSystem())
            .Add(new EnemyDeathSystem())
            .Add(new TakeCaseSystem())
            .Add(new TimerSystem())
            .Inject(sceneData);

        fixedUpdateSystems = new EcsSystems(ecsWorld)
            .Add(new PlayerMoveSystem())
            .Add(new PlayerRotationSystem())
            .Inject(sceneData);

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(ecsWorld);
#endif

        initSystems.ProcessInjects();
        updateSystems.ProcessInjects();
        fixedUpdateSystems.ProcessInjects();

        initSystems.Init();
        updateSystems.Init();
        fixedUpdateSystems.Init();

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(initSystems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(updateSystems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(fixedUpdateSystems);
#endif

    }

    private void Update()
    {
        updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        fixedUpdateSystems?.Run();
    }

    private void OnDestroy()
    {
        initSystems?.Destroy();
        initSystems = null;
        updateSystems?.Destroy();
        updateSystems = null;
        fixedUpdateSystems?.Destroy();
        fixedUpdateSystems = null;
        ecsWorld?.Destroy();
        ecsWorld = null;
    }
}

