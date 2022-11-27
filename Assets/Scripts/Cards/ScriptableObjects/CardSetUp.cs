using UnityEngine;

[CreateAssetMenu(fileName = "CardSetUp", menuName = "ScriptableObjects/CardSetUp", order = 1)]
public class CardSetUp : ScriptableObject
{
    public GameObject _instantiablePrefab;
    [Space]

    public Sprite _cardSprite;

    //We can add more things:
    //Mana cost,
    //Whatever...
}
