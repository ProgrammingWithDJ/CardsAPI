using CardsAPi.Models;

namespace WebApplication1.Interfaces
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetCardsAsync();
        void AddCard(Card card);

        void DeleteCard(Guid cardId);

        Task<Card> FindCard(Guid Id);
    }
}
