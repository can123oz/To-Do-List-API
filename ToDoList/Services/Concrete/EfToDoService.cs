using Entity.Context;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Services.Abstract;

namespace ToDoList.Services.Concrete
{
    public class EfToDoService : IToDoService
    {
        private readonly DatabaseContext _db;
        public EfToDoService(DatabaseContext db)
        {
            _db = db;
        }

        public void AddTask(MyTask myTask)
        {
            _db.Add(myTask);
            _db.SaveChanges();
        }

        public MyTask ChangeToTrue(int id)
        {
            var selectedTask = _db.MyTasks.Find(id);
            if(selectedTask != null)
            {
                selectedTask.Status = true;
                _db.SaveChanges();
                return selectedTask;
            }
            return null;
        }

        public bool DeleteTask(int id)
        {
            var deletedTask = _db.MyTasks.FirstOrDefault(p => p.Id == id);
            if (deletedTask != null)
            {
                _db.Remove(deletedTask);
                _db.SaveChanges();
            }
            return false;
        }

        public List<MyTask> FindUndone()
        {
            var undoneTasks = _db.MyTasks.Where(p => p.Status == false).ToList();
            return undoneTasks;
        }

        public List<MyTask> GetAll()
        {
            var myTasks = _db.MyTasks.ToList();
            return myTasks;
        }

        public MyTask GetTask(int id)
        {
            var myTask = _db.MyTasks.FirstOrDefault(p => p.Id == id);
            if (myTask != null)
            {
                return myTask;
            }
            return null;
        }

        public MyTask UpdateTask(MyTask myTask)
        {
            var updatedTask = _db.MyTasks.FirstOrDefault(p => p.Id == myTask.Id);
            if (updatedTask != null)
            {
                updatedTask.Status = myTask.Status;
                updatedTask.Description = myTask.Description;
                updatedTask.Title = myTask.Title;
                _db.SaveChanges();
                return updatedTask;
            }
            return null;
        }
    }
}