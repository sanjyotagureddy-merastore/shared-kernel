# API Client Library

## Overview

This library provides classes for making HTTP requests, handling responses, and logging details in a .NET application. The key components are:

- `ApiClient`: Handles HTTP requests and responses with logging and error handling.
- `HttpRequestBuilder`: Builds HTTP requests with customizable settings and policies.
- `HttpResponseExtensions`: Provides extension methods for handling HTTP responses.

## Components

### ApiClient

The `ApiClient` class is responsible for sending HTTP requests, handling responses, and logging detailed information. It supports request and response masking and integrates with Elasticsearch for logging.

#### Features

- Request and response masking using `MaskingService`.
- Logging of request and response details, including HTTP status codes, to Elasticsearch.
- Retry and circuit breaker policies with Polly.

#### Example Usage

```csharp
// Configure services
services.AddSingleton<ApiClient>();
services.AddSingleton<IElasticClient>(provider =>
{
    var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
        .DefaultIndex("request-response-logs");
    return new ElasticClient(settings);
});
services.AddSingleton<MaskingService>();
services.AddSingleton<IConfiguration>(provider => new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build());

// Create and use the ApiClient
var apiClient = serviceProvider.GetRequiredService<ApiClient>();
var requestBuilder = new HttpRequestBuilder()
    .WithMethod(HttpMethod.Get)
    .WithUri("https://api.example.com/data");

var response = await apiClient.ExecuteAsync(requestBuilder);

// Handle the response
if (response.IsSuccessStatusCode)
{
    Console.WriteLine("Request succeeded.");
}
else
{
    Console.WriteLine($"Request failed with status code: {response.StatusCode}");
}

```

## HttpRequestBuilder

The HttpRequestBuilder class provides a fluent interface for constructing HTTP requests. It supports setting HTTP methods, URIs, headers, content, and policies.

### Features

Set HTTP method, URI, headers, and content.
Add retry and circuit breaker policies using Polly.
Configure request timeout.
### Example Usage
```csharp
var requestBuilder = new HttpRequestBuilder()
    .WithMethod(HttpMethod.Post)
    .WithUri("https://api.example.com/submit")
    .WithContent(new StringContent("{ \"key\": \"value\" }", Encoding.UTF8, "application/json"))
    .WithHeader("Authorization", "Bearer token")
    .WithTimeout(TimeSpan.FromSeconds(30));

// Build the request
var request = requestBuilder.Build();
```

## HttpResponseExtensions

The HttpResponseExtensions class provides extension methods for processing HTTP responses. It includes methods to deserialize response content into custom success and error types.

### Features
GetResponseOrError<TSuccess>: Deserialize the response into a success type or an error type.
GetResponseOrError<TSuccess, TError>: Supports custom error types.

### Example Usage

```csharp
public class SuccessResponse
{
    public string Data { get; set; }
}

public class ErrorResponse
{
    public string ErrorMessage { get; set; }
}

// Usage in ApiClient
public async Task<ApiResponse<SuccessResponse, ErrorResponse>> ExecuteRequestAsync()
{
    var response = await httpClient.SendAsync(request);
    return await response.GetResponseOrError<SuccessResponse, ErrorResponse>();
}

// Handle the response
var apiResponse = await ExecuteRequestAsync();
if (apiResponse.IsSuccess)
{
    Console.WriteLine($"Success: {apiResponse.Result.Data}");
}
else
{
    Console.WriteLine($"Error: {apiResponse.Error.ErrorMessage}");
}
```