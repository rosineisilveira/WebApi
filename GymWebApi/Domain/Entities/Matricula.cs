namespace Domain.Entities
{
    public class Matricula
    {
        public int Id { get; set; }
        public DateTime  DataCadastro { get; set; }
        public bool Status { get; set; }

        public Aluno Aluno { get; set; }
        public int AlunoId { get; set; }

        public Plano Plano { get; set; }
        public int PlanoId { get; set; }
        public Pagamento Pagamento { get; set; }
        public int PagamentoId { get; set; }
        public IList<Treino> Treinos { get; set; }

    }
}