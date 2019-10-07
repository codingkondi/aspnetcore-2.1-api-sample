using MyCompany.MyProject.DataBase.Models;

namespace MyCompany.MyProject.DataRepository.Interface
{
    public interface IUnitOfWorks
    {
        IGenericRepository<Users> UsersRepository { get; }

        bool SaveChanges { get; }
    }
}
