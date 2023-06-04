using Magic_API.Data;
using Magic_API.Models;
using Magic_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ExceptionServices;

namespace Magic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        //Crear un metodo para la version

        //IEnumerable es que retorna una lista
        [HttpGet]
        public ActionResult< IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("id", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]//Para tener documentado los estatus que utilizo.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVillaID(Int32 id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            if (villa==null)
            {
                return NotFound();
            }
            return Ok(villa);

        }



        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]//Para tener documentado los estatus que utilizo.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto)
        {
            try
            {
                if(!ModelState.IsValid)//Lo que se valido en el VillaDto
                {
                    return BadRequest(villaDto);
                }

                if(VillaStore.villaList.FirstOrDefault(v=>v.Nombre.ToLower()==villaDto.Nombre.ToLower())!=null)
                {

                    //Retorno un JSON con el campo Nombre Existe y el mensaje
                    ModelState.AddModelError("Nombre Existe", "La villa con ese nombre ya existe!!");
                    return BadRequest(ModelState);

                }
                if(villaDto == null)
                {
                    return BadRequest(villaDto);
                }

                if(villaDto.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
                VillaStore.villaList.Add(villaDto);
                return CreatedAtRoute("GetVilla",  new {id=villaDto.Id}, villaDto);
              
            }
            catch (Exception )
            {

                return BadRequest();
            }

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            if(id== 0)
            {
                return BadRequest();
            }

            var villa=VillaStore.villaList.FirstOrDefault(villa=>villa.Id==id);
            if(villa == null) { 
            return NotFound();
            }

            VillaStore.villaList.Remove(villa);
            return NoContent();

        }


        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if(villaDto==null || id!=villaDto.Id)
            {
                return BadRequest();
            }
            var villa=VillaStore.villaList.FirstOrDefault(v=> v.Id==id);
            villa.Nombre=villaDto.Nombre;
            villa.Ocupantes=villaDto.Ocupantes;
            villa.MetrosCuadrados = villaDto.MetrosCuadrados;
            return NoContent() ;    

        }



        //Para usar el HttpPatch debemos de agregar unos paquete
        /*
         * 
         *
         *
         [
  {
   
    "path": "/nombre",
    "op": "replace",
    "value": "Villa linda"
  }
]
         * */
        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePatchVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            //if (patchDto == null || id != 0)
            //{
            //    return BadRequest();
            //}
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            patchDto.ApplyTo(villa,ModelState);
            
            if(!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            return NoContent();

        }


    }
}
