using Leopotam.Ecs;

public class TakeCaseSystem : IEcsRunSystem
{
    private SceneData sceneData;
    private EcsWorld ecsWorld;
    private EcsFilter<TryTakeCase> caseFilter;
    private EcsFilter<PlayerComponent> playerFilter;

    public void Run()
    {
        foreach (var i in caseFilter)
        {
            ref var caseComponent = ref caseFilter.Get1(i);

            foreach (var j in playerFilter)
            {
                ref var playerComponent = ref playerFilter.Get1(i);
                
                switch (caseComponent.Type)
                {
                    case "armor":
                        var newArmorVal = playerFilter.GetEntity(j).Get<Armor>().Value + 10;
                        if (newArmorVal > 100)
                            newArmorVal = 100;
                        playerFilter.GetEntity(j).Get<Armor>().Value = newArmorVal;
                        sceneData.uiData.SetArmorValue(newArmorVal);
                        break;
                    case "health":
                        var newHealthVal = playerFilter.GetEntity(j).Get<Health>().Value + 10;
                        if (newHealthVal > 100)
                            newHealthVal = 100;
                        playerFilter.GetEntity(j).Get<Health>().Value = newHealthVal;
                        sceneData.uiData.SetHealthValue(newHealthVal);
                        break;
                    case "weapon":
                        var newAmmoVal = playerFilter.GetEntity(j).Get<WeaponComponent>().TotalAmmo + 16;
                        if (newAmmoVal > 96)
                            newAmmoVal = 96;
                        playerFilter.GetEntity(j).Get<WeaponComponent>().TotalAmmo = newAmmoVal;
                        sceneData.uiData.SetTotalAmoTextValue(newAmmoVal);
                        break;
                }
            }

            caseFilter.GetEntity(i).Destroy();
        }
    }
}