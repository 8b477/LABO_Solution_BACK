using LABO_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABO_DAL.Interfaces
{
    public interface IUserRepo
    {
        IEnumerable<UserModel> Get();
        UserModel GetById(int id);
        void Create(UserModel item);
        bool Delete(int id);
        void Update(UserModel item);
    }
}
