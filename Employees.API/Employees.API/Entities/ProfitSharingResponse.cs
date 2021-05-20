using System;
using System.Collections.Generic;

namespace Employees.API.Entities
{
    public class ProfitSharingResponse
    {
        public ProfitSharingResponse()
        {
        }

        public List<Participacao> Participacoes { get; set; }
        public Decimal TotalFuncionarios { get; set; }
        public Decimal TotalDistribuido { get; set; }
        public Decimal TotalDisponibilizado { get; set; }
        public Decimal SaldoTotalDisponibilizado { get; set; }
    }
}
