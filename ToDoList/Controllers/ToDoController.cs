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

        [HttpGet]
        public List<MyTask> Get()
        {
            var myTasks = _toDoService.GetAll();
            return myTasks;
        }

        [HttpGet("Getsingle/{id}")]
        public MyTask GetSingle(int id)
        {
            var findedTask = _toDoService.GetTask(id);
            return findedTask;
        }

        [HttpPost("Add")]
        public IActionResult Add(MyTask myTask)
        {
            myTask.Status = false;
            _toDoService.AddTask(myTask);
            return Ok(myTask);
        }

        [HttpPut("Update")]
        public IActionResult Update(MyTask myTask)
        {
            var updatedTask = _toDoService.UpdateTask(myTask);
            return Ok(updatedTask);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _toDoService.DeleteTask(id);
            return Ok();
        }

        [HttpGet("Findundone")]
        public List<MyTask> Findundone()
        {
            var undoneTasks = _toDoService.FindUndone();
            return undoneTasks;
        }

        [HttpGet("Finishedtask/{id}")]
        public IActionResult FinishedTask(int id)
        {
            var changedTask = _toDoService.ChangeToTrue(id);
            return Ok(changedTask);
        }
    }
}
