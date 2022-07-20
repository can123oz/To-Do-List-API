using Entity.Context;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.Services.Abstract;

namespace ToDoList.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private IToDoService _toDoService;
        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService; 
        }

        [HttpGet("Tasks")]
        public List<MyTask> Tasks()
        {
            var myTasks = _toDoService.GetAll();
            return myTasks;
        }

        [HttpGet("Tasks/{id}")]
        public IActionResult Tasks([FromRoute]int id)
        {
            var findedTask = _toDoService.GetTask(id);
            if(findedTask != null)
            {
                return Ok(findedTask);
            }
            return BadRequest();
        }

        [HttpPost("Tasks")]
        public IActionResult Tasks([FromBody]MyTask myTask)
        {
            myTask.Status = false;
            _toDoService.AddTask(myTask);
            return Ok(myTask);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromBody]MyTask myTask, [FromRoute]int id)
        {
            myTask.Id = id;
            var updatedTask = _toDoService.UpdateTask(myTask);
            if(updatedTask != null)
            {
                return Ok(updatedTask);
            }
            return BadRequest();
        }

        [HttpDelete("Delete/{id}")]
        public bool Delete([FromRoute]int id)
        {    
            return _toDoService.DeleteTask(id);
        }

        [HttpGet("Findundone")]
        public List<MyTask> Findundone()
        {
            var undoneTasks = _toDoService.FindUndone();
            return undoneTasks;
        }

        [HttpGet("Finishedtask/{id}")]
        public IActionResult FinishedTask([FromRoute]int id)
        {
            var changedTask = _toDoService.ChangeToTrue(id);
            if(changedTask != null)
            {
                return Ok(changedTask);
            }
            return BadRequest();
        }
    }
}
