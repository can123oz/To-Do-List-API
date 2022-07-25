using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Models;

namespace ToDoList.Services.Abstract
{
    public interface IToDoService
    {
        Task<List<MyTask>> GetAll();
        MyTask GetTask(int id);
        bool DeleteTask(int id);
        MyTask UpdateTask(MyTask task);
        void AddTask(MyTask task);
        List<MyTask> FindUndone();
        MyTask ChangeToTrue(int id);
    }
}
