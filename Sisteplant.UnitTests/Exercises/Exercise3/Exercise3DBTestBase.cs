using Microsoft.EntityFrameworkCore;
using Sisteplant.Domain.Exercises.Exercise3;
using Sisteplant.Infrastructure.Exercises;

namespace Sisteplant.UnitTests.Exercises.Exercise3
{

    public abstract class Exercise3DBTestBase
    {

        protected SisteplantContext CreateInMemoryDatabase()
        {
            var context = new SisteplantContext(
                new DbContextOptionsBuilder<SisteplantContext>().UseInMemoryDatabase("SisteplantTestDb").Options);

            // Return the context to be used later
            return context;
        }

        protected void SeedDatabase(SisteplantContext context, string scenario)
        {
            context.Database.EnsureCreated();
            ClearDatabase(context);
            switch (scenario)
            {
                case "Returning1Distributor":
                    PopulateReturning1Distributor(context);
                    break;

                case "Returning2Distributors":
                    PopulateReturning2Distributors(context);
                    break;

                case "ReturningEmptyList":
                    PopulateReturningEmptyList(context);
                    break;

                default:
                    throw new ArgumentException("Unknown scenario: " + scenario);
            }

            context.SaveChanges();
        }
        private void ClearDatabase(SisteplantContext context)
        {
            context.Items.RemoveRange(context.Items);
            context.Orders.RemoveRange(context.Orders);
            context.SaveChanges();
        }

        protected void PopulateReturning1Distributor(SisteplantContext context)
        {
            context.Items.AddRange(new List<Item>
        {
            new Item { ItemCode = 10091, ItemName = "juice" },
            new Item { ItemCode = 10092, ItemName = "chocolate" },
            new Item { ItemCode = 10093, ItemName = "cookies" },
            new Item { ItemCode = 10094, ItemName = "cake" },
        });

            context.Orders.AddRange(new List<Order>
        {
            new Order { DistributorId = 501, ItemOrdered = 10091, ItemQuantity = 250 },
            new Order { DistributorId = 502, ItemOrdered = 10093, ItemQuantity = 100 },
            new Order { DistributorId = 503, ItemOrdered = 10091, ItemQuantity = 200 },
            new Order { DistributorId = 502, ItemOrdered = 10091, ItemQuantity = 150 },
            new Order { DistributorId = 502, ItemOrdered = 10092, ItemQuantity = 300 },
            new Order { DistributorId = 504, ItemOrdered = 10094, ItemQuantity = 200 },
            new Order { DistributorId = 503, ItemOrdered = 10093, ItemQuantity = 250 },
            new Order { DistributorId = 503, ItemOrdered = 10092, ItemQuantity = 250 },
            new Order { DistributorId = 501, ItemOrdered = 10094, ItemQuantity = 180 },
            new Order { DistributorId = 503, ItemOrdered = 10094, ItemQuantity = 350 }
        });
        }

        protected void PopulateReturning2Distributors(SisteplantContext context)
        {
            context.Items.AddRange(new List<Item>
        {
            new Item { ItemCode = 10091, ItemName = "juice" },
            new Item { ItemCode = 10092, ItemName = "chocolate" },
            new Item { ItemCode = 10093, ItemName = "cookies" },
            new Item { ItemCode = 10094, ItemName = "cake" },
        });

            context.Orders.AddRange(new List<Order>
        {
            new Order { DistributorId = 501, ItemOrdered = 10091, ItemQuantity = 250 },
            new Order { DistributorId = 501, ItemOrdered = 10093, ItemQuantity = 100 },
            new Order { DistributorId = 503, ItemOrdered = 10091, ItemQuantity = 200 },
            new Order { DistributorId = 502, ItemOrdered = 10091, ItemQuantity = 150 },
            new Order { DistributorId = 501, ItemOrdered = 10092, ItemQuantity = 300 },
            new Order { DistributorId = 504, ItemOrdered = 10094, ItemQuantity = 200 },
            new Order { DistributorId = 503, ItemOrdered = 10093, ItemQuantity = 250 },
            new Order { DistributorId = 503, ItemOrdered = 10092, ItemQuantity = 250 },
            new Order { DistributorId = 501, ItemOrdered = 10094, ItemQuantity = 180 },
            new Order { DistributorId = 503, ItemOrdered = 10094, ItemQuantity = 350 }
        });
        }

        protected void PopulateReturningEmptyList(SisteplantContext context)
        {
            context.Items.AddRange(new List<Item>
        {
            new Item { ItemCode = 10091, ItemName = "juice" },
            new Item { ItemCode = 10092, ItemName = "chocolate" },
            new Item { ItemCode = 10093, ItemName = "cookies" },
            new Item { ItemCode = 10094, ItemName = "cake" },
        });

            context.Orders.AddRange(new List<Order>
        {
            new Order { DistributorId = 501, ItemOrdered = 10091, ItemQuantity = 250 },
            new Order { DistributorId = 501, ItemOrdered = 10093, ItemQuantity = 100 }
        });
        }

    }
}
