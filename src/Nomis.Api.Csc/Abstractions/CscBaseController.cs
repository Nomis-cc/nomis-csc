using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomis.Api.Common.Abstractions;
using Nomis.Api.Common.Swagger.Examples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Nomis.Api.Csc.Abstractions
{
    /// <summary>
    /// Base controller for CSC.
    /// </summary>
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class CscBaseController :
        BaseController
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        protected internal new const string BasePath = "api/v{version:apiVersion}/csc";

        /// <summary>
        /// Common tag for CSC actions.
        /// </summary>
        protected internal const string CscTag = "Csc";
    }

    /// <summary>
    /// A controller to aggregate all CSC-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Csc.")]
    internal sealed partial class CscController :
        CscBaseController
    {
        /// <summary>
        /// Ping API.
        /// </summary>
        /// <remarks>
        /// For health checks.
        /// </remarks>
        /// <returns>Return "Ok" string.</returns>
        /// <response code="200">Returns "Ok" string.</response>
        [HttpGet(nameof(Ping))]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = nameof(Ping),
            Tags = new[] { CscTag })]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PingResponseExample))]
        public IActionResult Ping()
        {
            return Ok("Ok");
        }
    }
}