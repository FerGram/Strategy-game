using UnityEngine;

[CreateAssetMenu(fileName = "CardSetUp", menuName = "ScriptableObjects/CardSetUp", order = 1)]
public class CardSetUp : ScriptableObject
{
    public enum CARD_TYPE
    {
        BOMB, ARCHER, GIANT, BARBARIAN
    }
    public GameObject _instantiablePrefab;
    [Space]

    public Sprite _cardSprite;
    public int _cardCost;
    public CARD_TYPE _cardType;

}
