using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloadingSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, WeaponComponent, AnimatorComponent> playerFilter;
    private EcsFilter<TryReload> TryReloadFilter;
    private SceneData sceneData;

    public void Run()
    {
        foreach (var i in TryReloadFilter)
        {
            var entity = TryReloadFilter.GetEntity(i);
            TryReloadFilter.GetEntity(i).Del<TryReload>();

            foreach (var j in playerFilter)
            {
                ref var playerComponent = ref playerFilter.Get1(j);
                ref var weaponComponent = ref playerFilter.Get2(j);
                ref var animatorComponent = ref playerFilter.Get3(j);

                if (animatorComponent.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "PlayerReload")
                {
                    if (weaponComponent.TotalAmmo > 0)
                    {
                        sceneData.soundData.Reload();
                        animatorComponent.Animator.SetBool("Reload", true);
                        if (weaponComponent.TotalAmmo >= weaponComponent.MaxInMagazine)
                        {
                            weaponComponent.CurrentInMagazine = weaponComponent.MaxInMagazine;
                            weaponComponent.TotalAmmo = weaponComponent.TotalAmmo - weaponComponent.MaxInMagazine;
                            sceneData.uiData.SetTotalAmoTextValue(weaponComponent.TotalAmmo);
                            sceneData.uiData.SetCurrentInMagazineValue(weaponComponent.CurrentInMagazine);

                        }
                        else
                        {
                            weaponComponent.CurrentInMagazine = weaponComponent.TotalAmmo;
                            weaponComponent.TotalAmmo = weaponComponent.TotalAmmo - weaponComponent.TotalAmmo;
                            sceneData.uiData.SetTotalAmoTextValue(weaponComponent.TotalAmmo);
                            sceneData.uiData.SetCurrentInMagazineValue(weaponComponent.CurrentInMagazine);
                        }
                    }
                }
            }
        }
    }
}