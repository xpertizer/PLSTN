using System;
namespace PLSTN.Model
{
    public class Funcionario
    {
        public string matricula { get; set; }
        public string nome { get; set; }
        public EnumPesoArea area { get; set; }
        public string cargo { get; set; }
        public double salariobruto { get; set; }
        public DateTime datadeadmissao { get; set; }
        public Funcionario()
        {
            

    }
    }
}
