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
            //Arrange i Act
            var product = new Product("ubrania", 120, "Pi¿amka", "w cêtki", ProductCondition.New);
           
            //Assert
            Assert.Equal("Pi¿amka", product.Name);
            Assert.Equal(120, product.Price);
            Assert.Equal("ubrania", product.Category);
            Assert.Equal("w cêtki", product.Description);
            Assert.Equal(ProductCondition.New, product.ProductCondition);
        }

        [Fact]
        public void should_throw_error_when_creating_a_new_product_a_name_shorter_than_five_characters()
        {
            //Arrange and Act, Assert
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿", "w cêtki", ProductCondition.New));
        }

        [Fact]
        public void should_throw_an_error_when_creating_a_new_product_with_special_character_in_the_name()
        {
            //Arrange and Act, Assert
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿@mk@", "w cêtki", ProductCondition.New));
        }

        [Fact]
        public void should_throw_exception_on_validation_description()
        {
            //Arrange and Act, Assert
            Assert.Throws<Exception>(() => new Product("ubrania", 120, "Pi¿amka", CreateLongText(), ProductCondition.New));
        }

        private static string CreateLongText()
        {
            StringBuilder dlugiTekst = new StringBuilder("1234567890");
            for (int i = 0; i < 500; i++)
            {
                dlugiTekst.Append("1234567890");
            }
            return dlugiTekst.ToString();
        }
    }
}
