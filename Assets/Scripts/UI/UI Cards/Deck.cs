using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] int _maxCardAmount = 4;
    [SerializeField] GameObject _cardPrefab;


    public void AddCard(CardSetUp setup)
    {
        if (transform.childCount > _maxCardAmount)
        {
            Debug.LogWarning("Had more cards than the max amount permitted. Removing extra cards...");

            while (transform.childCount > _maxCardAmount)
            {
                Destroy(transform.GetChild(0));
            }
        }
        else if (transform.childCount == _maxCardAmount)
        {
            Debug.LogWarning("Tried adding a card but max amount reached");
        }
        else
        {
            GameObject card = Instantiate(_cardPrefab, transform);
            card.GetComponent<Card>().SetCardSetup(setup);
        }
    }
}
