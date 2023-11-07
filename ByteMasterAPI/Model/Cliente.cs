using static ByteMasterAPI.Enum.SituacaoEnum;

namespace ByteMasterAPI.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }
        public SituacaoCliente Situacao { get; set; }
    }
}
