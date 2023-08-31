using Dapper;
using ExcelData.DataAccess.Interfaces.Users;
using ExcelData.DataAccess.Utils;
using ExcelData.DataAccess.ViewModels.Users;
using ExcelData.Domain.Entities.Users;

namespace ExcelData.DataAccess.Repositories.Users
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from users";
                var result = await _connection.QuerySingleAsync<long>(query);
                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> CreateAsync(User entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.users(" +
                    " firstname, lastname, phonenumber, birthdate, passportserianumber, region, district, address, created_at, updated_at) " +
                    $"VALUES (@FirstName, @LastName, @PhoneNumber, '{entity.BirthDate.Year}-{entity.BirthDate.Month}-{entity.BirthDate.Day}', " +
                    " @PassportSeriaNumber, @Region, @District, @Address, @CreatedAt, @UpdatedAt);";
                return await _connection.ExecuteAsync(query, entity);
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"delete from users where id = {id}";
                return await _connection.ExecuteAsync(query);
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IList<User>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM users order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
                var result = (await _connection.QueryAsync<User>(query)).ToList();
                return result;
            }
            catch
            {
                return new List<User>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<UserViewModel?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM users where id = @Id";
                var result = await _connection.QuerySingleAsync<UserViewModel>(query, new { Id = id });
                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }


        public async Task<int> UpdateAsync(long id, User entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"UPDATE public.users " +
                    $"SET firstname=@FirstName, lastname = @LastName, phonenumber = @PhoneNumber, birthdate = @BirthDate, " +
                    $"passportserianumber = @PassportSeriaNumber, region = @Region, district = @District, address = @Address," +
                    $" created_at=@CreatedAt, updated_at=@UpdatedAt " +
                    $"WHERE id={id};";
                var result = await _connection.ExecuteAsync(query, entity);
                return result;
            }
     
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }
    }
}
