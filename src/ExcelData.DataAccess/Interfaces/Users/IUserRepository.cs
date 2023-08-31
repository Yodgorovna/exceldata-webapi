using ExcelData.DataAccess.Utils;
using ExcelData.DataAccess.ViewModels.Users;
using ExcelData.Domain.Entities.Users;

namespace ExcelData.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User, UserViewModel>
{
    Task<IList<User>> GetAllAsync(PaginationParams @params);
}
