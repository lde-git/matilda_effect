using System;
using UnityEngine;

public class Area : MonoBehaviour, ICardDropArea
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CardType areaType;
    public bool hasCard = false;
    public bool hasCorrectCard = false;
    public Card currentCard;


    private void OnValidate()
    {
        this.name = $"Area_{transform.GetSiblingIndex()}_{areaType}";
    }


    public void OnCardDrop(Card card) {

        card.transform.SetParent(this.transform);
        //in layer set card above target for mouse collision
        card.transform.position = transform.position + new Vector3(0,0,-1);
        this.hasCard = true;
        this.currentCard = card;
        if (this.areaType != card.cardType) { 
            Debug.Log($"wrong {card.name} inserted");
            return;
        }

        this.hasCorrectCard = true;
        Debug.Log($"correct {card.name} inserted");
        GameManager.Instance.OnCardPlaced();
    }


    public bool CanDropCard(Card card) {
        if (hasCard)
        {
            Debug.Log($"already has {this.currentCard.name} inserted \ncant insert {card.name}");
            return false;
        }
        return true;
    }

    public void OnCardRemoved(Card card)
    {
        this.hasCard = false;
        this.currentCard = null;
        this.hasCorrectCard = false;
        Debug.Log($"{card.name} removed");
    }
}
