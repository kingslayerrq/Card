using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Canvas mainCanvas;
    public GameState gameState;
    public static event Action<GameState> onGameStateChanged;
    public int GameTurn;
    private void Awake()
    {
        Instance = this;
        GameTurn = 0;
    }

    private void Start()
    {
        updateGameState(GameState.initState);
    }
    public void updateGameState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.initState:
                UnitManager.Instance.spawnPlayer("Player01");
                UnitManager.Instance.spawnEnemy("Enemy01");
                CardManager.Instance.initDrawPile();
                CardManager.Instance.drawCards(2);
                updateGameState(GameState.drawState);
                break;
            case GameState.checkState:
                break;
            case GameState.drawState:
                GameTurn++;                            // drawState signals a new turn
                Debug.Log("drawstate");
                CardManager.Instance.drawCards(2);
                updateGameState(GameState.playerTurn);
                break;
            case GameState.playerTurn:
                Debug.Log("playerTurn");
                break;
            case GameState.enemyTurn:
                Debug.Log("enemyturn");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        onGameStateChanged?.Invoke(newState);
    }

    public void endTurn()
    {
        if (gameState == GameState.playerTurn)
        {
            updateGameState(GameState.enemyTurn);
        }
        else
        {
            Debug.Log("it's not ur turn!");
        }

    }


}
public enum GameState
{
    initState = 0,
    drawState = 1,
    checkState = 2,
    playerTurn = 3,
    enemyTurn = 4
}