using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.MatriculaDTO;
using WebApi.ViewModel.MatriculaViewModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

    public class MatriculaController : ControllerBase
    {
        private readonly IMatriculaRepository _repository;
        private readonly ITreinoRepository _treinoRepository;

        private readonly IUnitOfWork _unitOfWork;

        public MatriculaController(
                IMatriculaRepository MatriculaRepository,
                ITreinoRepository TreinoRepository,
                IUnitOfWork unitOfWork
        )
        {
            this._repository = MatriculaRepository;
            this._unitOfWork = unitOfWork;
            this._treinoRepository = TreinoRepository;
        }

        [HttpPost()]

        public async Task<IActionResult> PostAsync([FromBody]CreateMatriculaViewModel model)
        {
            var matricula = new Matricula()
            {
            
               Status = model.Status,
               AlunoId = model.AlunoId,
               PlanoId= model.PlanoId,
               PagamentoId = model.PagamentoId,
               DataCadastro = model.DataCadastro,

            };

            _repository.Create(matricula);
            await _unitOfWork.CommitAsync();

            return Ok(matricula);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var matriculasList = await _repository.GetAllAsync();

            List<MatriculaDto> matriculasDto = new List<MatriculaDto>();

            foreach (Matricula matricula in matriculasList)
            {
                var matriculaDto = new MatriculaDto()
                {
                    Id = matricula.Id,
                    Status = matricula.Status,
                    Aluno = matricula.Aluno,
                    Plano = matricula.Plano,
                    Treinos = matricula.Treinos, 
                    Pagamento = matricula.Pagamento,  
                };

                matriculasDto.Add(matriculaDto);
            }

            return Ok(matriculasDto);
            
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromBody]int id)
        {
           
           var matriculaDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if(matriculaDel == false)
                return Ok(new
            {
                message = "Matricula nao existe ou está ativa!!"
            });
            else
                return Ok(id); 
           
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var matricula = await _repository.GetByIdAsync(id);

            if(matricula == null)
                 return NotFound();
            else
            {
                var matriculaDto = new MatriculaDto()
                {
                    Id = matricula.Id,
                    Status = matricula.Status,
                    Aluno = matricula.Aluno,
                    Plano = matricula.Plano,
                    Treinos = matricula.Treinos,
                    Pagamento = matricula.Pagamento
                };
                return Ok(matriculaDto);
            }
        }

        [HttpPatch("{id}")] 
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateMatriculaViewModel model)
        {
            var matricula = await _repository.GetByIdAsync(id);
            if (matricula == null) return NotFound();
                matricula.Status = model.Status;
                matricula.PlanoId = model.PlanoId;
                matricula.PagamentoId = model.PagamentoId;

            _repository.Update(matricula);
            await _unitOfWork.CommitAsync();
            return Ok(matricula);
        }

        //relacionamentos

        [HttpPatch("/TreinoToMatricula")] 
        public async Task<IActionResult> TreinoToMatriculaAsync([FromBody]InsertTreinoMatriculaViewModel model)
        {
            var matricula = await _repository.GetByIdAsync(model.MatriculaId);

            if(matricula == null)
            {
                return NotFound();
            }
            var treinosSearch = new List<Treino>();
            foreach(var treino in model.Treinos)
            {
                var treinoSearch = await _treinoRepository.GetByIdAsync(treino.TreinoId);
                treinosSearch.Add(treinoSearch);
            }
            if (treinosSearch.Count == 0)
            {
                 return Ok(new
            {
                message = "Treino nao encontrado!!"
            });
            }
            matricula.Treinos = treinosSearch;
            _repository.Update(matricula);
            await _unitOfWork.CommitAsync();
            return Ok();

        }

        [HttpDelete("/RemoveTreinoMatricula")]
        public async Task<IActionResult> DeleteTreino([FromBody]RemoveTreinoMatriculaViewModel  model)
        {
            Treino treino = await _treinoRepository.GetByIdAsync(model.TreinoId);
            Matricula matricula = await _repository.GetByIdAsync(model.MatriculaId);

            if (treino == null)
            {
                return NotFound();
            }

            if (matricula == null)
            {
                return NotFound();
            }

            matricula.Treinos.Remove(treino);
            _repository.Update(matricula);
            await _unitOfWork.CommitAsync();

            return Ok(treino);
        }

}
