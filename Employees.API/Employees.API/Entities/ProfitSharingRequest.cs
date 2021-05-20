using System;
using System.Collections.Generic;

namespace Employees.API.Entities
{
    public class ProfitSharingRequest
    {
        public ProfitSharingRequest()
        {
        }

        //"total_de_funcionarios": "XXXX",
        //"total_distribuido": "R$ XXXX",
        //"total_disponibilizado": "R$ XXXX",
        //"saldo_total_disponibilizado": "R$ XXXX",

        public Decimal ValorMaximoADistribuir { get; set; }
        public Decimal SalarioMinimoAtual { get; set; }


    }
}
