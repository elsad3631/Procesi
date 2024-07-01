using ImmobiliareApi.Entities;
using ImmobiliareApi.Interfaces;
using ImmobiliareApi.Interfaces.IBusinessServices;
using ImmobiliareApi.Models.BuildingModels;
using ImmobiliareApi.Models.CustomerModels;
using ImmobiliareApi.Models.ListViewModel;
using ImmobiliareApi.Models.ResponseModel;
using ImmobiliareApi.Services;
using ImmobiliareApi.Services.BusinessServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmobiliareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBuildingServices _buildingServices;
        private readonly ILogger<BuildingsController> _logger;

        public BuildingsController(
           IConfiguration configuration,
           IBuildingServices buildingServices,
            ILogger<BuildingsController> logger)
        {
            _configuration = configuration;
            _buildingServices = buildingServices;
            _logger = logger;
        }
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(BuildingCreateModel request)
        {
            try
            {
                await _buildingServices.Create(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseModel() { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost]
        [Route(nameof(Update))]
        public async Task<IActionResult> Update(BuildingUpdateModel request)
        {
            try
            {
                BuildingSelectModel Result = await _buildingServices.Update(request);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseModel() { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet]
        [Route(nameof(Get)), Authorize]
        public async Task<IActionResult> Get(int currentPage, string? filterRequest)
        {
            try
            {
                //currentPage = currentPage > 0 ? currentPage : 1;
                ListViewModel<BuildingSelectModel> res = await _buildingServices.Get(currentPage, filterRequest, null, null);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseModel() { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet]
        [Route(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                BuildingSelectModel result = new BuildingSelectModel();
                result = await _buildingServices.GetById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseModel() { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost]
        [Route(nameof(Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Building result = await _buildingServices.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseModel() { Status = "Error", Message = ex.Message });
            }
        }

    }
}
