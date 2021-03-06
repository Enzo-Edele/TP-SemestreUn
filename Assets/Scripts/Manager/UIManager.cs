using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject infoPannel;
    [SerializeField] Text Unitname;
    [SerializeField] Text PV;
    [SerializeField] Text action;
    [SerializeField] Text attackRange;
    [SerializeField] Text attack;
    [SerializeField] Text move;

    [SerializeField] Button nextTurnButon;
    [SerializeField] GameObject playerButon;
    [SerializeField] Button moveButon;
    [SerializeField] GameObject tutoPannel;
    [SerializeField] Text tutoText;
    [SerializeField] GameObject alienTurnText;

    [SerializeField] GameObject gameOverPannel;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject winPannel;
    [SerializeField] GameObject winText;

    float timeMove = 2.5f;
    public float timerMove = 0;
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {
        if (timerMove != 0)
            timerMove -= Time.deltaTime;
        if(timerMove < 0)
            ReactivateMove();
    }
    public void Select(string name, int HP, int HPMax, int PA, int PAMax, int attRange, int att, int mov)
    {
        infoPannel.SetActive(true);
        Unitname.text = name;
        PV.text = HP + " / " + HPMax;
        action.text = PA + " / " + PAMax;
        attackRange.text = "" + attRange;
        attack.text = "" + att;
        move.text = "" + mov;
    }
    public void ActiveButon()
    {
        playerButon.SetActive(true);
    }
    public void UnSelect()
    {
        infoPannel.SetActive(false);
        if (playerButon != null) playerButon.SetActive(false);
    }
    public void ActiveTurnText()
    {
        alienTurnText.SetActive(true);
        nextTurnButon.interactable = false;
    }
    public void DeactiveTurnText()
    {
        alienTurnText.SetActive(false);
        nextTurnButon.interactable = true;
    }
    public void ActiveGameOver()
    {
        gameOverPannel.SetActive(true);
        gameOverText.SetActive(true);
    }
    public void DeactiveGameOver()
    {
        if (gameOverPannel != null) gameOverPannel.SetActive(false);
        if (gameOverText != null) gameOverText.SetActive(false);
    }
    public void ActiveWin()
    {
        winPannel.SetActive(true);
        winText.SetActive(true);
    }
    public void DeactiveWin()
    {
        if (winPannel != null) winPannel.SetActive(false);
        if (winText != null) winText.SetActive(false);
    }
    public void DeactivateMove()
    {
        moveButon.interactable = false;
        timerMove = timeMove;
    }
    public void ReactivateMove()
    {
        moveButon.interactable = true;
        timerMove = 0;
    }
    public void ButtonNextTurn()
    {
        if(!GameManager.Instance.isAIPlaying) GameManager.Instance.AITurn();
    }
    public void ButtonMove()
    {
        if(MouseManager.Instance.select != null)
            MouseManager.Instance.ChangeMouseState(MouseManager.MouseState.move);
    }
    public void ButtonAtt()
    {
        if(MouseManager.Instance.select != null)
            MouseManager.Instance.ChangeMouseState(MouseManager.MouseState.shoot);
    }
    public void ButtonTuto()
    {
        UnSelect();
        if (tutoPannel.activeSelf)
        {
            tutoPannel.SetActive(false);
            tutoText.text = "Tuto";
        }
        else
        {
            tutoPannel.SetActive(true);
            tutoText.text = "Close";
        }
    }
    public void ButtonRestart()
    {
        DeactiveGameOver();
        DeactiveWin();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ButtonQuit()
    {
        Application.Quit();
    }
}
