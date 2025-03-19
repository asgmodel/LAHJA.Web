using AutoMapper;
using Infrastructure.DataSource.ApiClientFactory;
using Infrastructure.Nswag;
using Microsoft.Extensions.Configuration;
using Infrastructure.DataSource.ApiClient.Base;
using Domain.Wrapper;
using Domain.Entities.Profile;
using Domain.ShareData.Base;



namespace Infrastructure.DataSource.ApiClient.AuthorizationSession
{
    public class SpaceApiClient : BuildApiClient<SpaceClient>
    {

        public SpaceApiClient(ClientFactory clientFactory, IMapper mapper, IConfiguration config) : base(clientFactory, mapper, config)
        {
        }

        public async Task<Result<List<ProfileSpaceResponse>>> GetSpacesAsync(FilterResponseData? filter=null)
        {
            try
            {
                var client = await GetApiClient();
          
                var response = await client.GetSpacesAsync();
                var resModel = _mapper.Map<List<ProfileSpaceResponse>>(response);

                return Result<List<ProfileSpaceResponse>>.Success(resModel);
            }
            catch (ApiException ex)
            {
                return Result<List<ProfileSpaceResponse>>.Fail(ex.Response, ex.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<List<ProfileSpaceResponse>>.Fail(ex.Message);
            }
        } 
        public async Task<Result<ProfileSpaceResponse>> GetSpaceAsync(FilterResponseData filter)
        {
            try
            {
                var client = await GetApiClient();
          
                var response = await client.GetSpaceAsync(filter.Id);
                var resModel = _mapper.Map<ProfileSpaceResponse>(response);

                return Result<ProfileSpaceResponse>.Success(resModel);
            }
            catch (ApiException ex)
            {
                return Result<ProfileSpaceResponse>.Fail(ex.Response, ex.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<ProfileSpaceResponse>.Fail(ex.Message);
            }
        } 
        
        public async Task<Result<ProfileSpaceResponse>> CreateSpaceAsync(ProfileSpaceResponse request)
        {
            try
            {
                var client = await GetApiClient();
                var model = _mapper.Map<CreateSpaceRequest>(request);
                var response = await client.CreateSpaceAsync(model);
                var resModel = _mapper.Map<ProfileSpaceResponse>(response);

                return Result<ProfileSpaceResponse>.Success(resModel);
            }
            catch (ApiException ex)
            {
                return Result<ProfileSpaceResponse>.Fail(ex.Response, ex.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<ProfileSpaceResponse>.Fail(ex.Message);
            }
        }

        public async Task<Result<ProfileSpaceResponse>> UpdateSpaceAsync(ProfileSpaceResponse request)
        {
            try
            {
                var client = await GetApiClient();
                var model = _mapper.Map<UpdateSpaceRequest>(request);
                var response = await client.UpdateSpaceAsync(request.Id,model);
                var resModel = _mapper.Map<ProfileSpaceResponse>(response);

                return Result<ProfileSpaceResponse>.Success(resModel);
            }
            catch (ApiException ex)
            {
                return Result<ProfileSpaceResponse>.Fail(ex.Response, ex.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<ProfileSpaceResponse>.Fail(ex.Message);
            }
        }

        public async Task<Result<DeleteResponse>> DeleteSpaceAsync(string id)
        {
            try
            {
                var client = await GetApiClient();
                
                var response = await client.DeleteSpaceAsync(id);
                var resModel = _mapper.Map<DeleteResponse>(response);

                return Result<DeleteResponse>.Success(resModel);
            }
            catch (ApiException ex)
            {
                return Result<DeleteResponse>.Fail(ex.Response, ex.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<DeleteResponse>.Fail(ex.Message);
            }
        }


    }
}
