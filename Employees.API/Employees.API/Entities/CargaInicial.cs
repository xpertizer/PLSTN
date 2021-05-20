using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Employees.API.Entities
{
    public class CargaInicial
    {
        [JsonProperty("Funcionarios")]
        public List<CargaInicialFuncionarios> Funcionarios{ get; set; }
    }
}
