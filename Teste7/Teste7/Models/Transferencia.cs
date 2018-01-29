using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Teste7.Models
{
    public class Transferencia
    {
        public int ID { get; set; }
        public int ClientePagadorID { get; set; }
        public int ClienteRecebedorID { get; set; }
        public decimal Valor { get; set; }

        [ForeignKey("ClientePagadorID")]
        public virtual Cliente ClientePagador { get; set; }

        [ForeignKey("ClienteRecebedorID")]
        public virtual Cliente ClienteRecebedor { get; set; }

    }
}