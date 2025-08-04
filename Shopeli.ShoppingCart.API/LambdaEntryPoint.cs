namespace Shopeli.ShoppingCart.API
{
    using Amazon.Lambda.AspNetCoreServer;

    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseStartup<Program>();
        }
    }
}
