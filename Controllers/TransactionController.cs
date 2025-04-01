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

                if (!ModelState.IsValid) {
                    var erros = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return StatusCode(400, new ResponseViewModel<string>(erros));
                }

                if (transaction.DataHora > DateTime.UtcNow) {
                    return StatusCode(400, new ResponseViewModel<string>("Não pode realizar transferencia com data no futuro!"));
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
                    return BadRequest(new ResponseViewModel<GetTransactionDto>( new GetTransactionDto () {
                        Avg = 0,
                        Count = 0,
                        Max = 0,
                        Min = 0,
                        Sum = 0
                    }));
                }

                return Ok(new ResponseViewModel<GetTransactionDto>(new GetTransactionDto() {
                    Avg = Math.Round(transactions.Average(x => x.Valor) ?? 0, 2),
                    Count = transactions.Count,
                    Max = Math.Round(transactions.Max(x => x.Valor) ?? 0, 2),
                    Min = Math.Round(transactions.Min(x => x.Valor) ?? 0, 2),
                    Sum = Math.Round(transactions.Sum(x => x.Valor) ?? 0, 2)
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
