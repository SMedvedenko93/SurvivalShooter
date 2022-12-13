using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Configuration : ScriptableObject
{
    [Header("Player")]
    public int PlayerStartHealth;
    public int PlayerStartArmor;
    public float PlayerSpeed;

    [Header("Case")]
    public GameObject CaseHealth;
    public GameObject CaseWeapon;
    public GameObject CaseArmor;
    public float CaseSpawnInterval;
    public Vector3[] CaseSpawnPosition;


    [Header("Projectile")]
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;
    public float ProjectileTimeLive;

    [Header("Weapon")]
    public int weaponMeleeDamage;
    public int weaponRangeDamage;
    public float weaponRange;
    public float weaponMelleeRange;
    public float fireRate;
    public float weaponMelleDelayHit;
    public int currentInMagazine;
    public int maxInMagazine;
    public int totalAmmo;

    [Header("Enemy")]
    public GameObject EnemyPrefab;
    public float EnemySpawnInterval;
    public int EnemyDamage;
    public float EnemySpeedWalk;
    public int EnemyStartHealth;
    public int EnemyAttackDistance;
    public float enemyAttackDelayHit;
    public float EnemyAttackRate;
    public int EnemyFollowDistance;
    public int EnemySpeedUpFollow;
    public int EnemyScannerRadius;
    public int EnemyScannerAngle;
    public Vector3[] EnemySpawnPosition;
    public Vector3[] EnemyMoveStartPosition;
    public Vector3[] EnemyMoveMiddlePosition;
    public Vector3[] EnemyMoveEndPosition;

    /*
    public const string MOVE = "gameSaltCTSweet";
    public const string RANGE = "CurrentLocation";
    public const string RELOAD = "CurrentLevel";
    public const string SWAP = "Ads";
    public const string HIT = "Ads";
    public const string ENEMY = "CountHint";
    public const string PLAYER = "GameCount";
    public const string TOMELLEE = "GameCount";
    public const string TORANGE = "GameCount";
    public const string DEATH = "GameCount";
    public const string SPAWN = "EnemySpawn";
    */
    
}