using Leopotam.Ecs;

public class EnemyDeathSystem : IEcsRunSystem
{
    private EcsFilter<TryEnemyDeath, AnimatorComponent> deathFilter;
    private SceneData sceneData;

    public void Run()
    {
        foreach (var i in deathFilter)
        {
            ref var animatorComponent = ref deathFilter.Get2(i);
            animatorComponent.Animator.SetTrigger("Death");
        }
    }
}