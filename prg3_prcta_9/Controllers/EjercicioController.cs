using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using prg3_prcta_9.Data;
using prg3_prcta_9.Models;
using prg3_prcta_9.Models.Dto;

namespace prg3_prcta_9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjercicioController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<EjercicioDto>> GetEPDto()
        {
            return Ok(EjercicioStore.EjercicioLista);
        }

        [HttpGet("Id", Name = "GetEP")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EjercicioDto> GetEjercicioDto(int Id)
        {
            var ep = EjercicioStore.EjercicioLista.FirstOrDefault(x => x.Id == Id);
            if (Id == 0)
            {
                return BadRequest();
            }
            if (ep == null)
            {
                return NotFound();
            }
            return Ok(ep);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EjercicioDto> CrearEjercicioDto([FromBody] EjercicioDto epdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (EjercicioStore.EjercicioLista.FirstOrDefault(v => v.Nombre.ToLower() == epdto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Nombre Existe", "Ese nombre ya existe");
                return BadRequest(ModelState);
            }
            if (epdto == null)
            {
                return BadRequest(epdto);
            }
            if (epdto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            epdto.Id = EjercicioStore.EjercicioLista.OrderByDescending(e => e.Id).FirstOrDefault().Id + 1;
            EjercicioStore.EjercicioLista.Add(epdto);
            return CreatedAtRoute("GetEP", new {id = epdto.Id}, epdto);
        }

        [HttpDelete("{id: int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteEP(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var ep = EjercicioStore.EjercicioLista.FirstOrDefault(v => v.Id == id);
            if(ep == null)
            {
                return NotFound();
            }
            EjercicioStore.EjercicioLista.Remove(ep);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateEP(int id, JsonPatchDocument<EjercicioDto> patchdto)
        {
            if(patchdto == null)
            {
                return BadRequest();
            }
            var ep = EjercicioStore.EjercicioLista.FirstOrDefault(v => v.Id == id);
            patchdto.ApplyTo(ep, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
