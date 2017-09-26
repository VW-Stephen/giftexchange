using Microsoft.EntityFrameworkCore;

namespace giftexchange.Models
{
    /// <summary>
    /// DbContext for accessing GiftExchange related data
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }

        public DbSet<GiftExchange> GiftExchanges { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
