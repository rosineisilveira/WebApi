using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.TreinoDTO;
using WebApi.ViewModel.TreinoViewModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
    public class TreinoController : ControllerBase
    {
        private readonly ITreinoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExercicioRepository _exercicioRepository;
        private readonly IMatriculaRepository _matriculaRepository;

        public TreinoController(
                ITreinoRepository repository,
                IExercicioRepository exercicioRepository,
                IMatriculaRepository matriculaRepository,
                IUnitOfWork unitOfWork
        )
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._exercicioRepository = exercicioRepository;
            this._matriculaRepository = matriculaRepository;
        }

        [HttpPost()]

        public async Task<IActionResult> PostAsync([FromBody]CreateTreinoViewModel model)
        {
            var treino = new Treino()
            {
            
                Nome = model.Nome,
                InstrutorId = model.InstrutorId,

            };

            _repository.Create(treino);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Treino " + treino.Nome + " foi adicionado com sucesso!"
            });
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var treinosList = await _repository.GetAllAsync();

            List<TreinoDto> treinosDto = new List<TreinoDto>();

            foreach (Treino treino in treinosList)
            {
                var treinoDto = new TreinoDto()
                {
                    Id = treino.Id,
                    Nome = treino.Nome,
                    Instrutor = treino.Instrutor,
                    Exercicios = treino.Exercicios,   
                };

                treinosDto.Add(treinoDto);
            }

            return Ok(treinosDto);
            
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id,[FromBody]Treino treino)
        {
           
           var treinoDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if(treinoDel == false)
                return NotFound();
            else
                return Ok(id); 
           
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var treino = await _repository.GetByIdAsync(id);

            if(treino == null)
                 return NotFound();
            else
            {
                var treinoDto = new TreinoDto()
                {
                    Id = treino.Id,
                    Nome = treino.Nome,
                    Instrutor = treino.Instrutor,
                    Exercicios = treino.Exercicios,
                };
                return Ok(treinoDto);
            }
        }

        [HttpPatch("{id}")] 
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateTreinoViewModel model)
        {
            var treino = await _repository.GetByIdAsync(id);
            if (treino == null) return NotFound();

            treino.Nome = model.Nome;
            treino.InstrutorId = model.InstrutorId;
    
            _repository.Update(treino);
            await _unitOfWork.CommitAsync();
            return Ok(treino);
        }

        //relacionamentos

        [HttpPatch("/ExercicioToTreino")]

         public async Task<IActionResult> ExercicioToTreinoAsync([FromBody]ExercicioTreinoViewModel model)
         {

            var treino = await _repository.GetByIdAsync(model.TreinoId);

            if(treino == null)
            {
                return NotFound();
            }
            var exerciciosSearch = new List<Exercicio>();
            foreach(var exercicio in model.Exercicios)
            {
                var exercicioSearch = await _exercicioRepository.GetByIdAsync(exercicio.ExercicioId);
                exerciciosSearch.Add(exercicioSearch);
            }
            if (exerciciosSearch.Count == 0)
            {
                 return Ok(new
            {
                message = "Exercicio nao encontrado!!"
            });
            }
            treino.Exercicios = exerciciosSearch;
            _repository.Update(treino);
            await _unitOfWork.CommitAsync();
            return Ok();
           

         }
       

        [HttpDelete("/RemoveExercicioTreino")]
        public async Task<IActionResult> DeleteExercicio([FromBody]RemoveExercicioTreinoViewModel  model)
        {
            Treino treino = await _repository.GetByIdAsync(model.TreinoId);
            Exercicio exercicio = await _exercicioRepository.GetByIdAsync(model.ExercicioId);

            if (treino == null)
            {
                return NotFound();
            }

            if (exercicio == null)
            {
                return NotFound();
            }

            treino.Exercicios.Remove(exercicio);
            _repository.Update(treino);
            await _unitOfWork.CommitAsync();

            return Ok(treino);
        }

       
    }
