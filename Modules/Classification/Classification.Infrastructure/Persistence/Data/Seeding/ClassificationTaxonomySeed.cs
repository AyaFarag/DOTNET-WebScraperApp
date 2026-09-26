using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Infrastructure.Persistence.Data.Seeding
{
    public static class ClassificationTaxonomySeed
    {
        public static readonly Guid Food =
            Guid.Parse("10000000-0000-0000-0000-000000000001");

        public static readonly Guid Beverages =
            Guid.Parse("10000000-0000-0000-0000-000000000002");

        public static readonly Guid Water =
            Guid.Parse("10000000-0000-0000-0000-000000000003");

        public static readonly Guid BottledWater =
            Guid.Parse("10000000-0000-0000-0000-000000000004");

        public static readonly Guid SoftDrinks =
            Guid.Parse("10000000-0000-0000-0000-000000000005");

        public static readonly Guid Juice =
            Guid.Parse("10000000-0000-0000-0000-000000000006");

        public static readonly Guid Dairy =
            Guid.Parse("10000000-0000-0000-0000-000000000007");

        public static readonly Guid Milk =
            Guid.Parse("10000000-0000-0000-0000-000000000008");

        public static readonly Guid Cheese =
            Guid.Parse("10000000-0000-0000-0000-000000000009");

        public static readonly Guid Yogurt =
            Guid.Parse("10000000-0000-0000-0000-000000000010");
    }
}
