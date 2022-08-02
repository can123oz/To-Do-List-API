using Entity.Context;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.Services.Abstract;

namespace ToDoList.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Tasks()
        {
            var myTasks = await _toDoService.GetAll();
            if (myTasks != null)
            {
                return Ok(myTasks);
            }
            return BadRequest();
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
        public IActionResult Findundone()
        {
            var undoneTasks = _toDoService.FindUndone();
            if(undoneTasks != null)
            {
                return Ok(undoneTasks);
            }
            return BadRequest();
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

        [HttpGet("CurrentUser")]
        public IActionResult CurrentUser()
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                return Ok($"Hello {user.UserName} you are an admin.");
            }
            return BadRequest();
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new User
                {
                    UserName = userClaims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value,
                };
            }
            return null;
        }
    }
}
