using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.RepositoryTest
{
    public class ProvinceCityRepositoryTest
    {
        private readonly AppDbContext _ctx;

        public ProvinceCityRepositoryTest()
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
            _ctx.ProvinceCities.AddRange(ProvinceCityMockData.GetProvinceCities());
            await _ctx.SaveChangesAsync();
            var newProvinceCity = ProvinceCityMockData.AddProvinceCity();
            var sut = new ProvinceCityRepository(_ctx);
            // Act
            var result = await sut.AddUpdate(newProvinceCity);
            await _ctx.SaveChangesAsync();
            // Assert
            int expectedCount = ProvinceCityMockData.GetProvinceCities().Count + 1;
            _ctx.ProvinceCities.Count().Should().Be(expectedCount);

        }

        [Fact]
        public async Task UpdateTest()
        {
            // Arrange
            _ctx.Database.EnsureDeleted();
            _ctx.ProvinceCities.AddRange(ProvinceCityMockData.GetProvinceCities());
            await _ctx.SaveChangesAsync();
            var newProvinceCity = ProvinceCityMockData.UpdateProvinceCity();
            var sut = new ProvinceCityRepository(_ctx);
            // Act
            var result = await sut.AddUpdate(newProvinceCity);
            await _ctx.SaveChangesAsync();
            // Assert
            int expectedCount = ProvinceCityMockData.GetProvinceCities().Count;
            _ctx.ProvinceCities.Count().Should().Be(expectedCount);
        }
    }
}
