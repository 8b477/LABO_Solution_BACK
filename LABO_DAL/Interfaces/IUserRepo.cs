using LABO_DAL.DTO;
using LABO_DAL.Repositories;
using LABO_Entities;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABO_DAL.Interfaces
{
    public interface IUserRepo : IRepo<UserDTO, UserDTOCreate, UserDTOList, Utilisateur, int, string>
    {

        Task<UserDTO?> Logger(string email, string motDePasse);

        UserDTO ToModelCreate(UserDTOCreate model);
        UserDTOList? ToModelDisplay(UserDTO model);
    }
}
