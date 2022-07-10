using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.PagamentoDTO;
using WebApi.ViewModel.PagamentoViewModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

    public class PagamentoController : ControllerBase
    {
         private readonly IPagamentoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PagamentoController(
                IPagamentoRepository pagamentoRepository,
                IUnitOfWork unitOfWork
        )
        {
            this._repository = pagamentoRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost()]

        public async Task<IActionResult> PostAsync([FromBody]CreatePagamentoViewModel model)
        {
            var pagamento = new Pagamento()
            {
            
                Tipo = model.Tipo,
                
            };

            _repository.Create(pagamento);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Pagamento " + pagamento.Tipo + " foi adicionado com sucesso!"
            });
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var pagamentoList = await _repository.GetAllAsync();

            List<PagamentoDto> pagamentosDto = new List<PagamentoDto>();

            foreach (Pagamento pagamento in pagamentoList)
            {
                var pagamentoDto = new PagamentoDto()
                {
                    Id = pagamento.Id,
                    Tipo = pagamento.Tipo,
                   
                   
                };

                pagamentosDto.Add(pagamentoDto);
            }

            return Ok(pagamentosDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromBody]int id)
        {
           
           var pagamentoDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if(pagamentoDel == false)
                return NotFound();
            else
                return Ok(id); 
           
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var pagamento = await _repository.GetByIdAsync(id);

            if(pagamento == null)
                 return NotFound();
            else
            {
                var pagamentoDto = new PagamentoDto()
                {
                    Id = pagamento.Id,
                    Tipo = pagamento.Tipo,
                    
                };
                return Ok(pagamentoDto);
            }
        }

        [HttpPatch("{id}")] 
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdatePagamentoViewModel model)
        {
            var pagamento = await _repository.GetByIdAsync(id);
            if (pagamento == null) return NotFound();

            pagamento.Tipo = model.Tipo;

            _repository.Update(pagamento);
            await _unitOfWork.CommitAsync();
            return Ok(pagamento);
        }

    }
