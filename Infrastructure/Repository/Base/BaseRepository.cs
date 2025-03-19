using AutoMapper;
using Shared.Settings;

namespace Infrastructure.Repository.Base
{
    public class BaseRepository<T>
    {
        protected readonly IMapper _mapper;
        protected readonly ApplicationModeService _appModeService;
        protected readonly T _apiClient;
        public BaseRepository(IMapper mapper, ApplicationModeService appModeService, T apiClient)
        {
            _mapper = mapper;
            _appModeService = appModeService;
            _apiClient = apiClient;
        }
    }

}
