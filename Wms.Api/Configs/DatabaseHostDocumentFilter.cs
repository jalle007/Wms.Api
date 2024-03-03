using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Wms.Api.Configs
{
    public class DatabaseHostDocumentFilter : IDocumentFilter
    {
        private readonly IConfiguration _config;

        public DatabaseHostDocumentFilter(IConfiguration config)
        {
            _config = config;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var connectionString = _config.GetConnectionString("WmsDatabase");
            var databaseHost = new NpgsqlConnectionStringBuilder(connectionString).Host;
            var host = databaseHost.ToLower().Contains("local") ? "Local" : "Server";

            swaggerDoc.Info.Description = "(Database host: <b>" + host + "</b>)" +
            "</br></br>First create new User with one of following Role types: <i>0 (SuperAdmin) 1 (BackOfficeAdmin) 2 (Operator)</i>" + "</br>For entities follow this order:" + "<i> Location (city, adress) -> Warehouse -> Area -> Shelf (row, column) -> Storage location</i>";
        }
    }

}
