namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface ITicketRepository {
        void MarkAsSold(int quantity, int eventId, int userId, int saleId);
    }
}