using System.ComponentModel.DataAnnotations;

namespace ItauChallenge.Models {
    public class Transaction {

        [Required(ErrorMessage = " Necessario informar o Valor")]
        [Range(0, double.MaxValue, ErrorMessage = "Necessario incluir um número positivo")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Necessario informar a data")]
        public DateTime DataHora { get; set; }
    }
}
