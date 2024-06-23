using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        var customers = new List<Customer>
        {
            new Customer { Id = Guid.NewGuid(), Name = "Ahmet Yılmaz", CompanyName = "Yılmaz Ticaret", Address = "İstanbul", Phone = "05321234567", Email = "ahmet@yilmaz.com", ContactPerson = "Ahmet Yılmaz", Sector = "Gıda", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Mehmet Öz", CompanyName = "Öz İnşaat", Address = "Ankara", Phone = "05329876543", Email = "mehmet@ozinsaat.com", ContactPerson = "Mehmet Öz", Sector = "İnşaat", ShoppingArea = "Toplu Satış", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Ayşe Demir", CompanyName = "Demir Tekstil", Address = "İzmir", Phone = "05331234567", Email = "ayse@demir.com", ContactPerson = "Ayşe Demir", Sector = "Tekstil", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Fatma Kaya", CompanyName = "Kaya Elektronik", Address = "Bursa", Phone = "05345678901", Email = "fatma@kaya.com", ContactPerson = "Fatma Kaya", Sector = "Elektronik", ShoppingArea = "Toplu Satış", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Hasan Çelik", CompanyName = "Çelik Mobilya", Address = "Adana", Phone = "05346789012", Email = "hasan@celik.com", ContactPerson = "Hasan Çelik", Sector = "Mobilya", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Emine Aslan", CompanyName = "Aslan Otomotiv", Address = "Antalya", Phone = "05347890123", Email = "emine@aslan.com", ContactPerson = "Emine Aslan", Sector = "Otomotiv", ShoppingArea = "Toplu Satış", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Hüseyin Koç", CompanyName = "Koç Bilgisayar", Address = "Konya", Phone = "05348901234", Email = "huseyin@koc.com", ContactPerson = "Hüseyin Koç", Sector = "Bilgisayar", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Selin Öztürk", CompanyName = "Öztürk Medikal", Address = "Gaziantep", Phone = "05349012345", Email = "selin@ozturk.com", ContactPerson = "Selin Öztürk", Sector = "Medikal", ShoppingArea = "Toplu Satış", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Cemre Polat", CompanyName = "Polat Gıda", Address = "Kayseri", Phone = "05340123456", Email = "cemre@polat.com", ContactPerson = "Cemre Polat", Sector = "Gıda", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Eren Aydın", CompanyName = "Aydın İnşaat", Address = "Samsun", Phone = "05341234567", Email = "eren@aydin.com", ContactPerson = "Eren Aydın", Sector = "İnşaat", ShoppingArea = "Toplu Satış", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Esra Şahin", CompanyName = "Şahin Tekstil", Address = "Denizli", Phone = "05342345678", Email = "esra@sahin.com", ContactPerson = "Esra Şahin", Sector = "Tekstil", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Ali Vural", CompanyName = "Vural Elektronik", Address = "Eskişehir", Phone = "05343456789", Email = "ali@vural.com", ContactPerson = "Ali Vural", Sector = "Elektronik", ShoppingArea = "Toplu Satış", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Buse Korkmaz", CompanyName = "Korkmaz Mobilya", Address = "Mersin", Phone = "05344567890", Email = "buse@korkmaz.com", ContactPerson = "Buse Korkmaz", Sector = "Mobilya", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Onur Çakır", CompanyName = "Çakır Otomotiv", Address = "Şanlıurfa", Phone = "05345678901", Email = "onur@cakir.com", ContactPerson = "Onur Çakır", Sector = "Otomotiv", ShoppingArea = "Toplu Satış", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "İlayda Güneş", CompanyName = "Güneş Bilgisayar", Address = "Tekirdağ", Phone = "05346789012", Email = "ilayda@gunes.com", ContactPerson = "İlayda Güneş", Sector = "Bilgisayar", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Burak Demirtaş", CompanyName = "Demirtaş Medikal", Address = "Trabzon", Phone = "05347890123", Email = "burak@demirtas.com", ContactPerson = "Burak Demirtaş", Sector = "Medikal", ShoppingArea = "Toplu Satış", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Nisa Aksoy", CompanyName = "Aksoy Gıda", Address = "Malatya", Phone = "05348901234", Email = "nisa@aksoy.com", ContactPerson = "Nisa Aksoy", Sector = "Gıda", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Orhan Güngör", CompanyName = "Güngör İnşaat", Address = "Sivas", Phone = "05349012345", Email = "orhan@gungor.com", ContactPerson = "Orhan Güngör", Sector = "İnşaat", ShoppingArea = "Toplu Satış", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Pelin Yıldırım", CompanyName = "Yıldırım Tekstil", Address = "Van", Phone = "05340123456", Email = "pelin@yildirim.com", ContactPerson = "Pelin Yıldırım", Sector = "Tekstil", ShoppingArea = "Perakende", IsDeleted = false },
            new Customer { Id = Guid.NewGuid(), Name = "Serkan Uçar", CompanyName = "Uçar Elektronik", Address = "Aydın", Phone = "05341234567", Email = "serkan@ucar.com", ContactPerson = "Serkan Uçar", Sector = "Elektronik", ShoppingArea = "Toplu Satış", IsDeleted = false }
        };

        builder.HasData(customers);
        builder.HasMany(x => x.Sales)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}