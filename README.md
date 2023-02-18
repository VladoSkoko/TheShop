# TheShop

# Setup
1. Run `Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r` to fix the build.

# Changes To The Original Source
1. Added dependency injection with Autofac.
2. Added NLog to handle logging.
3. Added an ExceptionFilterAttribute to log Exceptions and create HTTP responses when they occur.
4. Added Exceptions for common HTTP status codes which are sent to the client.
5. Added support for localization, which is configured through Web.config files.
6. Renamed ShopController to ArticleController and utilized Attribute Routing for both Actions.
7. Addded DTO and DB Entity classes for Article and changed the regular Article class to ensure data validity.
8. Renamed and consolidated CachedSupplier, Warehouse, Dealer1 and Dealer2 as ISupplierService.
9. Added SupplierManager to asynchronously check the inventory of all suppliers and keep the cache updated.

# Questions & Uncertainties
1. Are all suppliers equal in order of preference?
2. Should there be any correspondence between the mocked database and the available suppliers?