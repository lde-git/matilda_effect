using UnityEngine;

public interface ICardDropArea
{
    void OnCardDrop(Card card);
    void OnCardRemoved(Card card);

    bool CanDropCard(Card card);

}
