using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.PlanoDTO;
using WebApi.ViewModel.PlanoViewModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
    public class PlanoController : ControllerBase
    {
         private readonly IPlanoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PlanoController(
                IPlanoRepository PlanoRepository,
                IUnitOfWork unitOfWork
        )
        {
            this._repository = PlanoRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost()]

        public async Task<IActionResult> PostAsync([FromBody]CreatePlanoViewModel model)
        {
            var plano = new Plano()
            {
            
                Nome = model.Nome,
                Valor = model.Valor,
            };

            _repository.Create(plano);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Plano " + plano.Nome + " foi adicionado com sucesso!"
            });
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var planosList = await _repository.GetAllAsync();

            List<PlanoDto> planosDto = new List<PlanoDto>();

            foreach (Plano plano in planosList)
            {
                var planoDto = new PlanoDto()
                {
                    Id = plano.Id,
                    Nome = plano.Nome,
                    Valor = plano.Valor,
                   
                };

                planosDto.Add(planoDto);
            }

            return Ok(planosDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromBody]int id)
        {
           
           var planoDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if(planoDel == false)
                return NotFound();
            else
                return Ok(id); 
           
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var plano = await _repository.GetByIdAsync(id);

            if(plano == null)
                 return NotFound();
            else
            {
                var planoDto = new PlanoDto()
                {
                    Id = plano.Id,
                    Nome = plano.Nome,
                    Valor = plano.Valor,
                };
                return Ok(planoDto);
            }
        }

        [HttpPatch("{id}")] 
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdatePlanoViewModel model)
        {
            var plano = await _repository.GetByIdAsync(id);
            if (plano == null) return NotFound();

            plano.Nome = model.Nome;
            plano.Valor = model.Valor;

            _repository.Update(plano);
            await _unitOfWork.CommitAsync();
            return Ok(plano);
        }

    }
