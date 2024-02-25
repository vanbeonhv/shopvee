using System.Net;

namespace shopveeAPI.Common;

public abstract class Service
{
    protected ServiceResponse Ok(object data, string message = "Success")
    {
        return ServiceResponse.Succeed((int)HttpStatusCode.OK, data, message);
    }

    protected ServiceResponse Created(object data, string message = "Created")
    {
        return ServiceResponse.Succeed((int)HttpStatusCode.Created, data, message);
    }

    protected ServiceResponse BadRequest(string message = "Bad Request")
    {
        return ServiceResponse.Fail((int)HttpStatusCode.BadRequest, null, message);
    }
    protected ServiceResponse BadRequest(object data, string message = "Bad Request")
    {
        return ServiceResponse.Fail((int)HttpStatusCode.BadRequest, data, message);
    }

    protected ServiceResponse Unauthorized(object data, string message = "Unauthorized")
    {
        return ServiceResponse.Fail((int)HttpStatusCode.Unauthorized, data, message);
    }

    protected ServiceResponse NotFound(string message = "Not Found")
    {
        return ServiceResponse.Fail((int)HttpStatusCode.NotFound, null, message);
    }

    protected ServiceResponse Forbidden(object data, string message = "Forbidden")
    {
        return ServiceResponse.Fail((int)HttpStatusCode.Forbidden, data, message);
    }
}