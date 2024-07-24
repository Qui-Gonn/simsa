namespace Simsa.Extensions;

using Simsa.Model;

public static class RouteGroupBuilderExtensions
{
    public static void AddApiEndpoints(this IEndpointRouteBuilder builder)
    {
        var simsaEndpointsBuilder = SimsaEndpointsBuilder.Create(builder);
        simsaEndpointsBuilder.MapFullGroup<Event>();
        simsaEndpointsBuilder.MapFullGroup<Person>();
    }
}