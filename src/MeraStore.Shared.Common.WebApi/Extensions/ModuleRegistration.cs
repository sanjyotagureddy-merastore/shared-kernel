using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using MeraStore.Service.Logging.SDK;
using MeraStore.Shared.Common.Logging;
using MeraStore.Shared.Common.Logging.Masking;
using MeraStore.Shared.Common.Logging.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MeraStore.Shared.Common.WebApi.Extensions;


[ExcludeFromCodeCoverage]
public static class ModuleRegistration
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
        var maskingConfig = new Dictionary<MaskingType, List<string>>();
        configuration.GetSection("MaskingConfig").Bind(maskingConfig);
        services.AddSingleton(maskingConfig);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddLoggingApiServices();
        services.TryAddSingleton<MaskingService>();
        services.AddElasticsearch(configuration);
        services.AddHttpContextAccessor();
        services.AddResponseCompression();

        return services;
    }
}