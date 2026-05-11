using Application.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

public static class ControllerExtensions
{
    public static IActionResult ToActionResult<T>(this ControllerBase controller, ResponseValue<T> response)
    {
        return response.Status switch
        {
            ResponseStatus.Success => controller.Ok(response),
            ResponseStatus.NotFound => controller.NotFound(response),
            ResponseStatus.BadRequest => controller.BadRequest(response),
            ResponseStatus.Conflict => controller.Conflict(response),
            ResponseStatus.Unauthorized => controller.Unauthorized(response),
            _ => controller.StatusCode(500, response)
        };
    }
}