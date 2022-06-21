namespace WebApplication1.Interfaces
{
    public interface IUnitOfWork 
    {
        ICardRepository cardRepository { get; }

        Task<bool> SaveChangesAsync();
    }
}
