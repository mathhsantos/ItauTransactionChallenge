using System.ComponentModel.DataAnnotations;

namespace ItauChallenge.Models {
    public class Transaction {

        [Range(0, double.MaxValue, ErrorMessage = "Necessario incluir um número positivo")]
        [Required(ErrorMessage = "Necessario incluir valor")]
        public double? Valor { get; set; }

        [Required(ErrorMessage = "Necessario informar a data")]
        public DateTime? DataHora { get; set; }
    }
}
