using System;
using System.Text.RegularExpressions;

namespace WebApplication3.Models

{
    public class Product
    {
        public Guid Id { get; protected set; }
        public string Category { get; protected set; }
        public decimal Price { get; protected set; }
        public string Name { get; protected set; }
        public int? Discount { get; protected set; }
        public decimal RealPrice { 
            get 
            {
                if(Discount==null)
                {
                    return Price;
                }
                else
                {
                    return Price - (Price * Discount.Value / 100);
                }
            } 
        }
        public string Description { get; set; }
        public ProductCondition ProductCondition { get; set; }
        public string Brand { get; set; }
        public double Weight { get; set; }

        public Product(Builder builder)
        {
            Id = Guid.NewGuid();
            Category = builder.Category;
            Price = builder.Price;
            Name = builder.Name;
            Discount = builder.Discount;
        }

        public Product(string category, decimal price, string name, string description, ProductCondition productCondition)
        {
            Id = Guid.NewGuid();
            Category = category;
            Price = price;
            SetName(name);
            SetDescription(description);
            ProductCondition = productCondition;
        }

        public Product WithWeight(double weight)
        {
            if(weight<0)
            {
                throw new Exception("Weight can not be less than 0.");
            }
            Weight = weight;
            return this;
        }

        public Product WithDiscount(int discount)
        {
            if (discount < 90 && discount > 0)
            {
                Discount = discount;
            }
            else
            {
                throw new Exception("The discount cannot be greater than 90% and less than 0%.");
            }
            return this;
        }

        public Product WithBrand(string brand)
        {
            ValidateSpecialCharacters(brand, "There is an illegal character in the brand name: {0}.");
            Brand = brand;
            return this;
        }
        
        public void SetName(string name)
        {
           ValidateSpecialCharacters(name, "There is an illegal character in the product name: {0}.");

            if (name.Length>250 || name.Length<5)
            {
                throw new Exception($"Incorrect number of characters in the name: {name.Length}. Valid name length 5-250 characters.");
            }

            Name = name;
        }

        public void SetDescription(string description)
        {
            if(description.Length>5000)
            {
                throw new Exception($"The description exceeds the number of characters allowed. Your number of characters: {description.Length} The maximum number of characters is: 5000");
            }
            Description = description;
        }

        public class Builder
        {
            internal string Category;
            internal decimal Price;
            internal string Name;
            internal int Discount;

            public Builder WithCategory(string category)
            {
                Category = category;
                return this;
            }

            public Builder WithPrice(decimal price)
            {
                Price = price;
                return this;
            }

            public Builder WithName (string name)
            {
                Name = name;
                return this;
            }

            public Builder WithDiscount (int discount)
            {
                Discount = discount;
                return this;
            }

            public Product Build()
            {
                return new Product(this);
            }
        }

        /// <summary>
        /// Validates special characters in string
        /// </summary>
        /// <param name="string">Value to be validated</param>
        /// <param name="error">Error message if fails, contains string format which interpolates character which failed at {0} format.</param>
        private static void ValidateSpecialCharacters(string @string, string error)
        {
            const string SpecialCharacters = "~`!@#$%^&*()_+={}[]:;'<>,.?/|";
            foreach (var @char in @string)
            {
                if (SpecialCharacters.Contains(@char))
                {
                    throw new Exception(string.Format(error, @char));
                }
            }
        }
    }
}
