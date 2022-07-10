using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.InstrutorDTO;
using WebApi.ViewModel.InstrutorViewModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
    public class InstrutorController : ControllerBase
    {
        
        private readonly IInstrutorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public InstrutorController(
                IInstrutorRepository instrutorRepository,
                IUnitOfWork unitOfWork
        )
        {
            this._repository = instrutorRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost()]

        public async Task<IActionResult> PostAsync([FromBody]CreateInstrutorViewModel model)
        {
            var instrutor = new Instrutor()
            {
            
                Name = model.Name,
                Phone = model.Phone,
            };

            _repository.Create(instrutor);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Instrutor " + instrutor.Name + " foi adicionado com sucesso!"
            });
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var instrutorList = await _repository.GetAllAsync();

            List<InstrutorDto> instrutoresDto = new List<InstrutorDto>();

            foreach (Instrutor instrutor in instrutorList)
            {
                var instrutorDto = new InstrutorDto()
                {
                    Id = instrutor.Id,
                    Name = instrutor.Name,
                    Phone = instrutor.Phone,
                   
                };

                instrutoresDto.Add(instrutorDto);
            }

            return Ok(instrutoresDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromBody]int id)
        {
           
           var instrutorDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if(instrutorDel == false)
                return NotFound();
            else
                return Ok(id); 
           
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var instrutor = await _repository.GetByIdAsync(id);

            if(instrutor == null)
                 return NotFound();
            else
            {
                var instrutorDto = new InstrutorDto()
                {
                    Id = instrutor.Id,
                    Name = instrutor.Name,
                    Phone = instrutor.Phone,
                };
                return Ok(instrutorDto);
            }
        }

        [HttpPatch("{id}")] 
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateInstrutorViewModel model)
        {
            var instrutor = await _repository.GetByIdAsync(id);
            if (instrutor == null) 
            {
                return NotFound();
            }
            
            instrutor.Name = model.Name;
            instrutor.Phone = model.Phone;

            _repository.Update(instrutor);
            await _unitOfWork.CommitAsync();
            return Ok(instrutor);
        }
    }
