using AutoMapper;
using Domain.Entities.ModelAi;
using Domain.Exceptions;
using Infrastructure.DataSource.ApiClient.Base;
using Infrastructure.DataSource.ApiClientFactory;
using Infrastructure.Nswag;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataSource.ApiClient.Payment
{
    public class ModelAiApiClient : BuildApiClient<ModelAiClient>
    {

        public ModelAiApiClient(ClientFactory clientFactory, IMapper mapper, IConfiguration config) : base(clientFactory, mapper, config)
        {
        }

        public async Task<ICollection<ModelAiResponseEntity>> GetModelsAiAsync()
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelsAiAsync();
                return _mapper.Map<ICollection<ModelAiResponseEntity>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }
        public  async Task<ICollection<ModelAiResponseEntity>> GetModelsByCategoryAsync(string category)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelsByCategoryAsync(category);
                return _mapper.Map<ICollection<ModelAiResponseEntity>>(response);
            }
            catch (ApiException e) {

                throw new ServerException(e.Message,e.StatusCode);
            }
        }

        //public async Task<ModelAiResponseEntity> CreateModelAiAsync(ModelAiCreateEntity model)
        //{

        //    try
        //    {
        //        var client = await GetApiClient();
        //        var response = await client.CreateModelAiAsync(id);
        //        return _mapper.Map<ModelAiResponseEntity>(response);
        //    }
        //    catch (ApiException e)
        //    {

        //        throw new ServerException(e.Message, e.StatusCode);
        //    }
        //}


        //public async Task<ModelAiResponseEntity> UpdateModelAiAsync(ModelAiUpdateEntity model)
        //{

        //    try
        //    {
        //        var client = await GetApiClient();
        //        var response = await client.UpdateModelAiAsync(id);
        //        return _mapper.Map<ModelAiResponseEntity>(response);
        //    }
        //    catch (ApiException e)
        //    {

        //        throw new ServerException(e.Message, e.StatusCode);
        //    }
        //}

        public async Task<ModelAiResponseEntity> GetModelAiAsync(string id)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelAiAsync(id);
                return _mapper.Map<ModelAiResponseEntity>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }

        public async Task<ItemEntity> GetStartStudioAsync(string lg)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetStartStudioAsync(lg);
                return _mapper.Map<ItemEntity>(response);
            }
            catch (ApiException e)
            {
                throw new ServerException(e.Message, e.StatusCode);
            }
        }

        public async Task<ICollection<ValueFilterModelEntity>> GetValueFilterServiceAsync(string lg)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetValueFilterServiceAsync(lg);
                return _mapper.Map<ICollection<ValueFilterModelEntity>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }


        public async Task<ModelPropertyValuesEntity> GetSettingModelAiAsync(string lg)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetSettingModelAiAsync(lg);
                return _mapper.Map<ModelPropertyValuesEntity>(response);
            }
            catch (ApiException e)
            {
                throw new ServerException(e.Message, e.StatusCode);
            }
        }
        public async Task<IDictionary<string, object>> GetModelChatStudioAsync(string lg)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelChatStudioAsync(lg);
                return _mapper.Map<IDictionary<string, object>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }


        public async Task<ICollection<ModelAiResponseEntity>> GetModelsByDialectAsync(string dialect)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelsByDialectAsync(dialect);
                return _mapper.Map<ICollection<ModelAiResponseEntity>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }
        public async Task<ICollection<ModelAiResponseEntity>> GetModelsByGenderAsync(string gender)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelsByGenderAsync(gender);
                return _mapper.Map<ICollection<ModelAiResponseEntity>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }

        public async Task<ICollection<ModelAiResponseEntity>> GetModelsByLanguageAsync(string lg)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelsByLanguageAsync(lg);
                return _mapper.Map<ICollection<ModelAiResponseEntity>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }
        public async Task<ICollection<ModelAiResponseEntity>> GetModelsByIsStandardAsync(string isStandard)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelsByIsStandardAsync(isStandard);
                return _mapper.Map<ICollection<ModelAiResponseEntity>>(response);
            }
            catch (ApiException e)
            {
                throw new ServerException(e.Message, e.StatusCode);
            }
        }

        public async Task<ICollection<ModelAiResponseEntity>> GetModelsByLanguageAndDialectAsync(string language, string dialect)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelsByLanguageAndDialectAsync(language,dialect);
                return _mapper.Map<ICollection<ModelAiResponseEntity>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }

        public async Task<ICollection<ModelAiResponseEntity>> GetModelsByTypeAndGenderAsync(string type, string gender)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelsByTypeAndGenderAsync(type, gender);
                return _mapper.Map<ICollection<ModelAiResponseEntity>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }

        public async Task<ICollection<ModelAiResponseEntity>> GetModelsByLanguageDialectTypeAsync(string language, string dialect,string type)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelsByLanguageDialectTypeAsync(language, dialect,type);
                return _mapper.Map<ICollection<ModelAiResponseEntity>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }

        public async Task<IDictionary<string, object>> GetModelSpeechStudioAsync(string lg)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelSpechStudioAsync(lg);
                return _mapper.Map<IDictionary<string, object>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }

        public async Task<IDictionary<string, object>> GetModelTextStudioAsync(string lg)
        {

            try
            {
                var client = await GetApiClient();
                var response = await client.GetModelTextStudioAsync(lg);
                return _mapper.Map<IDictionary<string, object>>(response);
            }
            catch (ApiException e)
            {

                throw new ServerException(e.Message, e.StatusCode);
            }
        }

       
    }
}
