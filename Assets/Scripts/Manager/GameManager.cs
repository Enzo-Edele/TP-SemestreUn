using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<AllyUnit> playerUnits;  //numeroté les unit pour sup de la liste, idem pour enemy peut etre fait auto dans start
    public List<EnemyUnit> enemyUnits; //numéroté et quand un alien meur check si il en reste pour 
    //list pour missile

    public int leftBorder, rightBorder;

    bool isAIPlaying;
    public int aliensTurnEnd = 0;
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
    }
    private void Update()
    {
        if(isAIPlaying && aliensTurnEnd == 0)
        {
            StartPlayerTurn();
            isAIPlaying = false;
        }
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
        UIManager.Instance.DeactiveTurnText();
    }
    public void AITurn()
    {
        UIManager.Instance.ActiveTurnText();
        UIManager.Instance.UnSelect();
        MouseManager.Instance.ChangeMouseState(MouseManager.MouseState.select);
        aliensTurnEnd = enemyUnits.Count;
        isAIPlaying = true;
        for (int i = 0; i < enemyUnits.Count; i++)
        {
            enemyUnits[i].NewTurn();
        }
        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerUnits[i].actionPoint = 0;
        }
        //StartPlayerTurn(); //le mettre a part aprés vérif que IA est one mettre le check dans check action point
    }
    void LunchMissile()
    {

    }
}
