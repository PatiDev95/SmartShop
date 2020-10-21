using HappyShop.Core.Domain;
using System;
using Xunit;

namespace HappyShop.Tests
{
    public class ProductTest
    {
        [Fact]
        public void WhenPurchasingAProductTheProductShouldBeArchived()
        {
            //Arrange
            var newProduct = new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New);

            //Act
            newProduct.BuyProduct();

            //Assert
            Assert.True(newProduct.IsArchived);
        }

        [Fact]
        public void ThereShouldBeAnExceptionWhenBuyingAProductAlreadyPurchased()
        {
            //Arrange
            var product = new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New);

            //Act
            product.BuyProduct();

            //Assert
            Assert.Throws<Exception>(() => product.BuyProduct());

        }

        [Fact]
        public void TheConstructorShouldAssignValuesCorrectly()
        {
            //Arrange i Act
            var product = new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New);
           
            //Assert
            Assert.Equal("Pi¿amka", product.Name);
            Assert.Equal(120, product.Price);
            Assert.Equal("ubrania", product.Category);
            Assert.Equal("w cêtki", product.Description);
            Assert.Equal(ProductCondition.New, product.ProductCondition);
        }
    }
}
