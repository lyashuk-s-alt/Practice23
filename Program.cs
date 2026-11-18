using System;
using System.Collections.Generic;

namespace DictionaryPractice
{
    // Клас Співробітник (значення у словнику)
    class Employee
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        public Employee(string fullName, string position, decimal salary)
        {
            FullName = fullName;
            Position = position;
            Salary = salary;
        }

        public override string ToString()
        {
            return "ПІБ: " + FullName + " | Посада: " + Position + " | Зарплата: " + Salary + " грн";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            // Створюємо словник: ключ - табельний номер (int), значення - об'єкт Employee
            Dictionary<int, Employee> employees = new Dictionary<int, Employee>();

            // Додаємо кілька початкових записів для зручності тестування
            employees.Add(101, new Employee("Іваненко Олександр", "Розробник C#", 45000));
            employees.Add(102, new Employee("Петренко Марина", "Тестувальник QA", 32000));
            employees.Add(103, new Employee("Сидоренко Андрій", "Менеджер проєктів", 50000));

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n================ МЕНЮ ВІДДІЛУ КАДРІВ ================");
                Console.WriteLine("1. Додати нового співробітника (Загальне завдання)");
                Console.WriteLine("2. Знайти співробітника за табельним номером (Загальне завдання)");
                Console.WriteLine("3. Видалити співробітника за табельним номером (Домашнє завдання)");
                Console.WriteLine("4. Вивести повний список усіх працівників (Домашнє завдання)");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee(employees);
                        break;
                    case "2":
                        FindEmployee(employees);
                        break;
                    case "3":
                        RemoveEmployee(employees);
                        break;
                    case "4":
                        ShowAllEmployees(employees);
                        break;
                    case "0":
                        isRunning = false;
                        Console.WriteLine("Роботу програми завершено.");
                        break;
                    default:
                        Console.WriteLine("Помилка: невідома команда. Оберіть пункт від 0 до 4.");
                        break;
                }
            }
        }

        // Метод додавання співробітника
        static void AddEmployee(Dictionary<int, Employee> dict)
        {
            Console.Write("Введіть табельний номер (ціле число): ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Помилка: табельний номер має бути цілим числом.");
                return;
            }

            // Перевіряємо, чи не існує вже такий ключ, щоб уникнути ArgumentException
            if (dict.ContainsKey(id))
            {
                Console.WriteLine("Помилка: співробітник із табельним номером " + id + " вже існує!");
                return;
            }

            Console.Write("Введіть ПІБ співробітника: ");
            string name = Console.ReadLine();

            Console.Write("Введіть посаду: ");
            string position = Console.ReadLine();

            Console.Write("Введіть зарплату (грн): ");
            decimal salary;
            if (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Помилка: некоректне значення зарплати.");
                return;
            }

            dict.Add(id, new Employee(name, position, salary));
            Console.WriteLine("Успішно: співробітника з табельним номером " + id + " додано до бази.");
        }

        // Метод пошуку за табельним номером
        static void FindEmployee(Dictionary<int, Employee> dict)
        {
            Console.Write("Введіть табельний номер для пошуку: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Помилка: введіть коректне ціле число.");
                return;
            }

            // Безпечний пошук без генерації винятку за допомогою TryGetValue
            Employee foundEmployee;
            if (dict.TryGetValue(id, out foundEmployee))
            {
                Console.WriteLine("Знайдено запис [Табельний № " + id + "]:");
                Console.WriteLine("   " + foundEmployee.ToString());
            }
            else
            {
                Console.WriteLine("Співробітника з табельним номером " + id + " не знайдено.");
            }
        }

        // Метод видалення за ключем (Домашнє завдання)
        static void RemoveEmployee(Dictionary<int, Employee> dict)
        {
            Console.Write("Введіть табельний номер співробітника для видалення: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Помилка: введіть коректне ціле число.");
                return;
            }

            // Метод Remove повертає true, якщо елемент знайдено і видалено, і false - якщо ключа немає
            if (dict.Remove(id))
            {
                Console.WriteLine("Успішно: співробітника з табельним номером " + id + " видалено зі словника.");
            }
            else
            {
                Console.WriteLine("Помилка: співробітника з табельним номером " + id + " не існує.");
            }
        }

        // Метод виведення повного списку працівників (Домашнє завдання)
        static void ShowAllEmployees(Dictionary<int, Employee> dict)
        {
            Console.WriteLine("=== Повний список працівників (всього: " + dict.Count + ") ===");
            if (dict.Count == 0)
            {
                Console.WriteLine("Словник порожній.");
                return;
            }

            // Перебір пар ключ-значення у словнику
            foreach (KeyValuePair<int, Employee> pair in dict)
            {
                Console.WriteLine("Табельний № " + pair.Key + " -> " + pair.Value.ToString());
            }
        }
    }
}
