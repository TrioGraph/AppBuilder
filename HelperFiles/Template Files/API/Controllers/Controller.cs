using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using <<namespace>>.Repositories;
using <<namespace>>.Models.DTO;
using <<namespace>>.Models.Domain;

namespace <<namespace>>.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class <<ControllerName>> : Controller
    {
        private readonly <<RepositoryInterfaceName>> <<RepositoryInstanceName>>;
        private readonly IMapper Mapper;
        private readonly ILogger<<<ControllerName>>> _logger;
        public <<ControllerName>>(<<RepositoryInterfaceName>> <<RepositoryInstanceName>>, IMapper mapper, ILogger<<<ControllerName>>> logger)
        {
            this.<<RepositoryInstanceName>> = <<RepositoryInstanceName>>;
            Mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/getAll<<FunctionName>>")]
        public async Task<IActionResult> getAll<<FunctionName>>()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var <<FunctionName>>List = await <<RepositoryInstanceName>>.get<<FunctionName>>();
                _logger.LogInformation($"database call done successfully with {<<FunctionName>>List?.Count()}");
                return Ok(<<FunctionName>>List);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/get<<FunctionName>>DLL")]
        public async Task<IActionResult> get<<FunctionName>>DLL()
        {
            try
            {
                _logger.LogInformation($"Start");
                var <<FunctionName>>List = await <<RepositoryInstanceName>>.get<<FunctionName>>DDL();
                _logger.LogInformation($"database call done successfully with {<<FunctionName>>List?.Count()}");
                return Ok(<<FunctionName>>List);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpGet]
        [ActionName("get<<FunctionName>>ById")]
        public async Task<IActionResult> get<<FunctionName>>ById(string Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var <<FunctionName>>List = await <<RepositoryInstanceName>>.get<<FunctionName>>ById(Id);
                _logger.LogInformation($"database call done successfully with {<<FunctionName>>List?.Id}");
                if (<<FunctionName>>List == null)
                {
                    return NotFound();
                }
                return Ok(<<FunctionName>>List);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/add<<FunctionName>>")]
        public async Task<IActionResult> add<<FunctionName>>(<<FunctionName>> <<FunctionName>>Details)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var <<FunctionInstanceName>> = new <<FunctionName>>()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var <<FunctionName>>DTO = await <<RepositoryInstanceName>>.add<<FunctionName>>(<<FunctionName>>Details);
                _logger.LogInformation($"database call done successfully with {<<FunctionName>>DTO?.Id}");
                return Ok(<<FunctionName>>DTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> delete<<FunctionName>>(string Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await <<RepositoryInstanceName>>.delete<<FunctionName>>(Id);
                _logger.LogInformation($"database call done successfully with {deletedItem}");
                if (deletedItem == null)
                {
                    return NotFound();
                }
                return Ok(deletedItem);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> update<<FunctionName>>([FromRoute] string Id, [FromBody] <<FunctionName>> deleteRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                // var role = new <<FunctionName>>()
                // {
                    // Name = deleteAuthRoleRequest.Name,
                    // ApplicationId = deleteAuthRoleRequest.ApplicationId,
                    // Status = deleteAuthRoleRequest.Status
                // };

                <<FunctionName>>DTO <<FunctionInstanceName>>DTO = await <<RepositoryInstanceName>>.update<<FunctionName>>(Id, role);
                _logger.LogInformation($"database call done successfully with {<<FunctionInstanceName>>DTO}");
                if (<<FunctionInstanceName>>DTO == null) 
                { 
                    return NotFound(); 
                }
                return Ok(<<FunctionInstanceName>>DTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
