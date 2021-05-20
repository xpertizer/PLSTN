using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [JsonProperty("ValorMaximoADistribuir")]
        public Double ValorMaximoADistribuir { get; set; }

        [Required]
        [JsonProperty("SalarioMinimoAtual")]
        public Double SalarioMinimoAtual { get; set; }


    }
}
