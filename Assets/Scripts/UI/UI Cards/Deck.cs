using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour
{
    [SerializeField] int _maxCardAmount = 4;
    [SerializeField] GameObject _cardPrefab;
    [Space]
    [SerializeField] GameObject _spawnAreaPanels;


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

    public void ActivateSpawnPanels(bool active)
    {
        if (_spawnAreaPanels != null)
        {
            if (active) _spawnAreaPanels.SetActive(true);
            else _spawnAreaPanels.SetActive(false);
        }
    }
}
