using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    [SerializeField] private float playerX, playerY;
    public List<ScriptableUnit> _units;   //load all ScriptableUnits into a list
    public Transform playerPanel;
    public Transform enemyPanel;
    public BasePlayer curPLayer;
    public BaseEnemy curEnemy;


    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    // construct Player, loadData
    public BasePlayer spawnPlayer(string name)
    {
        var playerData = getUnitData<BasePlayer>(Faction.Player, name);
        var player = Instantiate(playerData.unitPrefab, playerPanel);
        //player.loadUnitData(playerData);
        curPLayer = (BasePlayer)player;
        return curPLayer;
    }

    
    public BaseEnemy spawnEnemy(string name)
    {
        var enemyData = getUnitData<BaseEnemy>(Faction.Enemy, name);
        var enemy = Instantiate(enemyData.unitPrefab, enemyPanel);
        //enemy.loadUnitData(enemyData);
        curEnemy = (BaseEnemy)enemy;
        return (BaseEnemy)enemy;    
    }

    public void spawnUnit<T>(Faction unitFaction, string unitName) where T : BaseUnit
    {
        switch (unitFaction)
        {
            case Faction.Player:
                spawnPlayer(unitName);
                break;
            case Faction.Enemy:
                spawnEnemy(unitName);
                break;
        }
    }


    
    // get UnitData
    private ScriptableUnit getUnitData<T>(Faction unitFaction, string unitName) where T : BaseUnit
    {
        return (ScriptableUnit)_units.Where(u => u.unitFaction == unitFaction && u.unitName == unitName).FirstOrDefault();
    }

    // get Enemy
    private T getEnemy<T>(string unitName) where T:BaseEnemy
    {
        return (T)_units.Where(u => u.unitFaction == Faction.Enemy && u.unitName == unitName).FirstOrDefault().unitPrefab;
    }
}
