using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;
    private SceneData sceneData;

    public void Init()
    {
        var playerEntity = ecsWorld.NewEntity();

        ref var playerComponent = ref playerEntity.Get<PlayerComponent>();
        ref var playerInputComponent = ref playerEntity.Get<PlayerInputComponent>();
        ref var animatorComponent = ref playerEntity.Get<AnimatorComponent>();
        ref var health = ref playerEntity.Get<Health>();
        ref var armor = ref playerEntity.Get<Armor>();
        ref var weaponComponent = ref playerEntity.Get<WeaponComponent>();

        var player = sceneData.player;

        playerComponent.Transform = player.transform;
        playerComponent.Speed = sceneData.configuration.PlayerSpeed;
        playerComponent.Controller = player.GetComponent<CharacterController>();
        playerComponent.PlayerBody = player.transform.GetChild(0).transform;

        // weapon 
        weaponComponent.MelleWeapon = false;
        weaponComponent.CurrentInMagazine = sceneData.configuration.currentInMagazine;
        weaponComponent.MaxInMagazine = sceneData.configuration.maxInMagazine;
        weaponComponent.TotalAmmo = sceneData.configuration.totalAmmo;
        weaponComponent.WeaponMeleeDamage = sceneData.configuration.weaponMeleeDamage;
        weaponComponent.WeaponRangeDamage = sceneData.configuration.weaponRangeDamage;
        weaponComponent.WeaponRange = sceneData.configuration.weaponRange;
        weaponComponent.WeaponMelleeRange = sceneData.configuration.weaponMelleeRange;
        weaponComponent.FireRate = sceneData.configuration.fireRate;
        weaponComponent.ProjectileSpeed = sceneData.configuration.ProjectileSpeed;
        weaponComponent.ProjectilePrefab = sceneData.configuration.ProjectilePrefab;
        sceneData.uiData.SetTotalAmoTextValue(sceneData.configuration.totalAmmo);

        //health
        health.Value = sceneData.configuration.PlayerStartHealth;
        armor.Value = sceneData.configuration.PlayerStartArmor;
        sceneData.uiData.SetHealthValue(sceneData.configuration.PlayerStartHealth);
        sceneData.uiData.SetArmorValue(sceneData.configuration.PlayerStartArmor);

        animatorComponent.Animator = player.transform.GetChild(0).GetComponent<Animator>();
        animatorComponent.Animator.SetFloat("Range", 1);

        Cursor.lockState = CursorLockMode.Locked;
    }
}