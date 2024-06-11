using Microsoft.EntityFrameworkCore;
using Notebook.Models;


namespace Notebook
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            Peseg22Context db = new Peseg22Context();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Войти");
                Console.WriteLine("2. Зарегистрироваться");
                Console.WriteLine("3. Выйти из программы");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.WriteLine("Неверный ввод, введите число от 1 до 3");
                }

                switch (choice)
                {
                    case 1:
                        Login(db);
                        break;
                    case 2:
                        Register(db);
                        break;
                    case 3:
                        exit = true;
                        break;
                }
            }
        }

        private static void Login(Peseg22Context db)
        {
            Console.WriteLine("Введите имя пользователя:");
            string username = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            var user = db.Users.Include(u => u.Tasks)
                .FirstOrDefault(u => u.Name == username && u.Password == password);

            if (user != null)
            {
                Console.WriteLine($"Добро пожаловать, {username}!");
                UserMenu(user, db);
            }
            else
            {
                Console.WriteLine("Неверное имя пользователя или пароль");
            }
        }

        private static void Register(Peseg22Context db)
        {
            Console.WriteLine("Введите имя пользователя:");
            string newUsername = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string newPassword = Console.ReadLine();

            var newUser = new User { Name = newUsername, Password = newPassword };
            db.Users.Add(newUser);
            db.SaveChanges();

            Console.WriteLine("Пользователь успешно зарегистрирован");
        }


        private static void UserMenu(User user, Peseg22Context db)
        {
            bool userExit = false;
            while (!userExit)
            {
                Console.WriteLine("(O_O)  (^-^)  (o_o)  (^о^)  (>_<)  (^_^)  (*_*)");
                Console.WriteLine("/| |)  /| |)  /| |)  /| |)  /| |)  /| |)  /| |)");
                Console.WriteLine(" / |    / |    / |    / |    / |    / |    / | ");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Удалить задачу");
                Console.WriteLine("3. Редактировать задачу");
                Console.WriteLine("4. Просмотреть все задачи");
                Console.WriteLine("5. Просмотреть задачи на сегодня");
                Console.WriteLine("6. Просмотреть задачи на завтра");
                Console.WriteLine("7. Просмотреть задачи на эту неделю");
                Console.WriteLine("8. Выйти из аккаунта");
                Console.WriteLine("(O_O)  (^-^)  (o_o)  (^о^)  (>_<)  (^_^)  (*_*)");
                Console.WriteLine("/| |)  /| |)  /| |)  /| |)  /| |)  /| |)  /| |)");
                Console.WriteLine(" / |    / |    / |    / |    / |    / |    / | ");
                Console.WriteLine();

                int userChoice;
                while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 1 || userChoice > 8)
                {
                    Console.WriteLine("Неверный ввод, введите число от 1 до 8");
                }

                switch (userChoice)
                {
                    case 1:
                        AddTask(user, db);
                        break;
                    case 2:
                        DeleteTask(user, db);
                        break;
                    case 3:
                        EditTask(user, db);
                        break;
                    case 4:
                        ViewAllTasks(user);
                        break;
                    case 5:
                        ViewTasksForToday(user);
                        break;
                    case 6:
                        ViewTasksForTomorrow(user);
                        break;
                    case 7:
                        ViewTasksForThisWeek(user);
                        break;
                    case 8:
                        userExit = true;
                        break;
                }

                static void AddTask(User user, Peseg22Context db)
                {
                    Console.WriteLine("Введите название задачи:");
                    string title = Console.ReadLine();
                    Console.WriteLine("Введите описание задачи:");
                    string description = Console.ReadLine();
                    Console.WriteLine("Введите дату выполнения задачи в формате ГГГГ-ММ-ДД:");
                    DateTime dueDate;
                    while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
                    {
                        Console.WriteLine("Неверный формат даты, введите дату в формате ГГГГ-ММ-ДД:");
                    }

                    var newTask = new Models.Task
                    {
                        Title = title,
                        Description = description,
                        Duedate = dueDate,
                        Userid = user.Userid
                    };
                    db.Tasks.Add(newTask);
                    db.SaveChanges();
                    Console.WriteLine("Задача успешно добавлена");
                }

                static void DeleteTask(User user, Peseg22Context db)
                {
                    Console.WriteLine("Введите ID задачи, которую хотите удалить:");
                    int taskIdToDelete;
                    while (!int.TryParse(Console.ReadLine(), out taskIdToDelete))
                    {
                        Console.WriteLine("Неверный ID задачи, введите целое число:");
                    }

                    var taskToDelete =
                        db.Tasks.FirstOrDefault(t => t.Taskid == taskIdToDelete && t.Userid == user.Userid);
                    if (taskToDelete != null)
                    {
                        db.Tasks.Remove(taskToDelete);
                        db.SaveChanges();
                        Console.WriteLine("Задача успешно удалена");
                    }
                    else
                    {
                        Console.WriteLine("Задача не найдена");
                    }
                }

                static void EditTask(User user, Peseg22Context db)
                {
                    Console.WriteLine("Введите ID задачи, которую хотите отредактировать:");
                    int taskIdToEdit;
                    while (!int.TryParse(Console.ReadLine(), out taskIdToEdit))
                    {
                        Console.WriteLine("Неверный ID задачи, введите целое число:");
                    }

                    var taskToEdit =
                        db.Tasks.FirstOrDefault(t => t.Taskid == taskIdToEdit && t.Userid == user.Userid);
                    if (taskToEdit != null)
                    {
                        Console.WriteLine("Введите новое название задачи:");
                        taskToEdit.Title = Console.ReadLine();
                        Console.WriteLine("Введите новое описание задачи:");
                        taskToEdit.Description = Console.ReadLine();
                        Console.WriteLine("Введите новую дату выполнения задачи в формате ГГГГ-ММ-ДД:");
                        DateTime newDueDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out newDueDate))
                        {
                            Console.WriteLine("Неверный формат даты, введите дату в формате ГГГГ-ММ-ДД:");
                        }

                        taskToEdit.Duedate = newDueDate;
                        db.SaveChanges();
                        Console.WriteLine("Задача успешно отредактирована");
                    }
                    else
                    {
                        Console.WriteLine("Задача не найдена");
                    }
                }

                static void ViewAllTasks(User user)
                {
                    Console.WriteLine("Все задачи:");
                    foreach (var task in user.Tasks)
                    {
                        Console.WriteLine(
                            $"Task ID: {task.Taskid} - Title: {task.Title} - Description: {task.Description} - Due Date: {task.Duedate}");
                    }
                }

                static void ViewTasksForToday(User user)
                {
                    Console.WriteLine("Задачи на сегодня:");
                    var todayTasks = user.Tasks.Where(t =>
                        t.Duedate.HasValue && t.Duedate.Value.Date == DateTime.Today);
                    foreach (var task in todayTasks)
                    {
                        Console.WriteLine(
                            $"Task ID: {task.Taskid} - Title: {task.Title} - Description: {task.Description} - Due Date: {task.Duedate}");
                    }
                }

                static void ViewTasksForTomorrow(User user)
                {
                    Console.WriteLine("Задачи на завтра:");
                    var tomorrowTasks = user.Tasks.Where(t =>
                        t.Duedate.HasValue && t.Duedate.Value.Date == DateTime.Today.AddDays(1));
                    foreach (var task in tomorrowTasks)
                    {
                        Console.WriteLine(
                            $"Task ID: {task.Taskid} - Title: {task.Title} - Description: {task.Description} - Due Date: {task.Duedate}");
                    }
                }

                static void ViewTasksForThisWeek(User user)
                {
                    Console.WriteLine("Задачи на эту неделю:");
                    var weekTasks = user.Tasks.Where(t =>
                        !t.Duedate.HasValue || t.Duedate.Value.Date < DateTime.Today ||
                        t.Duedate.Value.Date > DateTime.Today.AddDays(7));
                    foreach (var task in weekTasks)
                    {
                        Console.WriteLine(
                            $"Task ID: {task.Taskid} - Title: {task.Title} - Description: {task.Description} - Due Date: {task.Duedate}");
                    }
                }
            }
        }
    }
    
}