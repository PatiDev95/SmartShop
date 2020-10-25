using HappyShop.Core.Domain;
using System;
using System.Text;
using Xunit;

namespace HappyShop.Tests
{
    public class ProductTest
    {
        [Fact]
        public void should_be_archived_when_purchasing_the_product()
        {
            //Arrange
            var newProduct = new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New);

            //Act
            newProduct.BuyProduct();

            //Assert
            Assert.True(newProduct.IsArchived);
        }

        [Fact]
        public void should_be_an_exception_when_buying_a_product_already_purchased()
        {
            //Arrange
            var product = new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New);

            //Act
            product.BuyProduct();

            //Assert
            Assert.Throws<Exception>(() => product.BuyProduct());

        }

        [Fact]
        public void should_assign_values_correctly_in_the_constructor()
        {
            //Arrange and Act
            var product = new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New);
           
            //Assert
            Assert.Equal("Pi¿amka", product.Name);
            Assert.Equal(120, product.Price);
            Assert.Equal("ubrania", product.Category);
            Assert.Equal("w cêtki", product.Description);
            Assert.Equal(ProductCondition.New, product.ProductCondition);
        }

        [Fact]
        public void should_throw_exception_on_product_validation()
        {
            //Arrange, Act and Assert
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿amka", new string('a', 5001), ProductCondition.New));
            Assert.Throws<Exception>(() => new Product("ubrania", 120, new string('a', 251), "w cêtki", ProductCondition.New));
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿", "w cêtki", ProductCondition.New));
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿@mk@", "w cêtki", ProductCondition.New));
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New).WithBrand("@didas"));
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New).WithDiscount(91));
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New).WithDiscount(-5));
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New).WithWeight(-5));
        }
    }
}
