using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int gameDuration = 300;
    public enum TURN
    {
        ALLY, ENEMY, SIMULATION
    }
    public TURN currentTurn;
    [SerializeField] private bool canDoTurn = true;
    [SerializeField] private int turnDuration;
    [SerializeField] private int simDuration;
    [SerializeField] private float simSpeed;

    [SerializeField] public CardSetUp[] posibleCards;
    [SerializeField] public GameObject[] cardSpawners;
    [SerializeField] private int deckLength;

    public int[] listOfThreats;
    public List<GameObject> enemyUnits;
    public List<CardSetUp> enemyCards;
    [SerializeField] public GameObject[] enemyTowers;
    [SerializeField] public GameObject[] allyTowers;
    [SerializeField] private float spawnRadius;

    public int allyTurnsMana;   
    public int enemyTurnsMana;
    public float timer;
    public float turnTimer;
    public float simTimer;
    [SerializeField] TURN nextTurn;
    [SerializeField] GameObject timerObject;

    [SerializeField] private GameObject allyTurnCounter;
    [SerializeField] private GameObject enemyTurnCounter;
    [SerializeField] private GameObject allyTurnText;
    [SerializeField] private GameObject enemyTurnText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject iaStatsPanel;
    [SerializeField] public TextMeshProUGUI iaStatsText;

    private void Awake()
    {
        Time.timeScale = 0.5f;
        currentTurn = TURN.ALLY;
        nextTurn = TURN.ENEMY;
        timer = gameDuration;
        simTimer = simDuration;
        turnTimer = turnDuration;
        
        listOfThreats = new int[3];
        enemyCards = new List<CardSetUp>();
        CreateDeck();
        allyTurnsMana = 1;
        enemyTurnsMana = 1;
    }

    private void Update()
    {
        DisplayTime();
        DisplayTurns();
        SomeoneWins();
        CalculateListOfThreats();
        IaStatsActivated();
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
            StoreTurn();            
        }
        else if(simTimer <= 0.0f)
        {
            Time.timeScale = 0.5f;
            currentTurn = nextTurn;
            
            if(currentTurn == TURN.ALLY)
            {
                canDoTurn = true;
                nextTurn = TURN.ENEMY;
                if (allyTurnsMana == 0)
                    allyTurnsMana = 1;
            }
            else
            {
                nextTurn = TURN.ALLY;
               
                StartCoroutine("TakeDecission");
                if (enemyTurnsMana == 0)
                    enemyTurnsMana = 1;
            }
            simTimer = simDuration;
        }
        
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        timerObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);


    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void SomeoneWins()
    {
        if(enemyTowers[2] == null)
        {
            Time.timeScale = 0.0f;
            endPanel.SetActive(true);
            endText.GetComponent<TextMeshProUGUI>().text = "¡Has ganado! ^_^";
            retryButton.SetActive(true);
            
        }
        else
        {
            if(allyTowers[2] == null)
            {
                Time.timeScale = 0.0f;
                endText.GetComponent<TextMeshProUGUI>().text = "Has perdido... :(";
                endPanel.SetActive(true);
                retryButton.SetActive(true);
            }
        }
    }
    public void PassTurnCardDragged()
    {        
        currentTurn = TURN.SIMULATION;
        Time.timeScale = 1.0f;
        turnTimer = turnDuration;
    }
    private void CreateDeck()
    {
        for (int i = 0; i < deckLength; i++)
        {
            ChangeCard();
        }
    }     
    
    private void ChangeCard()
    {
        int randomInt = UnityEngine.Random.Range(0, posibleCards.Length);
        CardSetUp _cardSetUp = posibleCards[randomInt];

        enemyCards.Add(_cardSetUp);
        print(_cardSetUp);
        print(enemyCards[0]);
    }

    public void CalculateListOfThreats()
    {
        for (int i = 0; i < enemyTowers.Length; i++)
        {
            if (enemyTowers[i] != null)
            {
                enemyTowers[i].GetComponent<Tower>().RecalculateThreat();
                listOfThreats[i] = enemyTowers[i].GetComponent<Tower>().threatLevel;
            }
        }
    }

    //0 torre abajo, 1 torre arriba, 2 torre grande
    public void PlayCard(int cardIndex, int towerIndex)
    {
        
        if (canDoTurn && currentTurn == TURN.ENEMY)
        {
            print("Juega carta.");
            Vector2 randomPosition = new Vector2(enemyTowers[towerIndex].transform.position.x, enemyTowers[towerIndex].transform.position.y) + UnityEngine.Random.insideUnitCircle * spawnRadius;
            GameObject unit = Instantiate(enemyCards[cardIndex]._instantiablePrefab,randomPosition, Quaternion.identity);
            unit.tag = "Enemy";           
            canDoTurn = false;
            enemyTurnsMana -= enemyCards[cardIndex]._cardCost;
            enemyCards.RemoveAt(cardIndex);
            PassTurnCardDragged();
            ChangeCard();
        }
        
    }

    public void PlayCard(int cardIndex, GameObject spawn)
    {
        if (canDoTurn && currentTurn == TURN.ENEMY)
        {
            print("Juega carta. " + enemyCards[cardIndex]);
            Vector2 randomPosition = new Vector2(spawn.transform.position.x, spawn.transform.position.y) + UnityEngine.Random.insideUnitCircle * spawnRadius;
            GameObject unit = Instantiate(enemyCards[cardIndex]._instantiablePrefab, randomPosition, Quaternion.identity);
            unit.tag = "Enemy";
            //Coger otra carta nueva para ese hueco
            canDoTurn = false;
            enemyTurnsMana -= enemyCards[cardIndex]._cardCost;
            enemyCards.RemoveAt(cardIndex);
            PassTurnCardDragged();
            ChangeCard();
        }
        
    }

    public void StoreTurn()
    {
        
        if (canDoTurn)
        {
           
            if (currentTurn == TURN.ALLY && allyTurnsMana < 2)
            {
                allyTurnsMana++;
                iaStatsText.text += "\nGuarda aliado.";
            }
            else if (currentTurn == TURN.ENEMY && enemyTurnsMana < 2)
            {
                enemyTurnsMana++;
                iaStatsText.text += "\nGuarda enemigo.";
            }
            canDoTurn = false;
            currentTurn = TURN.SIMULATION;
            Time.timeScale = 1.0f;
            turnTimer = turnDuration;
        }

     
    }

    private void IaStatsActivated()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !iaStatsPanel.activeInHierarchy) iaStatsPanel.SetActive(true);

        else if (Input.GetKeyDown(KeyCode.Space) && iaStatsPanel.activeInHierarchy) iaStatsPanel.SetActive(false);
    }


    IEnumerator TakeDecission()
    {
        Debug.Log("Start make decission");
        float randomDecissionTime = UnityEngine.Random.Range(0.5f, turnDuration - 0.5f);
        yield return new WaitForSeconds(randomDecissionTime);
        canDoTurn = true;
    }

}
