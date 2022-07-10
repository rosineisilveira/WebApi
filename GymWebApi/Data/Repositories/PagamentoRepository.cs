using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private DataContext _context;

        public PagamentoRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(Pagamento pagamento)
        {
             _context.DbSetPagamento.Add(pagamento);
        }

        public bool Delete(int pagamentoId)
        {
             var pagamento = _context.DbSetPagamento.FirstOrDefault(i => i.Id == pagamentoId);
            
            if(pagamento == null)
                return false;
            else
            {
                _context.DbSetPagamento.Remove(pagamento);
                return true;
            }
        }

        public async Task<IList<Pagamento>> GetAllAsync()
        {
            return await _context.DbSetPagamento.ToListAsync();
        }

        public  async Task<Pagamento> GetByIdAsync(int pagamentoId)
        {
             return await _context.DbSetPagamento
                .FirstOrDefaultAsync(x => x.Id == pagamentoId);
        }

        public void Update(Pagamento pagamento)
        {
            _context.Entry(pagamento).State = EntityState.Modified;
        }
    }
}