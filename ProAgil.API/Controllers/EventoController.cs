using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepositorio _repo;

        public EventoController(IProAgilRepositorio _repo)
        {
            this._repo = _repo;

        }

         [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                var result = await _repo.GetALLEventoAsincBy(true); 
                
                return Ok(result);
                
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no banco de dados");
            }
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try{
                var result = await _repo.GetALLEventoAsincById(EventoId,true); 
                
                return Ok(result);
                
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no banco de dados");
            }
        }

        [HttpGet("getBYTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try{
                var result = await _repo.GetALLEventoAsincByTema(tema,true); 
                
                return Ok(result);
                
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no banco de dados");
            }
        }

       [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
                try{
                
                _repo.Add(model); 
                if(await _repo.SaveChngesAsync()){
                    return Created($"/api/evento/{model.Id}",model);
                }
                
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no banco de dados");
            }

            return BadRequest();
        }

       [HttpPut]
        public async Task<IActionResult> Put(int EventoId,Evento model)
        {
            try{
                
                var evento = await _repo.GetALLEventoAsincById(EventoId,false);

                if(evento == null) return NotFound();

                _repo.Update(model); 
                if(await _repo.SaveChngesAsync()){
                    return Created($"/api/evento/{model.Id}",model);
                }
                
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no banco de dados");
            }

            return BadRequest();
        }
       [HttpDelete]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try{
                
                var evento = await _repo.GetALLEventoAsincById(EventoId,false);

                if(evento == null) return NotFound();

                _repo.Delete(evento); 
                if(await _repo.SaveChngesAsync()){
                    return Ok();
                }
                
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha no banco de dados");
            }

            return BadRequest();
        }
    }
}