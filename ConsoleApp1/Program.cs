using System;
using System.IO;
using System.Collections.Generic;
class MenuItem
{
    public string Description { get; }
    public decimal Price { get; }

    public MenuItem(string description, decimal price)
    {
        Description = description;
        Price = price;
    }
}
class Menu
{
    public string Title { get; }
    public List<MenuItem> Items { get; }

    public Menu(string title)
    {
        Title = title;
        Items = new List<MenuItem>();
    }

    public void AddItem(string description, decimal price)
    {
        Items.Add(new MenuItem(description, price));
    }
}

class CakeOrder
{
    public string Form { get; set; }
    public string Size { get; set; }
    public string Flavor { get; set; }
    public string Quantity { get; set; }
    public string Glaze { get; set; }
    public string Decor { get; set; }
    public decimal TotalPrice { get; set; }

    public CakeOrder()
    {
        Form = "";
        Size = "";
        Flavor = "";
        Quantity = "";
        Glaze = "";
        Decor = "";
    }

    public void PlaceOrder()
    {
        ConsoleKeyInfo key;
        do
        {
            Console.Clear();
            Console.WriteLine("Выбор торта:");
            Console.WriteLine("1. Выбрать форму");
            Console.WriteLine("2. Выбрать размер");
            Console.WriteLine("3. Выбрать вкус");
            Console.WriteLine("4. Выбрать количество");
            Console.WriteLine("5. Выбрать глазурь");
            Console.WriteLine("6. Выбрать декор");
            Console.WriteLine("7. Завершить заказ");
            Console.WriteLine();

            Console.WriteLine("Ваш заказ:");
            Console.WriteLine("Форма: " + Form);
            Console.WriteLine("Размер: " + Size);
            Console.WriteLine("Вкус: " + Flavor);
            Console.WriteLine("Количество: " + Quantity);
            Console.WriteLine("Глазурь: " + Glaze);
            Console.WriteLine("Декор: " + Decor);
            Console.WriteLine();

            Console.WriteLine("Цена: " + TotalPrice.ToString("F2") + " руб.");
            Console.WriteLine();

            key = Console.ReadKey();
            Console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Form = ChooseOption("Выберите форму", GetMenu("Формы"));
                    break;
                case ConsoleKey.D2:
                    Size = ChooseOption("Выберите размер", GetMenu("Размеры"));
                    break;
                case ConsoleKey.D3:
                    Flavor = ChooseOption("Выберите вкус", GetMenu("Вкусы"));
                    break;
                case ConsoleKey.D4:
                    Quantity = ChooseOption("Выберите количество коржей", GetMenu("Количество коржей"));
                    break;
                case ConsoleKey.D5:
                    Glaze = ChooseOption("Выберите глазурь", GetMenu("Глазури"));
                    break;
                case ConsoleKey.D6:
                    Decor = ChooseOption("Выберите декор", GetMenu("Декоры"));
                    break;
                case ConsoleKey.D7:
                    CalculateTotalPrice();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        } while (key.Key != ConsoleKey.D7);
    }

    private string ChooseOption(string title, Menu menu)
    {
        ConsoleKeyInfo key;
        int selectedOption = 0;

        do
        {
            Console.Clear();
            Console.WriteLine(title);

            for (int i = 0; i < menu.Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menu.Items[i].Description} ({menu.Items[i].Price.ToString("F2")} руб.)");
            }

            Console.WriteLine("Используйте стрелки вверх и вниз для выбора и Enter для подтверждения. Используйте Esc для выхода.");

            for (int i = 0; i < menu.Items.Count; i++)
            {
                Console.WriteLine((i == selectedOption) ? $"  > {menu.Items[i].Description}" : $"    {menu.Items[i].Description}");
            }

            key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow && selectedOption > 0)
            {
                selectedOption--;
            }
            else if (key.Key == ConsoleKey.DownArrow && selectedOption < menu.Items.Count - 1)
            {
                selectedOption++;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                return "";
            }

        } while (key.Key != ConsoleKey.Enter);

        TotalPrice += menu.Items[selectedOption].Price;
        return menu.Items[selectedOption].Description;
    }

    private void CalculateTotalPrice()
    {
        decimal basePrice = 0;

        if (!string.IsNullOrEmpty(Form))
        {
            basePrice = GetPriceForOption(Form, GetMenu("Формы"));
        }

        if (!string.IsNullOrEmpty(Size))
        {
            basePrice += GetPriceForOption(Size, GetMenu("Размеры"));
        }

        if (!string.IsNullOrEmpty(Flavor))
        {
            basePrice += GetPriceForOption(Flavor, GetMenu("Вкусы"));
        }

        if (!string.IsNullOrEmpty(Quantity))
        {
            basePrice += GetPriceForOption(Quantity, GetMenu("Количество коржей"));
        }

        if (!string.IsNullOrEmpty(Glaze))
        {
            basePrice += GetPriceForOption(Glaze, GetMenu("Глазури"));
        }

        if (!string.IsNullOrEmpty(Decor))
        {
            basePrice += GetPriceForOption(Decor, GetMenu("Декоры"));
        }

        TotalPrice = basePrice;
    }


    private decimal GetPriceForOption(string selectedOption, Menu menu)
    {
        foreach (var menuItem in menu.Items)
        {
            if (selectedOption == menuItem.Description)
            {
                return menuItem.Price;
            }
        }
        return 0;
    }

    public void SaveOrderToFile()
    {
        string orderDetails = $"Дата и время: {DateTime.Now:yyyy-MM-dd HH:mm:ss}, Форма: {Form}, Размер: {Size}, Вкус: {Flavor}, Количество: {Quantity}, Глазурь: {Glaze}, Декор: {Decor}, Стоимость: {TotalPrice.ToString("F2")} руб.";
        File.AppendAllText("История_заказов.txt", orderDetails + Environment.NewLine);
    }
    private Menu GetMenu(string menuName)
    {
        Menu menu = new Menu(menuName);
        if (menuName == "Формы")
        {
            menu.AddItem("Квадрат", 99.0m);
            menu.AddItem("Круг", 99.0m);
            menu.AddItem("Звезда", 119.0m);
        }
        else if (menuName == "Размеры")
        {
            menu.AddItem("Маленький", 99.0m);
            menu.AddItem("Средний", 149.0m);
            menu.AddItem("Большой", 199.0m);
        }
        else if (menuName == "Вкусы")
        {
            menu.AddItem("Ваниль", 20.0m);
            menu.AddItem("Шоколад", 15.0m);
            menu.AddItem("Клубника", 35.0m);
        }
        else if (menuName == "Глазури")
        {
            menu.AddItem("Шоколадная", 99.0m);
            menu.AddItem("Клубничная", 99.0m);
            menu.AddItem("Ванильная", 99.0m);
        }
        else if (menuName == "Декоры")
        {
            menu.AddItem("Сахарные украшения", 149.0m);
            menu.AddItem("Вафельные украшения", 249.0m);
            menu.AddItem("Несъедобные фигурки", 349.0m);
        }
        else if (menuName == "Количество коржей")
        {
            menu.AddItem("1 корж", 29.0m);
            menu.AddItem("2 коржа", 49.0m);
            menu.AddItem("3 коржа", 69.0m);
        }

        return menu;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var currentOrder = new CakeOrder();
        var orderHistoryFile = "История_заказов.txt";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать в кондитерскую!");

            Console.WriteLine("1. Сделать заказ");
            Console.WriteLine("2. Просмотреть историю заказов");
            Console.WriteLine("3. Выход");

            Console.Write("Выберите действие: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        currentOrder.PlaceOrder();
                        currentOrder.SaveOrderToFile();
                        Console.WriteLine("Заказ сохранен. Сумма заказа: " + currentOrder.TotalPrice.ToString("F2") + " руб.");
                        Console.WriteLine("Нажмите Enter для продолжения...");
                        Console.ReadLine();
                        currentOrder = new CakeOrder();
                        break;
                    case 2:
                        if (File.Exists(orderHistoryFile))
                        {
                            Console.WriteLine("История заказов:");
                            Console.WriteLine(File.ReadAllText(orderHistoryFile));
                        }
                        else
                        {
                            Console.WriteLine("История заказов пуста.");
                        }
                        Console.WriteLine("Нажмите Enter для продолжения...");
                        Console.ReadLine();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Некорректный выбор. Попробуйте снова.");
            }
        }
    }
}
