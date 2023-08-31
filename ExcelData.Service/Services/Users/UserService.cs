using ExcelData.DataAccess.Interfaces.Users;
using ExcelData.DataAccess.Utils;
using ExcelData.DataAccess.ViewModels.Users;
using ExcelData.Domain.Entities.Users;
using ExcelData.Domain.Exceptions.Users;
using ExcelData.Service.Common.Helpers;
using ExcelData.Service.Dtos.Users;
using ExcelData.Service.Interfaces.Users;

namespace ExcelData.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository userRepository)
    {
        this._repository = userRepository;
    }
    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(UserCreateDto dto)
    {
        User user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            BirthDate = dto.BirthDate,
            PassportSeriaNumber = dto.PassportSeriaNumber,
            Region = dto.Region,
            District = dto.District,
            Address = dto.Address,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _repository.CreateAsync(user);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long UserId)
    {
        var user = await _repository.GetByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException();
        var dbResult = await _repository.DeleteAsync(UserId);
        return dbResult > 0;
    }

    public async Task<IList<User>> GetAllAsync(PaginationParams @params)
    {
        var users = await _repository.GetAllAsync(@params);
        return users;
    }


    public async Task<UserViewModel> GetByIdAsync(long UserId)
    {
        var user = await _repository.GetByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException();
        else return user;
    }

    public async Task<bool> UpdateAsync(long UserId, UserUpdateDto dto)
    {
        var user = await _repository.GetByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException();

        // parse new items to discount
        var newUser = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            BirthDate = dto.BirthDate,
            PassportSeriaNumber = dto.PassportSeriaNumber,
            Region = dto.Region,
            District = dto.District,
            Address = dto.Address,
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var dbResult = await _repository.UpdateAsync(UserId, newUser); //mapper
        return dbResult > 0;
    }
}
