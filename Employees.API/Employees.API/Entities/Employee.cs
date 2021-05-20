using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Employees.API.Entities
{
    public class Employee
    {
        public Employee()
        {
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("matricula")]
        public string Matricula { get; set; }

        [BsonElement("nome")]
        public string Nome { get; set; }

        [BsonElement("area")]
        public string Area { get; set; }

        [BsonElement("cargo")]
        public string Cargo { get; set; }

        [BsonElement("salario_bruto")]
        public decimal SalarioBruto { get; set; }

        [BsonElement("data_de_admissao")]
        public DateTime DataAdmissao { get; set; }

    }
}
