using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
// using Dynatrace.OpenTelemetry;
// using Dynatrace.OpenTelemetry.Instrumentation.AwsLambda;
// using OpenTelemetry;
// using OpenTelemetry.Context.Propagation;
// using OpenTelemetry.Trace;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HelloWorld
{

    public class Function
       {
        // private static readonly TracerProvider TracerProvider;
        private static readonly ActivitySource ActivitySource;

        // static Function()
        // {
        //     DynatraceSetup.InitializeLogging();
        //     var activitySourceName = Assembly.GetExecutingAssembly().GetName().Name;
        //     ActivitySource = new ActivitySource(activitySourceName);
        //     TracerProvider = Sdk.CreateTracerProviderBuilder()
        //         .AddSource(activitySourceName)
        //         .AddDynatrace()
        //         .Build();
        // }

        // public static IEnumerable<KeyValuePair<string, object>> GetFunctionTags(ILambdaContext context, string trigger)
        // {
        //     return new KeyValuePair<string, object>[]
        //     {
        //         new("faas.name", context.FunctionName),
        //         new("faas.id", context.InvokedFunctionArn),
        //         new("faas.trigger", trigger),
        //         new("cloud.platform", "aws_lambda"),
        //         new("cloud.provider", "aws"),
        //         new("cloud.region", Environment.GetEnvironmentVariable("AWS_REGION")),
        //     };
        // }

        public APIGatewayProxyResponse FunctionHandler(APIGatewayHttpApiV2ProxyRequest apiGatewayProxyEvent, ILambdaContext context)
        {
            try
            {
                // var parentContext = ExtractParentContext(apiGatewayProxyEvent, context);
                // using var activity = ActivitySource.StartActivity(ActivityKind.Server, parentContext, GetFunctionTags(context, "http"));
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = "Result from pure function (no openTel)",
                };
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Exception occurred while handling request: {ex.Message}");
                throw;
            }
            finally
            {
                // TracerProvider?.ForceFlush();
            }
        }

        // private static ActivityContext ExtractParentContext(APIGatewayHttpApiV2ProxyRequest apiGatewayProxyEvent, ILambdaContext context)
        // {
        //     var propagationContext = AwsLambdaHelpers.ExtractPropagationContext(context);
        //     if (propagationContext == default)
        //     {
        //         propagationContext = Propagators.DefaultTextMapPropagator.Extract(default, apiGatewayProxyEvent, HeaderValuesGetter);
        //     }

        //     return propagationContext.ActivityContext;
        // }

        private static IEnumerable<string> HeaderValuesGetter(APIGatewayHttpApiV2ProxyRequest apiGatewayProxyEvent, string name) =>
            (apiGatewayProxyEvent.Headers != null && apiGatewayProxyEvent.Headers.TryGetValue(name.ToLowerInvariant(), out var value)) ? new[] { value } : null;
    }
}
