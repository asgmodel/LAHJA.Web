using Domain.Entities.Profile;
using Domain.Repository.Profile;
using Domain.Wrapper;

namespace Application.UseCase.Plans
{
    public class UpdateProfileUseCase
    {
        private readonly IProfileRepository repository;

        public UpdateProfileUseCase(IProfileRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<ProfileResponse>> ExecuteAsync(ProfileRequest profileRequest)
        {
            // يمكنك تعديل بروفايل مستخدم موجود بناءً على البيانات من ProfileRequest
            return await repository.UpdateProfileAsync(profileRequest);
        }
    }



}
