﻿using System;
using RIG.ProductModule.Domain.Exceptions;
using RIG.ProductModule.Domain.ValueObjects;

namespace RIG.ProductModule.Domain
{
    public class Product
    {
        public ProductId Id { get; private set; }
        public string ProductName { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public Product(string productName)
            : this(new ProductId(Guid.NewGuid()), productName, DateTime.UtcNow)
        {
        }

        private Product(ProductId id, string productName, DateTime createdOn)
        {
            Id = id;
            ProductName = productName;
            CreatedOn = createdOn;
        }

        public static Product Create(string productName)
        {
            productName = productName?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(productName)) throw new ProductNameEmptyException();

            Product product = new Product(productName);
            return product;
        }
    }
}