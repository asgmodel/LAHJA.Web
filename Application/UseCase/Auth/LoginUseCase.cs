using Domain.Repository.Auth;
using Infrastructure.Models.Plans;
using Domain.Wrapper;
using Domain.Entities.Auth.Response;
using Domain.Entities.Auth.Request;

namespace Application.UseCase.Auth
{
    public class LoginUseCase
    {
        private readonly IAuthRepository repository;
        public LoginUseCase(IAuthRepository repository)
        {

            this.repository = repository;
        }


        public async Task<Result<LoginResponse>> ExecuteAsync(LoginRequest request)
        {

            var data = await repository.loginAsync(request);
            return data;

            //if (data != null)
            //{
            //    return Result<LoginResponse>.Success(data);
            //}
            //else
            //{
            //    return Result<LoginResponse>.Fail("البريد الالكتروني او كلمة السر  غير صحيح !!");
            //}


        }
      
    }
    }

