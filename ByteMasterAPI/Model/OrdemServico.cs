using static ByteMasterAPI.Enum.SituacaoEnum;

namespace ByteMasterAPI.Model
{
    public class OrdemServico
    {
        public int Id { get; set; }
        public int IdCliente_os { get; set; }
        public int IdProduto_os { get; set; }
    }
}
