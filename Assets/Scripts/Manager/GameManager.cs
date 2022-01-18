using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<AllyUnit> playerUnits;
    private List<EnemyUnit> enemyUnits;
    //list pour missile

    public int leftBorder, rightBorder;

    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        menu,
        pause,
        inGame
    }
    public static GameState gameState { get; private set; }
    
    void Awake()
    {
        Instance = this;
        leftBorder = 0;
        rightBorder = 20;
        playerUnits = new List<AllyUnit>();
        enemyUnits = new List<EnemyUnit>();
    }
    public void ChangeGameState(GameState state)
    {
        gameState = state;
        switch(gameState)
        {
            case GameState.inGame:
                break;
            case GameState.menu:
                break;
            case GameState.pause:
                break;
        }
    }
    
    void StartPlayerTurn()
    {
        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerUnits[i].NewTurn();
        }
    }
    public void AITurn()
    {
        //activer les fonctions des ia 

        StartPlayerTurn();
    }
    void LunchMissile()
    {

    }
}
