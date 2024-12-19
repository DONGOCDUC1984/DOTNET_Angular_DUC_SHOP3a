using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.ControllerTest
{
    public class CategoryControllerTest 
    {
        
        [Fact]
        public async Task GetAllTest_ShouldReturnData()
        {
            // Arrange
            var categoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            categoryRepository.Setup(x => x.GetAll())
                 .ReturnsAsync(CategoryMockData.GetCategories());
            var sut = new CategoryController(mockUnitOfWork.Object);
            // Act
            var result = await sut.GetAll() as List<Category>;

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(CategoryMockData.GetCategories().Count, result.Count);
            categoryRepository.Verify(x => x.GetAll(), Times.Exactly(1));
        }
    }
}
