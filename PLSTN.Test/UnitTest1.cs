using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Employees.API.Data;
using Employees.API.Entities;
using Employees.API.Repository;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using Xunit;

namespace PLSTN.Test
{
    public class UnitTest1
    {
        
        [Fact]
        public void TestarSharingProfitReport()
        {
            //Arrange
            //cultureinfo.currentculture = new CultureInfo("en-gb");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            List<Employee> employees = (List<Employee>)GetMyEmployees();

            string fileName = "responseProfitSharing.json";
            string jsonString = File.ReadAllText(fileName);
            ProfitSharingResponse e = JsonConvert.DeserializeObject<ProfitSharingResponse>(jsonString);// JsonConverter<CargaInicial>(jsonString);// JsonSerializer.Deserialize<Object>(jsonString);
            ProfitSharingRequest request = new ProfitSharingRequest(){ 
                SalarioMinimoAtual= 900,
                ValorMaximoADistribuir= 10000000,
            };
            ProfitSharingRules psr = new ProfitSharingRules();
            string expected = JsonConvert.SerializeObject(e);
            //Act
            ProfitSharingResponse a = psr.ProfitSharingReport(request, employees);
            string actual = JsonConvert.SerializeObject(a);
            //Assert
            Assert.Equal(expected,actual);
            

        }


        public static IEnumerable<Employee> GetMyEmployees()
        {
            string fileName = "banco2.json";
            string jsonString = File.ReadAllText(fileName);
            CargaInicial cargaInicial = JsonConvert.DeserializeObject<CargaInicial>(jsonString);// JsonConverter<CargaInicial>(jsonString);// JsonSerializer.Deserialize<Object>(jsonString);
            List<Employee> employees = new List<Employee>();

            foreach (var f in cargaInicial.Funcionarios)
            {
                Employee emp = new Employee();
                emp.Matricula = f.Matricula;
                emp.Nome = f.Nome;
                emp.Cargo = f.Cargo;
                emp.Area = f.Area;
                emp.DataAdmissao = Convert.ToDateTime(f.DataAdmissao.Split("-")[2]
                    + "/" + f.DataAdmissao.Split("-")[1]
                    + "/" + f.DataAdmissao.Split("-")[0]);
                emp.SalarioBruto = Convert.ToDouble(f.SalarioBruto);
                employees.Add(emp);

            }
            return employees;

        }
    }
}
