namespace PeDeMeia.Domain.DTOs.InputModel
{
    public class CartaoInputModel
    {
        public decimal Limite { get; set; }
        public string Nome { get; set; }
        public DateTime DataVencimentoFatura { get; set; }
        public decimal ValorFatura { get; set; }
        public int PessoaId { get; set; }
    }
}
