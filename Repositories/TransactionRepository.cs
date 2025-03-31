using ItauChallenge.Interfaces;
using ItauChallenge.Models;

namespace ItauChallenge.Repositories {
    public class TransactionRepository : ITransactionRepository {

        private List<Transaction> _db = new List<Transaction>();

        public void AddTransaction(Transaction transaction) {
            _db.Add(transaction);
        }

        public IEnumerable<Transaction> GetTransaction() {

            return _db;
        }

        public void RemoveTransaction() {

            _db.RemoveRange(0, _db.Count);
        }
    }
}
