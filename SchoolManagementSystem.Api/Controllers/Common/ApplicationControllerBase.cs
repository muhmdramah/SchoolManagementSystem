using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

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
    }
}
