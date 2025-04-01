namespace ItauChallenge.Dtos {
    public class GetTransactionDto {
        public int Count { get; set; }
        public double? Sum { get; set; }
        public double? Avg { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
    }
}
