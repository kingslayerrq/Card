using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    [SerializeField] private float playerX, playerY;
    public List<ScriptableUnit> _units;   //load all ScriptableUnits into a list

    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    // construct Player, loadData
    public BasePlayer spawnPlayer(string name)
    {
        var playerData = getUnitData<BasePlayer>(Faction.Player, name);
        var player = Instantiate(playerData.unitPrefab);
        player.loadUnitData(playerData);
        player.transform.position = new Vector3(playerX, playerY, 0);
        return (BasePlayer)player;
    }

    

    // get PlayerData
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
