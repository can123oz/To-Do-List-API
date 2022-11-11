
This is a basic To Do list App that you can track your tasks and secure them with JWT token.
For this Rest Api my orm choice is Entityframework Ms-sql.

End Points for Login;
Login(User user) : checks your membership and returns you a JWT token to access your tasks.

End Points for Tasks;

Tasks() : Get every task in the database.

Tasks([FromRoute]int id) : Get a single task with id number.

Tasks([FromBody]MyTask myTask) : Posting a new task to the sql.

Update([FromBody]MyTask myTask, [FromRoute]int id) : finding the task and updating it.

Delete([FromRoute]int id) : Deleting a task returning a boolean value.

Findundone() : Returnig every undone task to the client.

FinishedTask([FromRoute]int id) : Changing the state of a single task at database.

    
