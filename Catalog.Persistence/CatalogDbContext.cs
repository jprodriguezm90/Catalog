using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Brand> Brands => Set<Brand>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.OwnsMany(p => p.Stocks, sb =>
            {
                sb.WithOwner().HasForeignKey("ProductId");
                sb.Property<Guid>("Id");
                sb.HasKey("Id");
                sb.Property(s => s.Size).IsRequired().HasMaxLength(5);
                sb.Property(s => s.InStore);
                sb.Property(s => s.Online);
                sb.ToTable("ProductStocks");
                // seed owned collection rows (example entries)
                sb.HasData(
                    new { Id = new Guid("57904706-5617-43F7-93B1-1693C11CBA99"), ProductId = new Guid("CF198219-DA46-4264-B5ED-7565A56D26FE"), Size = "S", InStore = 10, Online = 50 },
                    new { Id = new Guid("2B068228-D46C-4CBB-BC37-DB70F1B284C9"), ProductId = new Guid("CF198219-DA46-4264-B5ED-7565A56D26FE"), Size = "M", InStore = 10, Online = 50 },
                    new { Id = new Guid("03A001E7-168D-43A5-A599-A07B1AD3A692"), ProductId = new Guid("CF198219-DA46-4264-B5ED-7565A56D26FE"), Size = "L", InStore = 10, Online = 50 },
                    
                    new { Id = new Guid("6BC34F4B-6603-47B4-9D7A-1E1091BA9D0E"), ProductId = new Guid("59842595-1129-439E-A84F-DFF86C261A3C"), Size = "S", InStore = 10, Online = 50 },
                    new { Id = new Guid("8CB80B90-A919-4EE6-B0F1-426C7B0EF189"), ProductId = new Guid("59842595-1129-439E-A84F-DFF86C261A3C"), Size = "M", InStore = 10, Online = 50 },
                    new { Id = new Guid("E537F5F6-DB4E-46A5-8595-8644B13265BF"), ProductId = new Guid("59842595-1129-439E-A84F-DFF86C261A3C"), Size = "L", InStore = 10, Online = 50 },
                    
                    new { Id = new Guid("932AA8EF-59A6-47DA-AD4C-367C2DB2F5A2"), ProductId = new Guid("99199924-E42C-4DCF-A2EA-DC482BC3C7BE"), Size = "S", InStore = 10, Online = 50 },
                    new { Id = new Guid("15FAD713-5F5B-4C79-90E4-CC0D64EACF4B"), ProductId = new Guid("99199924-E42C-4DCF-A2EA-DC482BC3C7BE"), Size = "M", InStore = 10, Online = 50 },
                    new { Id = new Guid("3546C25A-03C4-4FD1-93DD-FAEF228A6035"), ProductId = new Guid("99199924-E42C-4DCF-A2EA-DC482BC3C7BE"), Size = "L", InStore = 10, Online = 50 },
                    
                    new { Id = new Guid("666EB407-DC0C-4DB7-BA61-F63B013CEF82"), ProductId = new Guid("4DB2C7D5-FC02-4BC4-A245-9F0A5396083A"), Size = "S", InStore = 10, Online = 50 },
                    new { Id = new Guid("DC8DFA43-4CF6-4106-9B42-DE66C0CDA92E"), ProductId = new Guid("4DB2C7D5-FC02-4BC4-A245-9F0A5396083A"), Size = "M", InStore = 10, Online = 50 },
                    new { Id = new Guid("EB58CDD8-0672-473A-B403-B695A2527D8D"), ProductId = new Guid("4DB2C7D5-FC02-4BC4-A245-9F0A5396083A"), Size = "L", InStore = 10, Online = 50 },
                    
                    new { Id = new Guid("5C787691-F278-4FD7-BE10-5AFC6BDA1292"), ProductId = new Guid("0064DB26-87A7-4DA2-AA13-C72096303B10"), Size = "S", InStore = 10, Online = 50 },
                    new { Id = new Guid("3AEDD23A-4E5E-46C3-9C0E-110FB4392D55"), ProductId = new Guid("0064DB26-87A7-4DA2-AA13-C72096303B10"), Size = "M", InStore = 10, Online = 50 },
                    new { Id = new Guid("93D82DA8-8CF7-496F-85ED-8B8515607BAE"), ProductId = new Guid("0064DB26-87A7-4DA2-AA13-C72096303B10"), Size = "L", InStore = 10, Online = 50 },

                    new { Id = new Guid("E0BD8859-5EA7-46D8-B95A-BC4F0DF8B411"), ProductId = new Guid("A4AA9101-5802-4EC9-A556-468A784DCFF6"), Size = "S", InStore = 10, Online = 50 },
                    new { Id = new Guid("C453BAFC-DC2E-4B7E-A94C-F234B91C57F7"), ProductId = new Guid("A4AA9101-5802-4EC9-A556-468A784DCFF6"), Size = "M", InStore = 10, Online = 50 },
                    new { Id = new Guid("0F62F318-C85F-4B9A-8580-A07321C57D85"), ProductId = new Guid("A4AA9101-5802-4EC9-A556-468A784DCFF6"), Size = "L", InStore = 10, Online = 50 },

                    new { Id = new Guid("EFDF260F-EF62-4E98-ACCC-BDE0474ACCA8"), ProductId = new Guid("097C365A-F057-4DE1-AAAF-829516C25467"), Size = "6", InStore = 10, Online = 50 },
                    new { Id = new Guid("FC8AFF80-ADF7-4232-97DE-6309DD767352"), ProductId = new Guid("097C365A-F057-4DE1-AAAF-829516C25467"), Size = "9", InStore = 10, Online = 50 },
                    new { Id = new Guid("7EEC7336-179E-44A1-A3CA-809C2D988ACA"), ProductId = new Guid("097C365A-F057-4DE1-AAAF-829516C25467"), Size = "10", InStore = 10, Online = 50 },

                    new { Id = new Guid("F379D3EA-9B35-42B7-B659-E78B3F1B0E96"), ProductId = new Guid("8A8038F9-9DF9-4A7B-A76E-414AE1312827"), Size = "U", InStore = 10, Online = 50 }
                );
            });

            entity.HasOne(p => p.Brand)
                .WithMany();

            entity.HasOne(p => p.Category)
                .WithMany();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        //seed data, added through migrations
        var shirtGuid = Guid.Parse("{A0530E8F-C985-461D-B5CA-CCE60ADEBE9E}");
        var pantsGuid = Guid.Parse("{2ABD90B8-845A-45F3-8081-9A49198105FB}");
        var shoesGuid = Guid.Parse("{79FBD40C-96B0-4DAA-A53B-0664D3A626D0}");
        var capsGuid = Guid.Parse("{E9EF65EF-A066-427D-88B0-7C7845B9C693}");

        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = shirtGuid,
            Name = "Shirts",
            Description = """Shirts are a type of clothing worn on the upper body, typically made of fabric and designed to cover the torso and arms. They come in various styles, such as dress shirts, casual shirts, and t-shirts, and can be made from different materials like cotton, linen, or synthetic fabrics."""
        });

        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = pantsGuid,
            Name = "Pants",
            Description = """Pants are a type of clothing worn on the lower body, covering the legs and typically extending from the waist to the ankles. They come in various styles, such as jeans, trousers, leggings, and shorts, and can be made from different materials like denim, cotton, or synthetic fabrics."""

        });
        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = shoesGuid,
            Name = "Shoes",
            Description = """Shoes are a type of footwear designed to protect and provide comfort to the feet while walking, running, or engaging in various activities. They come in various styles, such as sneakers, boots, sandals, and dress shoes, and can be made from different materials like leather, canvas, or synthetic fabrics."""

        });
        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = capsGuid,
            Name = "Caps",
            Description = """Caps are a type of headwear that typically features a rounded crown and a visor or brim at the front. They are designed to provide shade and protection from the sun, as well as to serve as a fashion accessory. Caps come in various styles, such as baseball caps, snapbacks, and fitted caps, and can be made from different materials like cotton, wool, or synthetic fabrics."""
        });

        var gymSharkGuid = Guid.Parse("{878BC0AF-6692-43F2-A131-32E5D6A22532}");
        var adidasGuid = Guid.Parse("{254D8BB7-FD25-4C80-AB2F-24ACBCAB94C8}");
        var nikeGuid = Guid.Parse("{6CA12909-23A9-4B62-A519-53E8E803E387}");
        var diadoraGuid = Guid.Parse("{B56A84D8-F6D7-4831-BC43-EB6B087F63B9}");

        modelBuilder.Entity<Brand>().HasData(new Brand
        {
            Id = gymSharkGuid,
            Name = "GymShark",
            Description = """GymShark is a fitness apparel, manufacturer & online retailer based in the United Kingdom."""
        });
        modelBuilder.Entity<Brand>().HasData(new Brand
        {
            Id = adidasGuid,
            Name = "Adidas",
            Description = """Adidas is a global German company that makes shoes, clothes, and sports gear."""
        });
        modelBuilder.Entity<Brand>().HasData(new Brand
        {
            Id = nikeGuid,
            Name = "Nike",
            Description = """Nike is the world's leading sports brand, designing and selling athletic footwear, apparel, and equipment."""
        });
        modelBuilder.Entity<Brand>().HasData(new Brand
        {
            Id = diadoraGuid,
            Name = "Diadora",
            Description = """Diadora is an Italian sportswear and footwear company founded in 1948 in Caerano di San Marco, Italy."""
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = new Guid("CF198219-DA46-4264-B5ED-7565A56D26FE"),
            Name = "Cottom Sweat Shirt",
            Price = 29.99M,
            BrandId = gymSharkGuid,
            CategoryId = shirtGuid

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = new Guid("59842595-1129-439E-A84F-DFF86C261A3C"),
            Name = "Cottom Sweat Jogger",
            Price = 39.99M,
            BrandId = gymSharkGuid,
            CategoryId = pantsGuid

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = new Guid("99199924-E42C-4DCF-A2EA-DC482BC3C7BE"),
            Name = "Dry Sweat Shirt",
            Price = 49.99M,
            BrandId = adidasGuid,
            CategoryId = shirtGuid

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = new Guid("4DB2C7D5-FC02-4BC4-A245-9F0A5396083A"),
            Name = "Dry Sweat Jogger",
            Price = 59.99M,
            BrandId = adidasGuid,
            CategoryId = pantsGuid
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = new Guid("8A8038F9-9DF9-4A7B-A76E-414AE1312827"),
            Name = "Baseball Cap Nike",
            Price = 19.99M,
            BrandId = nikeGuid,
            CategoryId = capsGuid
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = new Guid("097C365A-F057-4DE1-AAAF-829516C25467"),
            Name = "Pegasus Nike Runners",
            Price = 99.99M,
            BrandId = nikeGuid,
            CategoryId = shoesGuid

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = new Guid("0064DB26-87A7-4DA2-AA13-C72096303B10"),
            Name = "Cottom Sweat Jogger",
            Price = 39.99M,
            BrandId = diadoraGuid,
            CategoryId = pantsGuid

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = new Guid("A4AA9101-5802-4EC9-A556-468A784DCFF6"),
            Name = "Cottom Sweat Shirt",
            Price = 29.99M,
            BrandId = diadoraGuid,
            CategoryId = shirtGuid

        });




    }

}
