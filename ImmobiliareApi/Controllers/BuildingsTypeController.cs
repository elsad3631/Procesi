using ImmobiliareApi.Entities.Typologies;
using ImmobiliareApi.Interfaces.IBusinessServices;
using ImmobiliareApi.Interfaces.IBusinessServices.ITypologiesServices;
using ImmobiliareApi.Models.BuildingModels;
using ImmobiliareApi.Models.ListViewModel;
using ImmobiliareApi.Models.ResponseModel;
using ImmobiliareApi.Models.TypologiesModels.BuildingTypeModels;
using Microsoft.AspNetCore.Mvc;

namespace ImmobiliareApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsTypeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IBuildingTypeServices _buildingTypeServices;
        private readonly ILogger<BuildingsTypeController> _logger;

        public BuildingsTypeController(
           IConfiguration configuration,
           IBuildingTypeServices buildingTypeServices,
            ILogger<BuildingsTypeController> logger)
        {
            _configuration = configuration;
            _buildingTypeServices = buildingTypeServices;
            _logger = logger;
        }
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(BuildingTypeCreateModel request)
        {
            try
            {
                await _buildingTypeServices.Create(request);
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
        public async Task<IActionResult> Update(BuildingTypeUpdateModel request)
        {
            try
            {
                BuildingTypeSelectModel Result = await _buildingTypeServices.Update(request);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseModel() { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet]
        [Route(nameof(Get))]
        public async Task<IActionResult> Get(int currentPage, string? filterRequest)
        {
            try
            {
                //currentPage = currentPage > 0 ? currentPage : 1;
                ListViewModel<BuildingTypeSelectModel> res = await _buildingTypeServices.Get(currentPage, filterRequest, null, null);

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
                BuildingTypeSelectModel result = new BuildingTypeSelectModel();
                result = await _buildingTypeServices.GetById(id);

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
                BuildingType result = await _buildingTypeServices.Delete(id);
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
