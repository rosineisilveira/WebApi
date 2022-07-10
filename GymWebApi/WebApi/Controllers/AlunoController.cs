using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.AlunoDTO;
using WebApi.ViewModel.AlunoViewModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AlunoController(
                IAlunoRepository alunoRepository,
                IUnitOfWork unitOfWork
        )
        {
            this._repository = alunoRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost()]

        public async Task<IActionResult> PostAsync([FromBody]CreateAlunoViewModel model)
        {
            var aluno = new Aluno()
            {
            
                Name = model.Name,
                Phone = model.Phone,
            };

            _repository.Create(aluno);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Aluno " + aluno.Name + " foi adicionado com sucesso!"
            });
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var alunosList = await _repository.GetAllAsync();

            List<AlunoDto> alunosDto = new List<AlunoDto>();

            foreach (Aluno aluno in alunosList)
            {
                var alunoDto = new AlunoDto()
                {
                    Id = aluno.Id,
                    Name = aluno.Name,
                    Phone = aluno.Phone,
                   
                };

                alunosDto.Add(alunoDto);
            }

            return Ok(alunosDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromBody]int id)
        {
           
           var alunoDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if(alunoDel == false)
                return NotFound();
            else
                return Ok(id); 
           
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var aluno = await _repository.GetByIdAsync(id);

            if(aluno == null)
                 return NotFound();
            else
            {
                var alunoDto = new AlunoDto()
                {
                    Id = aluno.Id,
                    Name = aluno.Name,
                    Phone = aluno.Phone,
                };
                return Ok(alunoDto);
            }
        }

        [HttpPatch("{id}")] 
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateAlunoViewModel model)
        {
            var aluno = await _repository.GetByIdAsync(id);
            if (aluno == null) return NotFound();

            aluno.Name = model.Name;
            aluno.Phone = model.Phone;

            _repository.Update(aluno);
            await _unitOfWork.CommitAsync();
            return Ok(aluno);
        }

        
    }
