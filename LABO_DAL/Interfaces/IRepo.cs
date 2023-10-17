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
        IEnumerable<UseDTO> Get();
        UseDTO GetById(int id);
        void Create(UseDTO item);
        bool Delete(int id);
        void Update(UseDTO item);
    }
}
