using System;
using Employees.API.Entities;

namespace Employees.API.Repository
{
    public class ProfitSharingRules
    {
        private Employee _employee;
        private Decimal _salarioMinimo;
        public ProfitSharingRules(Employee employee, Decimal salariominimo)
        {
            _employee = employee;
            _salarioMinimo = salariominimo;
        }

        public int PesoAreaAtuacao()
        {
            int paa = 1;

            switch (_employee.Area.ToUpper())
            {
                case "DIRETORIA":
                    paa = 1;
                    break;
                case "CONTABILIDADE":
                    paa = 2;
                    break;
                case "FINANCEIRO":
                    paa = 2;
                    break;
                case "TECNOLOGIA":
                    paa = 2;
                    break;
                case "SERVIÇOS GERAIS":
                    paa = 3;
                    break;
                case "SERVICOS GERAIS":
                    paa = 3;
                    break;
                case "RELACIONAMENTO COM O CLIENTE":
                    paa = 5;
                    break;
                default:
                    break;
            }

            return paa;
        }

        public int PesoFaixaSalarial()
        {
            int pfs = 1;
            decimal SalarioBrutoemSalariosMinimos = _employee.SalarioBruto / _salarioMinimo;
            if (SalarioBrutoemSalariosMinimos>8)
            {
                pfs = 5;
            }
            else if(SalarioBrutoemSalariosMinimos > 5)
            {
                pfs = 3;
            }
            else if (SalarioBrutoemSalariosMinimos > 3)
            {
                pfs = 3;
            }
            else
            {
                pfs = 1;
            }
            return pfs;

        }

        public int PesoPorTempoDeAdmissao()
        {
            int pta = 1;

            int tempodeadmissao = new DateTime(DateTime.Now.Subtract(_employee.DataAdmissao).Ticks).Year;

            if (tempodeadmissao<=1)
            {
                pta = 1;
            }
            else if(tempodeadmissao<3){
                pta = 2;
            }
            else if (tempodeadmissao<8)
            {
                pta = 3;
            }
            else
            {
                pta = 5;
            }
            return pta;

        }

        public Decimal ValorDaParticipacao()
        {

            decimal resultadoFormula;



            resultadoFormula = _employee.SalarioBruto * this.PesoPorTempoDeAdmissao();
            resultadoFormula += _employee.SalarioBruto * this.PesoAreaAtuacao();
            resultadoFormula = ((resultadoFormula) / this.PesoFaixaSalarial()) * 12;

            return resultadoFormula;

        }
    }
}

/*
 * 
 * Regras Gerais
Foi estabelecido um peso por área de atuação:
Peso 1: Diretoria;
Peso 2: Contabilidade, Financeiro, Tecnologia; Peso 3: Serviços Gerais;
Peso 5: Relacionamento com o Cliente;
Foi estabelecido um peso por faixa salarial e uma exceção para estagiários:
Peso 5: Acima de 8 salários mínimos;
Peso 3: Acima de 5 salários mínimos e menor que 8 salários mínimos;
Peso 2: Acima de 3 salários mínimos e menor que 5 salários mínimos;
Peso 1: Todos os estagiários e funcionários que ganham até 3 salários mínimos;
Foi estabelecido um peso por tempo de admissão:
Peso 1: Até 1 ano de casa;
Peso 2: Mais de 1 ano e menos de 3 anos;
Peso 3: Acima de 3 anos e menos de 8 anos;
Peso 5: Mais de 8 anos
 */