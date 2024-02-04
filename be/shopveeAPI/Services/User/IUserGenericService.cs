using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Repository;
using shopveeAPI.Services.User.Dto.Request;

namespace shopveeAPI.Services.User;

using Models;

public interface IUserGenericService : IGenericRepository<User>
{

     
}