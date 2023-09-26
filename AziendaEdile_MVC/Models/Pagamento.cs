using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AziendaEdile_MVC.Models
{
    public class Pagamento
    {
        public int IdPagamento { get; set; }
        public int IDDipendente { get; set; }

        public string PeriodoPagamento { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Ammontare { get; set; }
        public string Tipo { get; set; }
    }
}