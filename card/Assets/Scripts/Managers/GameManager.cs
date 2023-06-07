using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Canvas mainCanvas;
    public GraphicRaycaster mainRaycaster;
    public GameState gameState;
    private void Awake()
    {
        Instance = this;
        mainRaycaster = mainCanvas.GetComponent<GraphicRaycaster>();
        
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
                CardManager.Instance.initDrawPile();
                CardManager.Instance.drawCards(1);
                
                updateGameState(GameState.drawState);
                break;
            case GameState.checkState:
                break;
            case GameState.drawState:
                Debug.Log("drawstate");
                CardManager.Instance.drawCards(2);
                
                break;
            case GameState.playerTurn:
                break;
            case GameState.enemyTurn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
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