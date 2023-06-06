using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    [SerializeField] private float playerX, playerY;
    private List<ScriptableUnit> _units;   //load all ScriptableUnits into a list

    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    // spawning the player
    public void spawnPlayer(string name)
    {
        var playerPrefab = getPlayer<BasePlayer>(name);
        var spawnedPlayer = Instantiate(playerPrefab);
        spawnedPlayer.transform.position = new Vector3(playerX, playerY, 0);
    }

    // get a Player by name
    private T getPlayer<T>(string unitName) where T:BasePlayer
    {
        return (T)_units.Where(u => u.unitFaction == Faction.Player && u.unitName == name).FirstOrDefault().unitPrefab;
    }

    // get Enemy
    private T getEnemy<T>(string unitName) where T : BaseEnemy
    {
        return (T)_units.Where(u => u.unitFaction == Faction.Enemy && u.unitName == name).FirstOrDefault().unitPrefab;
    }
}
