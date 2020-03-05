namespace GFT_ClubHouse__Management.Models.ViewModels.API {
    /**
     * Classe que será retornada nas requisições HTTP da API.
     */
    public class ResultViewModel<T> {
        public ResultViewModel(string message, T data) {
            Message = message;
            Data = data;
        }

        public string Message { get; set; }
        public T Data { get; set; }
    }
}