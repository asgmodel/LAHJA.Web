using AutoMapper;
using Infrastructure.DataSource.ApiClientFactory;
using Infrastructure.Nswag;
using Microsoft.Extensions.Configuration;
using Infrastructure.DataSource.ApiClient.Base;
using Infrastructure.Models.AuthorizationSession;
using Domain.ShareData.Base;
using Domain.Wrapper;
using Newtonsoft.Json.Linq;
using Domain.Exceptions;




namespace Infrastructure.DataSource.ApiClient.AuthorizationSession
{
    public class AuthorizationSessionApiClient : BuildApiClient<AuthorizationSessionClient>
    {




        public AuthorizationSessionApiClient(ClientFactory clientFactory, IMapper mapper, IConfiguration config) : base(clientFactory, mapper, config)
        {

        }
        public async Task<List<SessionTokenAuthResponseModel>> GetSessionsAsync()
        {
            try
            {
                var client = await GetApiClient();
                var response = await client.GetSessionsAsync();
                var resModel = _mapper.Map<List<SessionTokenAuthResponseModel>>(response?.ToList());

                return resModel;
            }
            catch (ApiException ex)
            {
                throw new ServerException(ex.Message, ex.StatusCode);
            }
        

        }
        public async Task<AuthorizationSessionWebResponseModel> CreateAuthorizationSessionAsync(AuthorizationWebRequestModel request)
        {
            
                try
                {
                    var client = await GetApiClient();
                    var model = _mapper.Map<CreateAuthorizationWebRequest>(request);
                    var response = await client.CreateAuthorizationSessionAsync(model);
                    var resModel = _mapper.Map<AuthorizationSessionWebResponseModel>(response);

                    return resModel;
                }
                catch (ApiException<ApiExceptionResult> ex)
                {
                    throw new ServerException(ex.Result.Message, ex.StatusCode);  
                }    
                catch (HttpRequestException e)
                {
                    throw new ServerException(e.Message, (int)(e.StatusCode??0));
                }
                catch (Exception e)
                {
                    throw;
                }
     
            
        }
        public async Task<DeleteResponse> PauseAuthorizationSessionAsync(string id)
        {

   
            try
            {
                var client = await GetApiClient();
                var response = await client.PauseAuthorizationSessionAsync(id);
                var resModel = _mapper.Map<DeleteResponse>(response);
                return resModel;
            }
            catch (ApiException ex)
            {
                throw new ServerException(ex.Message, ex.StatusCode);  
            }

        }
        public async Task<DeleteResponse> ResumeAuthorizationSessionAsync(string id)
        {
            try
            {
                var client = await GetApiClient();
                var response = await client.ResumeAuthorizationSessionAsync(id);
                var resModel = _mapper.Map<DeleteResponse>(response);
                return resModel;
            }
            catch (ApiException ex)
            {
                throw new ServerException(ex.Message, ex.StatusCode);
            }
    

        }
        public async Task<AuthorizationSessionCoreResponseModel> AuthorizationSessionAsync(ValidateTokenRequestModel request)
        {
            try
            {
                var client = await GetApiClient();
                var model = _mapper.Map<ValidateTokenRequest>(request);
                var response = await client.AuthorizationSessionAsync(model);
                var resModel = _mapper.Map<AuthorizationSessionCoreResponseModel>(response);
                return resModel;
            }
            catch (ApiException ex)
            {
                throw new ServerException(ex.Message, ex.StatusCode);
            }

    
          
        }
        public async Task ValidateSessionTokenAsync(string  token)
        {
            try
            {
                var client = await GetApiClient();

                await client.ValidateSessionTokenAsync(token);
            }
            catch (ApiException ex)
            {
                throw new ServerException(ex.Message, ex.StatusCode);
            }
     

        }
        public async Task<AuthorizationSessionEncryptResponseModel> EncryptFromWebAsync()
        {
            try
            {
                var client = await GetApiClient();
                var response = await client.EncryptFromWebAsync(new EncryptTokenRequest { AuthorizationType = "internal", Expires = DateTimeOffset.UtcNow });
                return new AuthorizationSessionEncryptResponseModel() { EncrptedToken = response.EncryptedToken };
            }
            catch (ApiException ex)
            {
                throw new ServerException(ex.Message, ex.StatusCode);
            }
         
        }

        public async Task<AuthorizationSessionEncryptResponseModel> EncryptFromCoreAsync(string sessionToken)
        {
            try
            {
                var client = await GetApiClient();
                var response = await client.EncryptFromCoreAsync(sessionToken);
                return new AuthorizationSessionEncryptResponseModel() { EncrptedToken = response.EncryptedToken };
            }
            catch (ApiException ex)
            {
                throw new ServerException(ex.Message, ex.StatusCode);
            }

    
        }

        public async Task<DeleteResponse> DeleteAuthorizationSessionAsync(string sessionId)
        {
            try
            {
                var client = await GetApiClient();
                var response = await client.DeleteAuthorizationSessionAsync(sessionId);
                var resModel = _mapper.Map<DeleteResponse>(response);
                return resModel;
            }
            catch (ApiException ex)
            {
                throw new ServerException(ex.Message, ex.StatusCode);
            }
       
            
        }


     
        //public async Task<Result<bool>> ValidateSessionTokenAsync(string token)
        //{
        //    try
        //    {
        //        var client = await GetApiClient();  // الحصول على العميل

        //        await client.ValidateSessionTokenAsync(token);

        //        return Result<bool>.Success(true);  // إرجاع النتيجة بنجاح
        //    }
        //    catch (ApiException ex)
        //    {
        //        return Result<bool>.Fail(ex.Response, ex.StatusCode);  // في حال حدوث خطأ API
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result<bool>.Fail(ex.Message);  // في حال حدوث أي خطأ عام
        //    }
        //}
        //public async Task<Result<string>> EncryptFromWebAsync()
        //{
        //    try
        //    {
        //        var client = await GetApiClient();  // الحصول على العميل
        //        await client.EncryptFromWebAsync();  // استدعاء API لتشفير البيانات من الويب

        //        return Result<string>.Success();  // إرجاع النتيجة بنجاح
        //    }
        //    catch (ApiException ex)
        //    {
        //        return Result<string>.Fail(ex.Response, ex.StatusCode);  // في حال حدوث خطأ API
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result<string>.Fail(ex.Message);  // في حال حدوث أي خطأ عام
        //    }
        //}

        //public async Task<Result<string>> EncryptFromCoreAsync(string sessionToken)
        //{
        //    try
        //    {
        //        var client = await GetApiClient();  // الحصول على العميل
        //        await client.EncryptFromCoreAsync(sessionToken);  // استدعاء API لتشفير البيانات من الCore

        //        return Result<string>.Success();  // إرجاع النتيجة بنجاح
        //    }
        //    catch (ApiException ex)
        //    {
        //        return Result<string>.Fail(ex.Response, ex.StatusCode);  // في حال حدوث خطأ API
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result<string>.Fail(ex.Message);  // في حال حدوث أي خطأ عام
        //    }
        //}

        //public async Task<Result<DeleteResponse>> DeleteAuthorizationSessionAsync(string sessionId)
        //{
        //    try
        //    {
        //        var client = await GetApiClient();
        //        var response = await client.DeleteAuthorizationSessionAsync(sessionId);
        //        var resModel = _mapper.Map<DeleteResponse>(response);
        //        return Result<DeleteResponse>.Success(resModel);
        //    }
        //    catch (ApiException ex)
        //    {
        //        return Result<DeleteResponse>.Fail(ex.Response, ex.StatusCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result<DeleteResponse>.Fail(ex.Message);
        //    }
        //}
    }
}
