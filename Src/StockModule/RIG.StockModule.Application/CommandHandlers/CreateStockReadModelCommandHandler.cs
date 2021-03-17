using System;
using System.Threading;
using System.Threading.Tasks;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;
using RIG.StockModule.Application.Commands;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Exceptions;
using RIG.StockModule.Domain.Repositories;
using RIG.StockModule.Domain.Rules;
using RIG.StockModule.Domain.Specifications.StockSpecifications;

namespace RIG.StockModule.Application.CommandHandlers
{
    public class CreateStockReadModelCommandHandler : IDomainCommandHandler<CreateStockReadModelCommand>
    {
        private readonly IStockDbContext _stockDbContext;
        private readonly IStockUniqueChecker _stockUniqueChecker;

        public CreateStockReadModelCommandHandler(IStockDbContext stockDbContext, IStockUniqueChecker stockUniqueChecker)
        {
            _stockDbContext = stockDbContext;
            _stockUniqueChecker = stockUniqueChecker;
        }

        public async Task<bool> Handle(CreateStockReadModelCommand request, CancellationToken cancellationToken)
        {
            IStockRepository stockRepository = _stockDbContext.StockRepository;

            var stock = Stock.Create(request.ProductId, request.AvailableStockCount, request.LastStockActionId, request.LastStockUpdatedOn, _stockUniqueChecker, cancellationToken);
            await stockRepository.AddAsync(stock, cancellationToken);

            return true;
        }
    }
}