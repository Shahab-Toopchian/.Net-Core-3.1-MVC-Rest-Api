using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers {
    //api/[controller]
    [Route ("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase {

        //DI
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController (ICommanderRepo repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        // [HttpGet]
        // public ActionResult<IEnumerable<Command>> GetAllCommands () {
        //     var commandItem = _repository.GetAllCommands ();
        //     return Ok (commandItem);
        // }

        // [HttpGet ("{id}")]
        // public ActionResult<Command> GetCommandById (int id) {
        //     var commandItem = _repository.GetCommandById (id);
        //     if (commandItem != null)
        //     {
        //          return Ok(commandItem);
        //     }
        //     return NotFound();
        // }

        //Use Automapper / Data transfer object
        [HttpGet ("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById (int id) {
            var commandItem = _repository.GetCommandById (id);
            if (commandItem != null) {
                return Ok (_mapper.Map<CommandReadDto> (commandItem));
            }
            return NotFound ();
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands () {
            var commandItem = _repository.GetAllCommands ();
            return Ok (_mapper.Map<IEnumerable<CommandReadDto>> (commandItem));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand (CommandCreateDto commandCreaeteDto) {
            var commandModel = _mapper.Map<Command> (commandCreaeteDto);
            _repository.CreateCommand (commandModel);
            _repository.SaveChanges ();

            var commandReadDto = _mapper.Map<CommandReadDto> (commandModel);
            return CreatedAtRoute (nameof (GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
            //return Ok(commandReadDto);
        }

        [HttpPut ("{id}")]
        public ActionResult UpdateCommand (int id, CommandUpdateDto commandUpdateDto) {
            var commandFromRepo = _repository.GetCommandById (id);
            if (commandFromRepo == null) {
                return NotFound ();
            }
            _mapper.Map (commandUpdateDto, commandFromRepo);
            _repository.UpdateCommand (commandFromRepo);
            _repository.SaveChanges ();
            return NoContent ();
        }

        [HttpPatch ("{id}")]
        public ActionResult PartialCommandUpdate (int id, JsonPatchDocument<CommandUpdateDto> patchDoc) {
            var commandFromRepo = _repository.GetCommandById (id);
            if (commandFromRepo == null) {
                return NotFound ();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDto> (commandFromRepo);
            patchDoc.ApplyTo (commandToPatch, ModelState);
            if (!TryValidateModel (commandToPatch)) {
                return ValidationProblem (ModelState);
            }
            _mapper.Map (commandToPatch, commandFromRepo);
            _repository.UpdateCommand (commandFromRepo);
            _repository.SaveChanges ();
            return NoContent ();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id){
            var commandFromRepo = _repository.GetCommandById (id);
            if (commandFromRepo == null) {
                return NotFound ();
            }
            _repository.DeleteCommand(commandFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}