using NinjaDomain.Classes;
using System.Data.Entity;

namespace NinjaDomain.DataModel
{
    public class NinjaContext : DbContext
    {
        public DbSet<Ninja> Ninjas { get; set; }

        public DbSet<NinjaEquipment> NinjaEquipments { get; set; }

        public DbSet<Clan> Clans { get; set; }
    }
}
