using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class Card : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CardSetUp _cardSetUp;
    [SerializeField] float _mouseHoverScale = 1.25f;
    [SerializeField] float _mouseHoverTweenDuration = 0.2f;

    private RectTransform _rectTransform;
    private Vector3 _defaultPosition;
    private Vector3 _defaultScale;
    private GameManager _gameManager;

    public void SetCardSetup(CardSetUp setup)
    {
        _cardSetUp = setup;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultScale = _rectTransform.localScale;

        GetComponent<Image>().sprite = _cardSetUp._cardSprite;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("ALLY MANA: " + _gameManager.allyTurnsMana.ToString());
        Debug.Log("ALLY CARD COST: " + _cardSetUp._cardCost.ToString());
        if (_gameManager.currentTurn == GameManager.TURN.ALLY && _gameManager.allyTurnsMana >= _cardSetUp._cardCost)
        {
            _defaultPosition = _rectTransform.position;
            
        }        
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_gameManager.currentTurn == GameManager.TURN.ALLY && _gameManager.allyTurnsMana >= _cardSetUp._cardCost)
        {
            _rectTransform.position = Input.mousePosition;
        }    
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_gameManager.currentTurn == GameManager.TURN.ALLY && _gameManager.allyTurnsMana >= _cardSetUp._cardCost)
        {
            
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            bool insideBattleField = true;

            foreach (var rayResult in results)
            {
                if (rayResult.gameObject.GetComponent<Deck>())
                {
                    insideBattleField = false;
                    _rectTransform.position = _defaultPosition;
                    break;
                }
            }
            if (insideBattleField)
            {
                Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                spawnPos.z = 1;

                //TODO: set parent (extra argument)
                Instantiate(_cardSetUp._instantiablePrefab,
                            spawnPos,
                            Quaternion.identity);
                GameObject objToSpawn = new GameObject();
                objToSpawn.AddComponent<Image>();
                objToSpawn.AddComponent<Card>();
                objToSpawn.GetComponent<Card>()._cardSetUp = _gameManager.posibleCards[UnityEngine.Random.Range(0, _gameManager.posibleCards.Length)];
                objToSpawn.GetComponent<Card>()._mouseHoverScale = 1.25f;
                objToSpawn.GetComponent<Card>()._mouseHoverTweenDuration = 0.2f;
                objToSpawn.GetComponent<Image>().sprite = objToSpawn.GetComponent<Card>()._cardSetUp._cardSprite;

                objToSpawn.transform.parent = _gameManager.cardSpawners[0].transform;
                Instantiate(objToSpawn, _gameManager.cardSpawners[0].transform.position, _gameManager.cardSpawners[0].transform.rotation);

                Destroy(gameObject);
                _gameManager.allyTurnsMana -= _cardSetUp._cardCost;
                _gameManager.PassTurnCardDragged();
            }
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 newScale = _defaultScale * _mouseHoverScale;
        _rectTransform.DOScale(newScale, _mouseHoverTweenDuration).SetEase(Ease.InOutQuint);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.DOScale(_defaultScale, _mouseHoverTweenDuration).SetEase(Ease.InOutQuint);
    }

    
}
