using DataBase;
using MyCompany.MyProject.DataBase.Models;

namespace MyCompany.MyProject.DataRepository.Interface
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly MyProjectContext _context;
        private GenericRepository<Users> _usersRepository;       
        public UnitOfWorks(MyProjectContext context)
        {
            _context = context;
        }
        public IGenericRepository<Users> UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new GenericRepository<Users>(_context);
                }
                return _usersRepository;
            }
        }

        public bool SaveChanges => _context.SaveChanges() > 0 ? true : false;
    }
}
