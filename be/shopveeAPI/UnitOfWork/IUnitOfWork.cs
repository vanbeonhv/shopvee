using shopveeAPI.Services.User;

namespace shopveeAPI.UnitOfWork;

public interface IUnitOfWork
{
    public IUserServices userServices { get; set; }

    int SaveChange();
}