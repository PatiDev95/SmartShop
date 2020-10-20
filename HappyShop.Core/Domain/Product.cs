using System;
using System.Linq;

namespace HappyShop.Core.Domain

{
    public class Product
    {
        public Guid Id { get; protected set; }
        public string Category { get; protected set; }
        public decimal Price { get; protected set; }
        public string Name { get; protected set; }
        public int? Discount { get; protected set; }
        public decimal RealPrice
        {
            get
            {
                if (Discount == null)
                {
                    return Price;
                }
                else
                {
                    return Price - Price * Discount.Value / 100;
                }
            }
        }
        public string Description { get; set; }
        public ProductCondition ProductCondition { get; set; }
        public string Brand { get; set; }
        public double Weight { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }

        public Product(string category, decimal price, string name, string description, ProductCondition productCondition)
        {
            Id = Guid.NewGuid();
            Category = category;
            Price = price;
            SetName(name);
            SetDescription(description);
            ProductCondition = productCondition;
            IsArchived = false;
            CreatedAt = DateTime.Now;
        }

        public void BuyProduct()
        {
            if (IsArchived == false)
            {
                IsArchived = true;
            }
            else
            {
                throw new Exception("The product has just been sold. You cannot buy the product.");
            }
        }

        public Product WithWeight(double weight)
        {
            if (weight < 0)
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

            if (name.Length > 250 || name.Length < 5)
            {
                throw new Exception($"Incorrect number of characters in the name: {name.Length}. Valid name length 5-250 characters.");
            }

            Name = name;
        }

        public void SetDescription(string description)
        {
            if (description.Length > 5000)
            {
                throw new Exception($"The description exceeds the number of characters allowed. Your number of characters: {description.Length} The maximum number of characters is: 5000");
            }
            Description = description;
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
