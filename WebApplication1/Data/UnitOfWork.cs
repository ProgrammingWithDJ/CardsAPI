using CardsAPi.Data;
using WebApplication1.Data.Repo;
using WebApplication1.Interfaces;

namespace WebApplication1.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CardsDbContext dc;

        public UnitOfWork(CardsDbContext dc)
        {
            this.dc = dc;
        }
        public ICardRepository cardRepository => new CardRepository(dc);

        public async Task<bool> SaveChangesAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
