using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Employees.API.Entities
{
    public class CargaInicialFuncionarios
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("matricula")]
        [JsonProperty("matricula")]
        public string Matricula { get; set; }

        [BsonElement("nome")]
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [BsonElement("area")]
        [JsonProperty("area")]
        public string Area { get; set; }

        [BsonElement("cargo")]
        [JsonProperty("cargo")]
        public string Cargo { get; set; }

        [BsonElement("salario_bruto")]
        [JsonProperty("salario_bruto")]
        public string SalarioBruto { get; set; }

        [BsonElement("data_de_admissao")]
        [JsonProperty("data_de_admissao")]
        public string DataAdmissao { get; set; }
    }
}
