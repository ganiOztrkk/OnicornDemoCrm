using Domain.Entities;
using Infastructure.Context;

namespace WebApi.Middlewares
{
    public static class SeedSalesMiddleware
    {
        public static void SeedSales(WebApplication app)
        {
            using var scoped = app.Services.CreateScope();
            var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            // Retrieve customer and salesperson IDs
            var customerIds = context.Customers.Select(c => c.Id).ToList();
            
            var salesPersonRole = context.Roles.FirstOrDefault(r => r.Name == "salesperson");
            var salesPersonIds = context.UserRoles
                .Where(ur => ur.RoleId == salesPersonRole!.Id)
                .Select(ur => ur.UserId)
                .ToList();

            if (customerIds.Count == 0 || salesPersonIds.Count == 0) return;

            var random = new Random();
            var sales = new List<Sale>();

            for (int i = 0; i < 20; i++)
            {
                var sale = new Sale
                {
                    Id = Guid.NewGuid(),
                    Description = $"Satış açıklaması {i + 1}",
                    Amount = random.Next(100, 10000),
                    Date = DateTime.Now.AddDays(-random.Next(1, 100)),
                    IsPaid = random.Next(0, 2) == 1,
                    CustomerId = customerIds[random.Next(customerIds.Count)],
                    SalesPersonId = salesPersonIds[random.Next(salesPersonIds.Count)],
                    IsDeleted = false
                };

                sales.Add(sale);
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }
    }
}
