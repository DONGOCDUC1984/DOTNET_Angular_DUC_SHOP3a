
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.RepositoryTest
{
    public class CategoryRepositoryTest
    {
        private readonly AppDbContext _ctx;

        public CategoryRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DOTNET_Angular_DUC_SHOP3aTest")
                .Options
                ;
            _ctx = new AppDbContext(options);
            _ctx.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddTest()
        {
            // Arrange
            _ctx.Database.EnsureDeleted();
            _ctx.Categories.AddRange(CategoryMockData.GetCategories());
            await _ctx.SaveChangesAsync();
            var newCategory = CategoryMockData.AddCategory();
            var sut = new CategoryRepository(_ctx);
            // Act
            var result= await sut.AddUpdate(newCategory);
            await _ctx.SaveChangesAsync();
            // Assert
            int expectedCount = CategoryMockData.GetCategories().Count + 1;
            _ctx.Categories.Count().Should().Be(expectedCount);

        }

        [Fact]
        public async Task UpdateTest()
        {
            // Arrange
            _ctx.Database.EnsureDeleted();
            _ctx.Categories.AddRange(CategoryMockData.GetCategories());
            await _ctx.SaveChangesAsync();
            var newCategory = CategoryMockData.UpdateCategory();
            var sut = new CategoryRepository(_ctx);
            // Act
            var result = await sut.AddUpdate(newCategory);
            await _ctx.SaveChangesAsync();
            // Assert
            int expectedCount = CategoryMockData.GetCategories().Count;
            _ctx.Categories.Count().Should().Be(expectedCount);
        }
    }
}
