using static ByteMasterAPI.Enum.SituacaoEnum;

namespace ByteMasterAPI.Model
{
    public class OrdemServico
    {
        public int Id { get; set; }
        public int IdCliente_os { get; set; }
        public int IdProduto_os { get; set; }
        public SituacaoOrdem IdSituacao_os { get; set; }
    }

    public class OrdemServicoInfo
    {
        public string ClienteNome { get; set; }
        public string ProdutoModelo { get; set; }
        public string DescricaoProduto { get; set; }
        public string SituacaoDescricao { get; set; }
    }
}
