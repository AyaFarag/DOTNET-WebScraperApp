using Classification.Domain.Enttities;
using Microsoft.EntityFrameworkCore;

namespace Classification.Infrastructure.Persistence.Data.Seeding
{
    public static class ClassificationCategoryConfiguration
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassificationCategory>()
                .HasData(

                    new
                    {
                        Id = ClassificationTaxonomySeed.Food,
                        Code = "FOOD",
                        Name = "Food & Beverages",
                        ParentId = (Guid?)null,
                        Level = 0,
                        IsActive = true
                    },

                    new
                    {
                        Id = ClassificationTaxonomySeed.Beverages,
                        Code = "BEVERAGES",
                        Name = "Beverages",
                        ParentId =
                            ClassificationTaxonomySeed.Food,
                        Level = 1,
                        IsActive = true
                    },

                    new
                    {
                        Id = ClassificationTaxonomySeed.Water,
                        Code = "WATER",
                        Name = "Water",
                        ParentId =
                            ClassificationTaxonomySeed.Beverages,
                        Level = 2,
                        IsActive = true
                    },

                    new
                    {
                        Id = ClassificationTaxonomySeed.BottledWater,
                        Code = "BOTTLED_WATER",
                        Name = "Bottled Water",
                        ParentId =
                            ClassificationTaxonomySeed.Water,
                        Level = 3,
                        IsActive = true
                    },

                    new
                    {
                        Id = ClassificationTaxonomySeed.SoftDrinks,
                        Code = "SOFT_DRINKS",
                        Name = "Soft Drinks",
                        ParentId =
                            ClassificationTaxonomySeed.Beverages,
                        Level = 2,
                        IsActive = true
                    },

                    new
                    {
                        Id = ClassificationTaxonomySeed.Juice,
                        Code = "JUICE",
                        Name = "Juice",
                        ParentId =
                            ClassificationTaxonomySeed.Beverages,
                        Level = 2,
                        IsActive = true
                    },

                    new
                    {
                        Id = ClassificationTaxonomySeed.Dairy,
                        Code = "DAIRY",
                        Name = "Dairy",
                        ParentId =
                            ClassificationTaxonomySeed.Food,
                        Level = 1,
                        IsActive = true
                    },

                    new
                    {
                        Id = ClassificationTaxonomySeed.Milk,
                        Code = "MILK",
                        Name = "Milk",
                        ParentId =
                            ClassificationTaxonomySeed.Dairy,
                        Level = 2,
                        IsActive = true
                    },

                    new
                    {
                        Id = ClassificationTaxonomySeed.Cheese,
                        Code = "CHEESE",
                        Name = "Cheese",
                        ParentId =
                            ClassificationTaxonomySeed.Dairy,
                        Level = 2,
                        IsActive = true
                    },

                    new
                    {
                        Id = ClassificationTaxonomySeed.Yogurt,
                        Code = "YOGURT",
                        Name = "Yogurt",
                        ParentId =
                            ClassificationTaxonomySeed.Dairy,
                        Level = 2,
                        IsActive = true
                    }
                );
        }
    }
}
