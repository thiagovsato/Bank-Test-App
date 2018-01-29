using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Teste7.Models
{
    public class Deposito
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public decimal Valor { get; set; }

        [ForeignKey ("ClienteID")]
        public virtual Cliente cliente { get; set; }
    }
}