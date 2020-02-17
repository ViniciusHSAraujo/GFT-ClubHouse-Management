namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface ITicketRepository {
        int CountRemainingTicketsForAnEvent(int eventId);
        int CountTicketsSoldForAnEvent(int eventId);
        void MarkAsSold(int quantity, int eventId, int userId, int saleId);
    }
}