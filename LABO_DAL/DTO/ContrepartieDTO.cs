using LABO_Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABO_DAL.DTO
{
    public class ContrepartieDTO
    {
        [ColumnName(nameof(IDContrepartie))]
        public int IDContrepartie { get; set; }

        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

        [ColumnName(nameof(Description))]
        public string Description { get; set; }

        [ColumnName(nameof(IDProjet))]
        public int IDProjet { get; set; }
    }


    public class ContrepartiDTOCreate
    {
        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

        [ColumnName(nameof(Description))]
        public string Description { get; set; }
    }


    public class ContrepartiDTOList
    {
        [ColumnName(nameof(Montant))]
        public decimal Montant { get; set; }

        [ColumnName(nameof(Description))]
        public string Description { get; set; }

        [ColumnName(nameof(IDProjet))]
        public int IDProjet { get; set; }
    }
}
