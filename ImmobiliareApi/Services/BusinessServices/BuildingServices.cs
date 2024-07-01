using AutoMapper;
using ImmobiliareApi.Entities;
using ImmobiliareApi.Interfaces;
using ImmobiliareApi.Interfaces.IBusinessServices;
using ImmobiliareApi.Models.BuildingModels;
using ImmobiliareApi.Models.CustomerModels;
using ImmobiliareApi.Models.ListViewModel;
using ImmobiliareApi.Models.PaginationOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ImmobiliareApi.Services.BusinessServices
{
    public class BuildingServices : IBuildingServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BuildingServices> _logger;
        private readonly IOptionsMonitor<PaginationOptions> options;
        public BuildingServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BuildingServices> logger, IOptionsMonitor<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            this.options = options;
        }

        public async Task Create(BuildingCreateModel dto)
        {
            try
            {
                var entityClass = _mapper.Map<Building>(dto);
                await _unitOfWork.BuildingRepository.InsertAsync(entityClass);
                _unitOfWork.Save();

                _logger.LogInformation(nameof(Create));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Si è verificato un errore in fase creazione");
            }
        }

        public async Task<Building> Delete(int id)
        {
            try
            {
                IQueryable<Building> query = _unitOfWork.dbContext.Buildings;

                if (id == 0)
                    throw new NullReferenceException("L'id non può essere 0");

                query = query.Where(x => x.Id == id);

                Building EntityClasses = await query.FirstOrDefaultAsync();

                if (EntityClasses == null)
                    throw new NullReferenceException("Record non trovato!");

                _unitOfWork.BuildingRepository.Delete(EntityClasses);
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

        public async Task<ListViewModel<BuildingSelectModel>> Get(int currentPage, string? filterRequest, char? fromName, char? toName)
        {
            try
            {
                IQueryable<Building> query = _unitOfWork.dbContext.Buildings;

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

                ListViewModel<BuildingSelectModel> result = new ListViewModel<BuildingSelectModel>();

                result.Total = await query.CountAsync();

                if (currentPage > 0)
                {
                    query = query
                    .Skip((currentPage * options.CurrentValue.BuildingItemPerPage) - options.CurrentValue.BuildingItemPerPage)
                            .Take(options.CurrentValue.BuildingItemPerPage);
                }

                List<Building> queryList = await query
                    .Include(x => x.BuildingType).ToListAsync();


                result.Data = _mapper.Map<List<BuildingSelectModel>>(queryList);

                _logger.LogInformation(nameof(Get));

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Si è verificato un errore");
            }
        }

        public async Task<BuildingSelectModel> GetById(int id)
        {
            try
            {
                if (id is not > 0)
                    throw new Exception("Si è verificato un errore!");

                var query = await _unitOfWork.dbContext.Buildings
                    .Include(x => x.BuildingType).FirstOrDefaultAsync(x => x.Id == id);

                BuildingSelectModel result = _mapper.Map<BuildingSelectModel>(query);

                _logger.LogInformation(nameof(GetById));

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Si è verificato un errore");
            }
        }

        public async Task<BuildingSelectModel> Update(BuildingUpdateModel dto)
        {
            try
            {
                var EntityClass =
                    await _unitOfWork.BuildingRepository.FirstOrDefaultAsync(q => q.Where(x => x.Id == dto.Id));

                if (EntityClass == null)
                    throw new NullReferenceException("Record non trovato!");

                EntityClass = _mapper.Map(dto, EntityClass);

                _unitOfWork.BuildingRepository.Update(EntityClass);
                await _unitOfWork.SaveAsync();

                BuildingSelectModel response = new BuildingSelectModel();
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
