using static ByteMasterAPI.Enum.SituacaoEnum;

namespace ByteMasterAPI.Model
{
    public class Orcamento
    {
        public int Id { get; set; }
        public string IdCliente { get; set; }
        public int IdProduto { get; set; }
        public SituacaoOrcamento IdSituacao { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
    }

    public class OrcamentoInfo
    {
        public int Id { get; set; }
        public string ClienteNome { get; set; }
        public string ProdutoModelo { get; set; }
        public double ProdutoValorUnitario { get; set; }
        public string SituacaoDescricao { get; set; }
        public DateTime Data { get; set; }
    }

}
