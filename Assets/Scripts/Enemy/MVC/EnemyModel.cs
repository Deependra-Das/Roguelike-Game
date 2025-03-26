using Roguelike.Enemy;
using UnityEngine;

public class EnemyModel
{
    private EnemyController _enemyController;

    public EnemyModel(EnemyScriptableObject enemyDataObj)
    {
        EnemyID = enemyDataObj.enemyID;
        EnemyType = enemyDataObj.EnemyType;
        EnemyPrefab = enemyDataObj.enemyPrefab;
        EnemyImage = enemyDataObj.enemyImage;
        EnemyName = enemyDataObj.enemyName;
        SpawnPosition = enemyDataObj.spawnPosition;
        SpawnRotation = enemyDataObj.spawnRotation;
        MovementSpeed = enemyDataObj.movementSpeed;
        Health = enemyDataObj.health;
        AttackPower = enemyDataObj.attackPower;
        AttackCooldown = enemyDataObj.attackCooldown;
    }

    ~EnemyModel() { }

    public void SetController(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public int EnemyID { get; private set; }
    public EnemyType EnemyType { get; private set; }
    public EnemyView EnemyPrefab { get; private set; }
    public Sprite EnemyImage { get; private set; }
    public string EnemyName { get; private set; }
    public Vector3 SpawnPosition { get; private set; }
    public Vector3 SpawnRotation { get; private set; }
    public float MovementSpeed { get; private set; }
    public int Health { get; private set; }
    public int AttackPower { get; private set; }
    public float AttackCooldown { get; private set; }

}
