using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryResult, defeatResult;

    private void Awake()
    {
        GameManager.onGameStateChanged += showResult;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= showResult;
    }

    private void showResult(GameState state)
    {
        if (state == GameState.resultState)
        {
            //Debug.Log(GameManager.Instance._winner.GetType());
            if (GameManager.Instance._winner is BasePlayer)
            {
                victoryResult.SetActive(true);
            }
            else
            {
                defeatResult.SetActive(true);
            }
        }
    }
}
