using System.Diagnostics.CodeAnalysis;
using MeraStore.Shared.Common.Core;
using MeraStore.Shared.Common.Logging.Enrichers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.Elasticsearch;

namespace MeraStore.Shared.Common.Logging
{
  [ExcludeFromCodeCoverage]
  public static class ModuleRegistration
  {
    public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
      var elasticUri = configuration["ElasticConfiguration:Uri"]!;
      var settings = new ConnectionSettings(new Uri(elasticUri))
        .DefaultIndex(Constants.SerilogIndex.RequestResponse);

      var elasticClient = new ElasticClient(settings);
      services.AddSingleton<IElasticClient>(elasticClient);

      CreateIndexWithMappings(elasticClient, elasticUri);

      return services;
    }

    public static IHostBuilder UseLogging(this IHostBuilder hostBuilder, string appName)
    {
      return hostBuilder.UseSerilog((context, configuration) =>
      {
        var elasticUri = context.Configuration["ElasticConfiguration:Uri"];

        // Base configuration for all logs
        configuration
          .ReadFrom.Configuration(context.Configuration)
          .Enrich.WithProperty("app-name", appName)
          .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName ?? "Development")
          .Enrich.WithAssemblyName()
          .Enrich.WithMachineName()
          .Enrich.WithProcessId()
          .Enrich.WithThreadId()
          .Enrich.With(new IpAddressEnricher())
          .WriteTo.Console(
            outputTemplate:
            $"[{{SourceContext}}-{appName}-{{MachineName}}]{{NewLine}}{{Timestamp:ddMMyyyy HH:mm:ss}} {{Level:u4}}] {{Message:lj}}{{NewLine}}{{Exception}}");

        // EF Core logs to a different index
        configuration.WriteTo.Logger(logger => logger
          .Filter.ByIncludingOnly(Matching.FromSource("Microsoft.EntityFrameworkCore"))
          .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri!))
          {
            IndexFormat =
              $"efcore-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1
          }));

        // ASP.NET Core logs to a different index
        configuration.WriteTo.Logger(lc => lc
          .Filter.ByIncludingOnly(Matching.FromSource("Microsoft.AspNetCore"))
          .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri!))
          {
            IndexFormat =
              $"aspnetcore-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1
          }));

        // General application logs to a different index
        configuration.WriteTo
          .Logger(lc => lc
            .Filter.ByExcluding(Matching.FromSource("Microsoft"))
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri!))
            {
              IndexFormat =
                $"app-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
              AutoRegisterTemplate = true,
              NumberOfShards = 2,
              NumberOfReplicas = 1
            }));
      });
    }

    private static void CreateIndexWithMappings(IElasticClient client, string elasticUri)
    {
      var indexNames = new[]
      {
        $"efcore-logs-{DateTime.Now:yyyy-MM}",
        $"aspnetcore-logs-{DateTime.Now:yyyy-MM}",
        $"app-logs-{DateTime.Now:yyyy-MM}"
      };

      foreach (var indexName in indexNames)
      {
        var createIndexResponse = client.Indices.Create(indexName, c => c
          .Map(m => m
            .DynamicTemplates(dt => dt
              .DynamicTemplate("text_fields_as_keyword", d => d
                .MatchMappingType("string")
                .Mapping(ma => ma
                  .Text(t => t
                    .Fields(fs => fs
                      .Keyword(k => k
                        .Name("{field}.keyword")
                      )
                    )
                  )
                )
              )
            )
          )
        );

        if (!createIndexResponse.IsValid)
        {
          Console.WriteLine($"Failed to create index {indexName}: {createIndexResponse.DebugInformation}");
        }
        else
        {
          Console.WriteLine($"Index {indexName} created with desired mappings.");
        }
      }
    }
  }
}
