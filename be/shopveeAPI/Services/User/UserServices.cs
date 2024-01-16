using System.ComponentModel.DataAnnotations;
using shopveeAPI.Services.User.Dto.Request;
using CommonLibs;
using Microsoft.AspNetCore.Mvc;

namespace shopveeAPI.Services.User;

using DbContext;
using Entities;
using CommonLibs;

public class UserServices : IUserServices
{
    private readonly ShopveeDbContext _dbContext;

    public UserServices(ShopveeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetUser()
    {
        var entities = new List<User>();
        try
        {
            entities = _dbContext.User.ToList();
            return entities;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<User?> AddUserAsync(UserRequest request)
    {
        try
        {
            if (!Common.CheckXSSInput(request.Email) || Common.CheckXSSInput(request.Password))
            {
                return null;
            }

            var user = new User()
            {
                Email = request.Email,
                Password = request.Password
            };
            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<int> DeleteUserAsync(Guid id)
    {
        try
        {
            var deleteProduct = await _dbContext.User.FindAsync(id);
            if (deleteProduct == null)
            {
                return -1;
            }

            _dbContext.User.Remove(deleteProduct);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}