using System;
using System.Collections.Generic;
using System.Linq;
using Employees.API.Entities;

namespace Employees.API.Repository
{
    public class ProfitSharingRules
    {
        private Employee _employee;
        private Double _salarioMinimo;
        public ProfitSharingRules(Employee employee, double salariominimo)
        {
            _employee = employee;
            _salarioMinimo = salariominimo;
        }
        public ProfitSharingRules()
        {

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
            double SalarioBrutoemSalariosMinimos = _employee.SalarioBruto / _salarioMinimo;
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

        public Double ValorDaParticipacao()
        {

            Double resultadoFormula;



            resultadoFormula = _employee.SalarioBruto * this.PesoPorTempoDeAdmissao();
            resultadoFormula += _employee.SalarioBruto * this.PesoAreaAtuacao();
            resultadoFormula = (((resultadoFormula) / this.PesoFaixaSalarial()) * 12);
            decimal d = decimal.Round(Convert.ToDecimal(resultadoFormula), 2, MidpointRounding.AwayFromZero);
            return Convert.ToDouble(d);

        }

        public ProfitSharingResponse
                ProfitSharingReport(ProfitSharingRequest request,
                                    List<Employee> employees
                                    )
        {
            ProfitSharingResponse response = new ProfitSharingResponse();
            response.TotalDisponibilizado = request.ValorMaximoADistribuir;
            response.TotalDistribuido = 0;
            response.TotalFuncionarios = employees.Count();
            Double _salmin = request.SalarioMinimoAtual;
            List<Participacao> participacoes = new List<Participacao>();
            Double _totalDistribuido = 0;

            foreach (var c in employees)
            {
                Participacao participacao = new Participacao();
                ProfitSharingRules pfsr = new ProfitSharingRules(c, _salmin);
                participacao.Matricula = c.Matricula;
                participacao.Nome = c.Nome;
                participacao.ValorDaParticipacao = pfsr.ValorDaParticipacao();
                _totalDistribuido += participacao.ValorDaParticipacao;
                participacoes.Add(participacao);
            }
            response.Participacoes = participacoes;
            response.TotalDistribuido = _totalDistribuido;
            response.SaldoTotalDisponibilizado = request.ValorMaximoADistribuir - _totalDistribuido;
            return response;





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