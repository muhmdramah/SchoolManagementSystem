using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using SchoolManagementSystem.Core.Bases;
using System.Net;

namespace SchoolManagementSystem.Api.Controllers.Common
{
    public class ApplicationControllerBase : ControllerBase
    {
        internal readonly IMediator _mediator;
        internal readonly IOutputCacheStore _outputCacheStore;

        public ApplicationControllerBase(IMediator mediator, IOutputCacheStore outputCacheStore)
        {
            _mediator = mediator;
            _outputCacheStore = outputCacheStore;
        }

        #region Actions
        public ObjectResult NewResult<T>(Response<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }
        #endregion
    }
}
