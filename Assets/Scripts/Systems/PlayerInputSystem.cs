using Leopotam.Ecs;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    private EcsFilter<PlayerInputComponent, WeaponComponent> playerFilter;

    public void Run()
    {
        foreach (var i in playerFilter)
        {
            ref var playerInputComponent = ref playerFilter.Get1(i);
            ref var weaponComponent = ref playerFilter.Get2(i);
            playerInputComponent.MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            playerInputComponent.Attack = Input.GetMouseButton(0);

            //reload weapon
            if (Input.GetKeyDown(KeyCode.R) && weaponComponent.MelleWeapon == false)
            {
                var tryReload = ecsWorld.NewEntity();
                tryReload.Get<TryReload>();
            }

            //select mellee weapon
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var tryWeaponSelect = ecsWorld.NewEntity();
                tryWeaponSelect.Get<TryWeaponSelect>().Type = "ToMellee";
            }

            //select range weapon
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                var tryWeaponSelect = ecsWorld.NewEntity();
                tryWeaponSelect.Get<TryWeaponSelect>().Type = "ToRange";
            }
        }
    }
}
