using Domain;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class ShippingDbContext : DbContext
    {
        public ShippingDbContext()
        {
        }

        public ShippingDbContext(DbContextOptions<ShippingDbContext> options) : base(options) { }

 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DILARA;Database=DotnetShippingApi;Trusted_Connection=True;Encrypt=False");
            }
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<CarrierConfiguration> CarrierConfigurations { get; set; }
        public DbSet<CarrierReport> CarrierReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary Keys
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<Carrier>().HasKey(c => c.CarrierId);
            modelBuilder.Entity<CarrierConfiguration>().HasKey(cc => cc.CarrierConfigurationId);
            modelBuilder.Entity<CarrierReport>().HasKey(cr => cr.CarrierReportId);

            // Carrier Entity Configuration
            modelBuilder.Entity<Carrier>()
                .Property(c => c.CarrierName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Carrier>()
                .Property(c => c.CarrierPlusDesiCost)
                .HasPrecision(18, 2)
                .IsRequired();

            modelBuilder.Entity<Carrier>()
                .Property(c => c.CarrierIsActive)
                .IsRequired();

            //Carrier - CarrierConfiguration Relationship
            modelBuilder.Entity<Carrier>()
                .HasMany(c => c.CarrierConfigurations)
                .WithOne(cc => cc.Carrier)
                .HasForeignKey(cc => cc.CarrierId)
                .OnDelete(DeleteBehavior.Cascade); // Carrier silindiğinde konfigürasyonlar da silinir.

            // CarrierConfiguration Entity Configuration
            modelBuilder.Entity<CarrierConfiguration>()
                .Property(cc => cc.CarrierMinDesi)
               .IsRequired();

            modelBuilder.Entity<CarrierConfiguration>()
                .Property(cc => cc.CarrierMaxDesi)
               .IsRequired();

            modelBuilder.Entity<CarrierConfiguration>()
                .Property(cc => cc.CarrierCost)
                .HasPrecision(18, 2)
                .IsRequired();

            // Order Entity Configuration
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDesi)
               .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderCarrierCost)
                .HasPrecision(18, 2)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .IsRequired();

            // Order - Carrier Relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Carrier)
                .WithMany()
                .HasForeignKey(o => o.CarrierId)
                .OnDelete(DeleteBehavior.Restrict); // Carrier silinse bile Order kayıtları korunur.

            modelBuilder.Entity<CarrierReport>()
               .HasOne(cr => cr.Carrier)
               .WithMany()
               .HasForeignKey(cr => cr.CarrierId);
        }
    }
}
