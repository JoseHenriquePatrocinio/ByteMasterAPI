using static ByteMasterAPI.Enum.SituacaoEnum;

namespace ByteMasterAPI.Model
{
    public class Orcamento
    {
        public int Id { get; set; }
        public int IdCliente { get; set;}
        public int IdProduto { get; set; }
        public SituacaoOrcamento IdSituacao { get; set; }
    }
}
