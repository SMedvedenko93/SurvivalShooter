using UnityEngine;

public struct WeaponComponent
{
    public bool MelleWeapon;

    public int WeaponMeleeDamage;
    public int WeaponRangeDamage;
    public float WeaponMelleeRange;
    public float WeaponRange;
    public float FireRate;
    public int CurrentInMagazine;
    public int MaxInMagazine;
    public int TotalAmmo;

    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;
}
