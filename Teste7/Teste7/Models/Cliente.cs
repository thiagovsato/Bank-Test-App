using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teste7.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeCompleto { get; set; }
        public decimal Saldo { get; set; }

    }
}