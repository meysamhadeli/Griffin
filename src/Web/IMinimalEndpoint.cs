using Microsoft.AspNetCore.Routing;

namespace Griffin.Web;

public interface IMinimalEndpoint
{
    IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder);
}