using System;
namespace Employees.API.Entities
{
    public class Participacao
    {
        public Participacao()
        {
        }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public Decimal ValorDaParticipacao { get; set; }

    }
}
