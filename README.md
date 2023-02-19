# The Shop

# APIs
API documentation: http://localhost:30496/swagger

**ArticleController -> GetArticle()**

GET `http://localhost:30496/api/v1/article/{articleId}?maxExpectedPrice={maxExpectedPrice}`

Checks whether the Article is present in cache. If it is, fetch it from there. If not, attempt to fetch it from external suppliers. Each supplier is queried to check whether the Article is in stock. All results are parsed and the first in order to have it in stock is used, with the warehouse having precedence over external suppliers. If none have it in stock, `null` is returned. In case one of the supplier calls errors out, so does the HTTP request.

**ArticleController -> BuyArticle()**

POST `http://localhost:30496/api/v1/article/{articleId}/buy/{buyerUserId}`

Expects `ArticleDto` as a raw JSON body parameter. "Sells" the Article by setting its selling information and inserting it into the mock database. 

# Localisation
To change the language of HTTP responses to Serbian, change `<globalization uiCulture="en-US" />` to `<globalization uiCulture="sr-Latn-RS" />` in `Web.config`.

# Setup of the Original Source (not needed now)
1. Run `Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r` to fix the build.

# Changes to the Original Source
1. Added dependency injection with Autofac.
2. Added NLog to handle logging.
3. Added an `ExceptionFilterAttribute` to log Exceptions and create HTTP responses when they occur.
4. Added Exceptions for common HTTP status codes which are sent to the client.
5. Added support for localization, which is configured through `Web.config` files.
6. Renamed `ShopController` to `ArticleController` and utilized Attribute Routing for both Actions.
7. Addded DTO and DB Entity classes for `Article` and changed the regular `Article` class to ensure data validity.
8. Renamed and consolidated `CachedSupplier`, `Warehouse`, `Dealer1` and `Dealer2` as `ISupplierService`.
9. Added `SupplierManager` to asynchronously check the inventory of all suppliers and keep the cache updated.
10. Added Swagger UI by appending `/swagger` to the URL - http://localhost:30496/swagger
11. Added a test project and an example of a fully tested class with `SupplierManagerTest`.

# Questions & Uncertainties
1. What is the intended functionality of both APIs?
2. Are all suppliers equal in order of preference?
3. Should there be any correspondence between the mocked database and the available suppliers?

# Not done
1. Incorporation of `maxExpectedPrice` into the Article fetching logic, as I wasn't sure what it was supposed to do. I would have passed it onto every `ISupplierService->GetArticleAsync()` method and returned true only if the present Article's price is lower than the provided `maxExpectedPrice` value.
2. Integration tests, due to uncertainty of what the APIs are supposed to do.
3. Removal of unnecessary logs, or replacing them with tracing.
4. Utilization of distributed caching, like Redis, in `CachedSupplierService`.
5. Testing using the command `dotnet test`, like .NET Core and newer versions allow.
6. Additional build configurations for Serbian localisation.
7. Linting.
8. Test coverage report generation.
