using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int gameDuration = 300;
    public enum TURN
    {
        ALLY, ENEMY, SIMULATION
    }
    public TURN currentTurn;
    private bool canDoTurn = true;
    [SerializeField] private int turnDuration;
    [SerializeField] private int simDuration;
    [SerializeField] private float simSpeed;

    [SerializeField] private CardSetUp[] posibleCards;
    [SerializeField] private int deckLength;

    public int[] listOfThreats;
    public List<GameObject> enemyUnits;
    public List<Card> enemyCards;
    [SerializeField] public GameObject[] enemyTowers;

    public int allyTurnsMana;   
    public int enemyTurnsMana;
    public float timer;
    public float turnTimer;
    public float simTimer;
    private TURN nextTurn;
    [SerializeField] GameObject timerObject;

    [SerializeField] private GameObject allyTurnCounter;
    [SerializeField] private GameObject enemyTurnCounter;
    [SerializeField] private GameObject allyTurnText;
    [SerializeField] private GameObject enemyTurnText;
    
    private void Awake()
    {
        currentTurn = TURN.ALLY;
        nextTurn = TURN.ENEMY;
        timer = gameDuration;
        simTimer = simDuration;
        turnTimer = turnDuration;
        
        listOfThreats = new int[3];
        enemyCards = new List<Card>();
        CreateDeck();
        allyTurnsMana = 1;
        enemyTurnsMana = 1;
        
    }

    private void Update()
    {
        DisplayTime();
        DisplayTurns();
    }

    private void DisplayTurns()
    {
        
        allyTurnCounter.GetComponent<TextMeshProUGUI>().text = "Ally turns: " + allyTurnsMana.ToString();
        enemyTurnCounter.GetComponent<TextMeshProUGUI>().text = "Enemy turns: " + enemyTurnsMana.ToString();

        if(currentTurn == TURN.ALLY)
        {
            allyTurnText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else if(currentTurn == TURN.ENEMY)
        {
            enemyTurnText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            allyTurnText.GetComponent<TextMeshProUGUI>().color = Color.white;
            enemyTurnText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }

    private void DisplayTime()
    {
        timer -= Time.deltaTime;

        if(currentTurn == TURN.ALLY || currentTurn == TURN.ENEMY)
        {
            turnTimer -= Time.deltaTime;
        }
        else
        {
            simTimer -= Time.deltaTime;
        }

        if(turnTimer <= 0.0f)
        {
            currentTurn = TURN.SIMULATION;
            turnTimer = turnDuration;
        }
        else if(simTimer <= 0.0f)
        {
            currentTurn = nextTurn;
            canDoTurn = true;
            if(currentTurn == TURN.ALLY)
            {
                nextTurn = TURN.ENEMY;
            }
            else
            {
                nextTurn = TURN.ALLY;
            }
            simTimer = simDuration;
        }
        
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        timerObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);


    }

    private void CreateDeck()
    {
        for (int i = 0; i < deckLength; i++)
        {
            Card newCard = new Card();
            int randomInt = UnityEngine.Random.Range(0, posibleCards.Length);
            newCard._cardSetUp = posibleCards[randomInt];
            enemyCards.Add(newCard);
            print(newCard._cardSetUp);
        }
    }       

    private void CalculateListOfThreats()
    {

    }

    //0 torre abajo, 1 torre arriba, 2 torre grande
    public void PlayCard(int cardIndex, int towerIndex)
    {
        
        if (canDoTurn && currentTurn == TURN.ENEMY)
        {
            print("Juega carta.");
            canDoTurn = false;
        }
        
    }

    public void PlayCard(int cardIndex, GameObject spawn)
    {
        if (canDoTurn && currentTurn == TURN.ENEMY)
        {
            print("Juega carta.");
            canDoTurn = false;
        }
        
    }

    public void StoreTurn()
    {
        
        if (canDoTurn)
        {
            print("Guarda turno.");
            if (currentTurn == TURN.ALLY && allyTurnsMana < 2)
            {
                allyTurnsMana++;
            }
            else if (currentTurn == TURN.ENEMY && enemyTurnsMana < 2)
            {
                enemyTurnsMana++;
            }
            canDoTurn = false;
        }
     
    }


}
