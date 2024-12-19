
namespace ProjectTest.MockData
{
    public class CategoryMockData
    {
        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Fruit and vegetable" },
                new Category { Id = 2, Name = "Bread and cake" },
                new Category { Id = 3, Name = "Milk" }
            };
        }

        public static Category AddCategory()
        {
            return new Category { Id = 0, Name = "Book" };
        }

        public static Category UpdateCategory()
        {
            return new Category { Id = 3, Name = "Weapon" };
        }
    }
}
