using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPi.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }

        public string CardholderName{ get; set; }

        public int CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }

        public int CVC { get; set; }
    }
}
