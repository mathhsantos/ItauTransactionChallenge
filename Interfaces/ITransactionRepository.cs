using ItauChallenge.Models;

namespace ItauChallenge.Interfaces {
    public interface ITransactionRepository {
        public void AddTransaction(Transaction transaction);
        public IEnumerable<Transaction> GetTransaction();
        public void RemoveTransaction();
    }
}
