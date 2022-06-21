using CardsAPi.Data;
using CardsAPi.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;

namespace WebApplication1.Data.Repo
{
    public class CardRepository : ICardRepository
    {
        private readonly CardsDbContext dc;
        public CardRepository(CardsDbContext dc)
        {
            this.dc = dc;
        }
        public void AddCard(Card card)
        {
            dc.Cards.Add(card);
        }

        public void DeleteCard(int cardId)
        {
            var card = dc.Cards.Find(cardId);
            dc.Cards.Remove(card);
        }

        public async Task<Card> FindCard(Guid Id)
        {
            return await dc.Cards.FindAsync(Id);
        }

        public async Task<IEnumerable<Card>> GetCardsAsync()
        {
            return await dc.Cards.ToListAsync();
        }
    }
}
