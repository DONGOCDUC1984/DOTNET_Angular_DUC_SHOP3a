using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.RepositoryTest
{
    public class DistrictRepositoryTest
    {
        private readonly AppDbContext _ctx;

        public DistrictRepositoryTest()
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
            _ctx.Districts.AddRange(DistrictMockData.GetDistricts());
            await _ctx.SaveChangesAsync();
            var newDistrictDTO = DistrictMockData.AddDistrict();
            var sut = new DistrictRepository(_ctx);
            // Act
            var result = await sut.AddUpdate(newDistrictDTO);
            await _ctx.SaveChangesAsync();
            // Assert
            int expectedCount = DistrictMockData.GetDistricts().Count + 1;
            _ctx.Districts.Count().Should().Be(expectedCount);

        }

        [Fact]
        public async Task UpdateTest()
        {
            // Arrange
            _ctx.Database.EnsureDeleted();
            _ctx.Districts.AddRange(DistrictMockData.GetDistricts());
            await _ctx.SaveChangesAsync();
            var newDistrictDTO = DistrictMockData.UpdateDistrict();
            var sut = new DistrictRepository(_ctx);
            // Act
            var result = await sut.AddUpdate(newDistrictDTO);
            await _ctx.SaveChangesAsync();
            // Assert
            int expectedCount = DistrictMockData.GetDistricts().Count;
            _ctx.Districts.Count().Should().Be(expectedCount);
        }

        [Fact]
        public async Task GetAllTest_ReturnDistrictCollection()
        {
            // Arrange
            _ctx.Database.EnsureDeleted();
            _ctx.Districts.AddRange(DistrictMockData.GetDistricts());
            await _ctx.SaveChangesAsync();
            var sut = new DistrictRepository(_ctx);
            // Act
            var result = await sut.GetAll();
            // Assert
            // result.Should().HaveCount((DistrictMockData.GetDistricts().Count));
            // The above line is equivalent to the following line
            Assert.Equal(result.Count(), DistrictMockData.GetDistricts().Count);
        }
    }
}
