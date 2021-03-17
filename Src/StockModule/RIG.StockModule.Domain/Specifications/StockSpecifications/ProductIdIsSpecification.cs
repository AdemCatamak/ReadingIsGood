using System;
using System.Linq.Expressions;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.StockModule.Domain.Specifications.StockSpecifications
{
    public class ProductIdIsSpecification : ExpressionSpecification<Stock>
    {
        public ProductIdIsSpecification(string productId) : base(stock => stock.ProductId == productId)
        {
        }
    }
}