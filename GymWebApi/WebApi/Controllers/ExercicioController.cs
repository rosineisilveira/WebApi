using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.ExercicioDTO;
using WebApi.ViewModel.ExercicioViewModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

    public class ExercicioController: ControllerBase
    {
        private readonly IExercicioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ExercicioController(
                IExercicioRepository exercicioRepository,
                IUnitOfWork unitOfWork
        )
        {
            this._repository = exercicioRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost()]

        public async Task<IActionResult> PostAsync([FromBody]CreateExercicioViewModel model)
        {
            var exercicio = new Exercicio()
            {
            
                Nome = model.Nome,
                Repeticao = model.Repeticao,
                Series = model.Series,
            };

            _repository.Create(exercicio);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Exercicio " + exercicio.Nome + " foi adicionado com sucesso!"
            });
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var exerciciosList = await _repository.GetAllAsync();

            List<ExercicioDto> exerciciosDto = new List<ExercicioDto>();

            foreach (Exercicio exercicio in exerciciosList)
            {
                var exercicioDto = new ExercicioDto()
                {
                    Id = exercicio.Id,
                    Nome = exercicio.Nome,
                    Repeticao = exercicio.Repeticao,
                    Series = exercicio.Series,
                   
                };

                exerciciosDto.Add(exercicioDto);
            }

            return Ok(exerciciosDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromBody]int id)
        {
           
           var exercicioDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if(exercicioDel == false)
                return NotFound();
            else
                return Ok(id); 
           
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var exercicio = await _repository.GetByIdAsync(id);

            if(exercicio == null)
                 return NotFound();
            else
            {
                var exercicioDto = new ExercicioDto()
                {
                    Id = exercicio.Id,
                    Nome = exercicio.Nome,
                    Repeticao = exercicio.Repeticao,
                    Series = exercicio.Series,
                    
                };
                return Ok(exercicioDto);
            }
        }

        [HttpPatch("{id}")] 
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateExercicioViewModel model)
        {
            var exercicio = await _repository.GetByIdAsync(id);
            if (exercicio == null) return NotFound();

            exercicio.Nome = model.Nome;
            exercicio.Repeticao = model.Repeticao;
            exercicio.Series = model.Series;

            _repository.Update(exercicio);
            await _unitOfWork.CommitAsync();
            return Ok(exercicio);
        }
        
    }
