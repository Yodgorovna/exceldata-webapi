using ExcelData.DataAccess.Utils;
using ExcelData.DataAccess.ViewModels.Users;
using ExcelData.Domain.Entities.Users;
using ExcelData.Service.Dtos.Users;

namespace ExcelData.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> CreateAsync(UserCreateDto dto);

    public Task<bool> DeleteAsync(long UserId);

    public Task<long> CountAsync();

    public Task<IList<User>> GetAllAsync(PaginationParams @params);

    public Task<UserViewModel> GetByIdAsync(long UserId);

    public Task<bool> UpdateAsync(long UserId, UserUpdateDto dto);
}
