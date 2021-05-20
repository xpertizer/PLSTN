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
        public Double TotalFuncionarios { get; set; }
        public Double TotalDistribuido { get; set; }
        public Double TotalDisponibilizado { get; set; }
        public Double SaldoTotalDisponibilizado { get; set; }
    }
}
