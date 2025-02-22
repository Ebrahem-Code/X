/*namespace X.API.Infrastructure;


public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);

    protected static IResult AsOk(string message = "")
    {
        if (message.IsEmpty())
        {
            var loc = AppHttpContext.Current.RequestServices
                .GetRequiredService<IStringLocalizer<EndpointGroupBase>>();
            message = loc[ResourceKeys.GenericAsOkMessage];
        }

        return TypedResults.Ok(new ApiResponse
        {
            Status = 200,
            Title = "Success",
            Detail = message,
            Errors = null
        });
    }

    protected static IResult AsBadRequest(string message = "")
    {
        if (message.IsEmpty())
        {
            var loc = AppHttpContext.Current.RequestServices
                .GetRequiredService<IStringLocalizer<EndpointGroupBase>>();
            message = loc[ResourceKeys.GenericAsBadRequestMessage];
        }

        return TypedResults.BadRequest(new ApiResponse
        {
            Status = 400,
            Title = "Failure",
            Detail = message,
            Errors = null
        });
    }

    protected static int GetUserId()
    {
        var userId = AppHttpContext.Current.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "0";
        return !int.TryParse(userId, out var result) ? 0 : result;
    }

    protected static int GetCountryId()
    {
        var countryId = AppHttpContext.Current.Request.Headers["x-country-id"];
        return !int.TryParse(countryId, out var result) ? 1 : result;
    }
    protected static string GetLanguage()
    {
        var lang = AppHttpContext.Current.Request.Headers["x-lang"];

        if (lang.IsEmpty())
            return "en";

        return lang;
    }

    protected static int GetDeviceType()
    {
        var countryId = AppHttpContext.Current.Request.Headers["deviceType"];
        return !int.TryParse(countryId, out var result) ? 0 : result;
    }
}*/