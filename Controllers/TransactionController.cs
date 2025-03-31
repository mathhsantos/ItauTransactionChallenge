using ItauChallenge.Dtos;
using ItauChallenge.Interfaces;
using ItauChallenge.Models;
using ItauChallenge.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItauChallenge.Controllers {

    [ApiController]
    public class TransactionController : ControllerBase {

        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository) {
            _transactionRepository = transactionRepository;
        }

        [HttpPost("transacao")]
        public IActionResult PostTransaction([FromBody] Transaction transaction) {

            try {

                if (transaction.DataHora > DateTime.UtcNow) {
                    return StatusCode(400, new ResponseViewModel<string>("Não pode realizar transferencia com data no futuro!"));
                }

                if (!ModelState.IsValid) {
                    var erros = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return StatusCode(400, new ResponseViewModel<string>(erros));
                }

                _transactionRepository.AddTransaction(transaction);

                return Created("", "");

            } catch {

                return BadRequest("Erro no servidor, tente novamente mais tarde");
            }
        }

        [HttpGet("estatistica")]
        public IActionResult GetEstatisticas() {

            try {

                var validTime = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(60));

                var transactions = _transactionRepository.GetTransaction().Where(x => x.DataHora >= validTime).ToList();

                if (transactions.Count == 0) {
                    return BadRequest(new ResponseViewModel<string>("Não existe transações nos ultimos 60 segundos"));
                }

                return Ok(new ResponseViewModel<GetTransactionDto>(new GetTransactionDto() {
                    Avg = transactions.Average(x => x.Valor),
                    Count = transactions.Count,
                    Max = transactions.Max(x => x.Valor),
                    Min = transactions.Min(x => x.Valor),
                    Sum = transactions.Sum(x => x.Valor)
                }));

            } catch {

                return BadRequest("Erro no servidor, tente novamente mais tarde");
            }
        }

        [HttpDelete("transacao")]
        public IActionResult DeleteTransactions() {

            try {

                _transactionRepository.RemoveTransaction();

                return Ok(new ResponseViewModel<string>("Todas as informações foram apagadas com sucesso", null));

            } catch {

                return BadRequest("Erro no servidor, tente novamente mais tarde");
            }
        }
    }
}
