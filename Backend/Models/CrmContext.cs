using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public partial class CrmContext : DbContext
    {
        public CrmContext()
        {
        }

        public CrmContext(DbContextOptions<CrmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Email> Emails { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<LineItem> LineItems { get; set; } = null!;
        public virtual DbSet<OrderType> OrderTypes { get; set; } = null!;
        public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\jacks\\Creative Cloud Files\\Eurydice\\Eurydice.mdf\";Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Category1);

                entity.Property(e => e.Category1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Category");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Acquisition).HasMaxLength(50);

                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.Address3).HasMaxLength(50);

                entity.Property(e => e.Address4).HasMaxLength(50);

                entity.Property(e => e.Address5).HasMaxLength(50);

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.KickstarterUsername).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(60);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Emails_ToCustomers");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.GamefoundOrder)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.InvoiceTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.KickstarterTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Note).HasMaxLength(250);

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_ToCustomers");

                entity.HasOne(d => d.PaymentMethodNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.PaymentMethod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_PaymentTypes");
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Product)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.InvoiceNavigation)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.Invoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineItems_ToInvoices");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineItems_ToProducts");
            });

            modelBuilder.Entity<OrderType>(entity =>
            {
                entity.HasKey(e => e.Type)
                    .HasName("PK_OrderType");

                entity.Property(e => e.Type)
                    .HasMaxLength(12)
                    .IsFixedLength();
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.HasKey(e => e.Type)
                    .HasName("PK__tmp_ms_x__F9B8A48AED265C7D");

                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Sku)
                    .HasName("PK__Products__CA1ECF0C9A65D4A4");

                entity.Property(e => e.Sku)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SKU");

                entity.Property(e => e.Category)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cogs)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("COGS");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.KsPrice).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.WebPrice).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ToCategories");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
