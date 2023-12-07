namespace ByteMasterAPI.Enum
{
    public class SituacaoEnum
    {
        public enum SituacaoCliente
        {
            Ativo,
            Inativo
        }

        public enum SituacaoOrcamento
        {
            Pendente,
            Aprovado,
            Reprovado
        }
        public enum SituacaoOrdem
        {
            Ativo = 3,
            Completa = 4
        }
        public enum Role
        {
            Gerente = 5,
            Tecnico = 6,
            Atendente = 7 
        }
    }
}
