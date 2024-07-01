using AutoMapper;
using ImmobiliareApi.Entities.Typologies;
using ImmobiliareApi.Interfaces;
using ImmobiliareApi.Interfaces.IBusinessServices.ITypologiesServices;
using ImmobiliareApi.Models.BuildingModels;
using ImmobiliareApi.Models.ListViewModel;
using ImmobiliareApi.Models.PaginationOptions;
using ImmobiliareApi.Models.TypologiesModels.BuildingTypeModels;
using ImmobiliareApi.Services.BusinessServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ImmobiliareApi.Services.TypologiesServices
{
    public class BuildingTypeServices : IBuildingTypeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BuildingTypeServices> _logger;
        private readonly IOptionsMonitor<PaginationOptions> options;
        public BuildingTypeServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BuildingTypeServices> logger, IOptionsMonitor<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            this.options = options;
        }

        public async Task Create(BuildingTypeCreateModel dto)
        {
            try
            {
                var entityClass = _mapper.Map<BuildingType>(dto);
                await _unitOfWork.BuildingTypeRepository.InsertAsync(entityClass);
                _unitOfWork.Save();

                _logger.LogInformation(nameof(Create));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Si è verificato un errore in fase creazione");
            }
        }

        public async Task<BuildingType> Delete(int id)
        {
            try
            {
                IQueryable<BuildingType> query = _unitOfWork.dbContext.BuildingTypes;

                if (id == 0)
                    throw new NullReferenceException("L'id non può essere 0");

                query = query.Where(x => x.Id == id);

                BuildingType EntityClasses = await query.FirstOrDefaultAsync();

                if (EntityClasses == null)
                    throw new NullReferenceException("Record non trovato!");

                _unitOfWork.BuildingTypeRepository.Delete(EntityClasses);
                await _unitOfWork.SaveAsync();
                _logger.LogInformation(nameof(Delete));

                return EntityClasses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                if (ex.InnerException.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    throw new Exception("Impossibile eliminare il record perché è utilizzato come chiave esterna in un'altra tabella.");
                }
                if (ex is NullReferenceException)
                {
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("Si è verificato un errore in fase di eliminazione");
                }
            }
        }

        public async Task<ListViewModel<BuildingTypeSelectModel>> Get(int currentPage, string? filterRequest, char? fromName, char? toName)
        {
            try
            {
                IQueryable<BuildingType> query = _unitOfWork.dbContext.BuildingTypes;

                if (!string.IsNullOrEmpty(filterRequest))
                    query = query.Where(x => x.Description.Contains(filterRequest));

                if (fromName != null)
                {
                    string fromNameString = fromName.ToString();
                    query = query.Where(x => string.Compare(x.Description.Substring(0, 1), fromNameString) >= 0);
                }

                if (toName != null)
                {
                    string toNameString = toName.ToString();
                    query = query.Where(x => string.Compare(x.Description.Substring(0, 1), toNameString) <= 0);
                }

                ListViewModel<BuildingTypeSelectModel> result = new ListViewModel<BuildingTypeSelectModel>();

                result.Total = await query.CountAsync();

                if (currentPage > 0)
                {
                    query = query
                    .Skip((currentPage * options.CurrentValue.BuildingItemPerPage) - options.CurrentValue.BuildingItemPerPage)
                            .Take(options.CurrentValue.BuildingItemPerPage);
                }

                List<BuildingType> queryList = await query.ToListAsync();


                result.Data = _mapper.Map<List<BuildingTypeSelectModel>>(queryList);

                _logger.LogInformation(nameof(Get));

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Si è verificato un errore");
            }
        }

        public async Task<BuildingTypeSelectModel> GetById(int id)
        {
            try
            {
                if (id is not > 0)
                    throw new Exception("Si è verificato un errore!");

                var query = await _unitOfWork.dbContext.BuildingTypes.FirstOrDefaultAsync(x => x.Id == id);

                BuildingTypeSelectModel result = _mapper.Map<BuildingTypeSelectModel>(query);

                _logger.LogInformation(nameof(GetById));

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Si è verificato un errore");
            }
        }

        public async Task<BuildingTypeSelectModel> Update(BuildingTypeUpdateModel dto)
        {
            try
            {
                var EntityClass =
                    await _unitOfWork.BuildingTypeRepository.FirstOrDefaultAsync(q => q.Where(x => x.Id == dto.Id));

                if (EntityClass == null)
                    throw new NullReferenceException("Record non trovato!");

                EntityClass = _mapper.Map(dto, EntityClass);

                _unitOfWork.BuildingTypeRepository.Update(EntityClass);
                await _unitOfWork.SaveAsync();

                BuildingTypeSelectModel response = new BuildingTypeSelectModel();
                _mapper.Map(EntityClass, response);

                _logger.LogInformation(nameof(Update));

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                if (ex is NullReferenceException)
                {
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("Si è verificato un errore in fase di modifica");
                }
            }
        }
    }
}
