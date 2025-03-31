namespace ItauChallenge.ViewModels {
    public class ResponseViewModel<T> {

        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public ResponseViewModel(T data, List<string> error ) {
            Data = data;
            Errors = error;
        }

        public ResponseViewModel(T data) {
            Data = data;
        }

        public ResponseViewModel(string error) {
            Errors.Add(error);
        }

        public ResponseViewModel(List<string> errors) {
            Errors = errors;
        }
    }
}
