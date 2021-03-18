# Reading Is Good

This project contains Account, Product, Stock and Order domains which are important for e-commerce.

## __RUN__

The project could be executed via _docker-compose_.

If you want to debug this code, you can use ```docker-compose -f docker-compose-test.yml up``` command. By doing so, Sql
Server and RabbitMq components will be up and running.
[DotNet 5](https://dotnet.microsoft.com/download/dotnet/5.0) is required to compile the project.

Note: If the `docker-compose up` command cannot be executed due to the port conflict, you should change the port
information both from _docker-compose.yml_ and _appsettings.json_ file.

## __General Info__

Admin Username: _admin_ </br>
Admin Password: _123456Ac_

The above information exists in the system. This information will be used for operations that can be carried out by
account in the admin role.

This project is built
using [Event Driven Programming](https://ademcatamak.medium.com/olaya-dayal%C4%B1-programlama-event-driven-programming-d6b7e2c0d948)
technique.

[Outbox Design Pattern](https://ademcatamak.medium.com/outbox-design-pattern-57e1519ed5e4) is used during
sending/publishing integration commands/events. With this pattern multiple processes merge into one transaction scope.

The responsibilities of the layers of the project were decided according to the DDD
concept. [Layers in DDD](https://ademcatamak.medium.com/layers-in-ddd-projects-bd492aa2b8aa)
blog post contains information about these responsibilities.

### __Account__

This module gives services for creating user, listing users and generating access token. This web api is coded in
accordance with the REST architecture.

You can click
on [5 Rules of REST Architecture](https://ademcatamak.medium.com/5-rules-of-rest-architecture-434abaf5db44) for details
for the 5 rules that are aimed to be followed.

### __Product__

POST Book, DELETE Book, and GET Books service points for creation, deletion and listing of book information are
available under the product domain. Book information can be created and deleted in the system by the admin.

When fetching book information, specifications are created according to the criteria entered. There is much more
information
about [Specification Design Pattern](https://ademcatamak.medium.com/specification-design-pattern-c814649be0ef) in this
blog post.

### __Stock__

Stock domain stores the number of products in the stock. All changes made on stock stored under StockActions resource.
All changes made on stock to the product through the domain event affects StockSnapshot with transactional integrity.

Changes on StockSnapshot affect Stock read model. Relation between StockSnapshot and Stock is eventually consistent.

Optimistic lock is used while updating the StockSnapshot data.

[CQRS pattern](https://ademcatamak.medium.com/cqrs-command-query-responsibility-segregation-476d2d81225a) is used in
this module. You can
check [Stock Management with CQRS](https://ademcatamak.medium.com/stok-y%C3%B6netimi-cqrs-%C3%B6rne%C4%9Fi-c8243b82c7b2)
blog post for more information.

### __Order__

Users can create an order with the product id and information of the quantity they want. If the stock control is made
and sufficient stock cannot be found, the order status set as _NotFulfilled_. On the other hand, if there is sufficient
stock, the order set as _Fulfilled_. After _Fulfilled_ status, the order can be set as _Shipped_ status by admin.

In this
project [Orchestrator based Saga](https://ademcatamak.medium.com/koordinat%C3%B6r-tabanl%C4%B1-saga-tasar%C4%B1m%C4%B1-sipari%C5%9F-y%C3%B6netimi-4db5fc546f68)
technique is used
for [Distributed Transaction](https://ademcatamak.medium.com/da%C4%9F%C4%B1t%C4%B1k-i%CC%87%C5%9Flemler-distributed-transaction-6c36f5e04266)
.

## Next Step

[Integration Test with Docker](https://ademcatamak.medium.com/integration-test-with-net-core-and-docker-21b241f7372)
will be implemented. After that, tests will be executed in pipeline
via [Cake Build](https://ademcatamak.medium.com/cake-build-nedir-684eb1885b06) files.