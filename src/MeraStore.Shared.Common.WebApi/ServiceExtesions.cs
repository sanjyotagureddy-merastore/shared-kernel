using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

using MeraStore.Shared.Common.Logging;
using MeraStore.Shared.Common.Logging.Masking;
using MeraStore.Shared.Common.WebApi.Filters;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeraStore.Shared.Common.WebApi;


[ExcludeFromCodeCoverage]
public static class ServiceExtensions
{
  public static IServiceCollection AddDefaultServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddControllers().AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
      options.JsonSerializerOptions.WriteIndented = true;
      options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    var maskingConfig = new Dictionary<string, List<string>>();
    configuration.GetSection("MaskingConfig").Bind(maskingConfig);
    services.AddSingleton(maskingConfig);
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(sg =>
    {
      sg.OperationFilter<CustomHeadersFilter>();
    });
    services.AddSingleton<MaskingService>();
    services.AddElasticsearch(configuration);
    services.AddHttpContextAccessor();
    services.AddResponseCompression();

    return services;
  }
}