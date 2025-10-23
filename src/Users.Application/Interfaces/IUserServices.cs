using static Users.Application.Dtos.UserDtos;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateAsync(CreateUserRequest request, CancellationToken ct);
        Task<IReadOnlyList<UserItem>> ListAsync(UserListRequest request, CancellationToken ct);
        Task<UserItem?> GetAsync(GetUserRequest request, CancellationToken ct);
        Task<bool> UpdateAsync(UpdateUserRequest request, CancellationToken ct);
        Task<bool> DeleteAsync(DeleteUserRequest request, CancellationToken ct);
    }
}
