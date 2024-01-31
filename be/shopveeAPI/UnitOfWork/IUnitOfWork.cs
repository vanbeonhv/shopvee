using shopveeAPI.Services.User;

namespace shopveeAPI.UnitOfWork;

public interface IUnitOfWork
{
    // public IUserServices userServices { get; set; }
    public IUserGenericService _userGenericService { get; set; }

    int SaveChange();
}