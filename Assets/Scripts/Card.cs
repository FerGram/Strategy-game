using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] CardSetUp _cardSetUp;
    [SerializeField] float _mouseHoverScale = 1.25f;
    [SerializeField] float _mouseHoverTweenDuration = 0.2f;

    private RectTransform _rectTransform;
    private Vector3 _defaultPosition;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultScale = _rectTransform.localScale;

        GetComponent<Image>().sprite = _cardSetUp._cardSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _defaultPosition = _rectTransform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
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

            Destroy(gameObject);
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
