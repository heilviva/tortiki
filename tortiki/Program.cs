using System.Text;
using System.Xml.Linq;

namespace Tortiki
{
    internal class Elements
    {
        // Конструктор класса
        public Elements(int element, string description, int price, Action action)
        {
            // Свойства класса
            this.EL = element;          // идентификатор 
            this.Description = description; // описание 
            this.Price = price;         // цена 
            this.Action = action;       // соответствующее действие
        }

        // Определение свойств класса
        public int EL { get; set; }      
        public string Description { get; set; } 
        public int Price { get; set; }
        public Action Action { get; set; }
    }

    // статический класса "Global"
    public static class Global
    {
        // Определение глобальной статической переменной для хранения цены
        public static int Price = 0;

        // Определение глобальной статической переменной для хранения списка заказов
        public static List<string> Order = new List<string> { };
    }
    public class Menu //хотела не делать элементы а всё забросить в меню но видимо программа не может выполнять столько ссылок в одном месте
{
    public Menu PlayMenu { get; set; }
    public string Imya { get; set; }
    public string Arrow { get; set; }
    public ConsoleColor ArrowColour { get; set; }
    public ConsoleColor ForegroundColour { get; set; }
    public ConsoleColor MenuItemColour { get; set; }
    public ConsoleColor ImyaColour { get; set; }

    public int AllPrice { get; private set; } = 0;

    private List<Elements> ItemList;
    public int[] prices = new int[] { };

    private int position;
    private bool Exit;
    public int Cena;

    public Menu(string arrow = "=>")
    {
        ItemList = new List<Elements>();
        position = 0;

        this.Arrow = arrow;
        ArrowColour = ConsoleColor.Blue;
        ForegroundColour = ConsoleColor.White;
        MenuItemColour = ConsoleColor.White;
        ImyaColour = ConsoleColor.Red;
    }
        public void Draw()
        {
            Console.Clear();
            Console.WriteLine(Imya);

            for (int i = 0; i < ItemList.Count; i++)
            {
                if (i == position)
                {
                    Console.ForegroundColor = ArrowColour;
                    Console.Write(Arrow + " ");
                }
                else
                {
                    Console.Write(new string(' ', Arrow.Length + 1));
                }

                Console.WriteLine(ItemList[i].Description);

                if (i == position)
                {
                    Console.ForegroundColor = ForegroundColour;
                }
            }

            Console.WriteLine($"Цена: {Global.Price}");

            if (Global.Order.Count > 0)
            {
                Console.WriteLine("Заказик: ");
                foreach (string item in Global.Order)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("Заказ пока пуст.");
            }
        }
        public void ShowMenu()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Draw();
        Exit = false;
        while (!Exit)
        {
            MenuUpdate();

        }
    }

    public void HideMenu()
    {

        Exit = true;
    }

    public void MenuUpdate()
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.Enter:
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    ItemList[position].Action();
                    Global.Price += ItemList[position].Price;
                    Global.Order.Add(ItemList[position].Description);
                    Console.CursorVisible = false;
                    Console.Clear();
                    Draw();
                    break;
                }
            case ConsoleKey.Escape:
                {
                    if (PlayMenu != null)
                    {
                        HideMenu();
                    }
                    break;
                }
            case ConsoleKey.UpArrow:
                {
                    position--;
                    if (position < 0)
                    {
                        position++;
                        Console.Clear();
                        Draw();

                    }
                    break;
                }
            case ConsoleKey.DownArrow:
                {

                    if (position < (ItemList.Count - 1))
                    {
                        position++;
                        Console.Clear();
                        Draw();

                    }
                    break;
                }
        }
    }

        public bool AddItem(int element, string description, int price, Action action)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (ItemList.Any(item => item.EL == element))
                return false;

            var newItem = new Elements(element, description, price, action);
            ItemList.Add(newItem);

            return true;
        }
    }
internal class Program
{
    static void Main()
    {
        Menu mainMenu = new Menu();
        Menu Forma= new Menu("=>");
        Forma.Imya = "Форма торта";
        Forma.AddItem(0, "Квадрат - 200", 200, Forma.HideMenu);
        Forma.AddItem(1, "Круг - 300", 300, Forma.HideMenu);
        Forma.AddItem(2, "Прямоугольный ну почти - 400", 400, Forma.HideMenu);
        Forma.PlayMenu = mainMenu;



        Menu Size= new Menu("=>");
        Size.Imya = "Размер тортика";
        Size.AddItem(0, "маленький - 1000", 1000, Size.HideMenu);
        Size.AddItem(1, "большой - 2000", 2000, Size.HideMenu);
        Size.AddItem(2, "гигантский - 3000", 3000, Size.HideMenu);



        Size.PlayMenu = mainMenu;

        Menu Taste = new Menu("=>");
        Taste.Imya = "Вкус коржей";
        Taste.AddItem(0, "вкусный - 300", 300, Taste.HideMenu);
        Taste.AddItem(1, "невкусный - 200", 200, Taste.HideMenu);
        Taste.AddItem(2, "полувкусный- 100", 100, Taste.HideMenu);
        Taste.prices = new int[] { 300, 350, 500 };
        Taste.PlayMenu = mainMenu;

        Menu Nachinka = new Menu("=>");
        Nachinka.Imya = "Начинка тортика";
        Nachinka.AddItem(0, "Шоколад - 200", 200, Nachinka.HideMenu);
        Nachinka.AddItem(1, "Клубника - 300", 300, Nachinka.HideMenu);
        Nachinka.AddItem(2, "ещё что-то - 400", 400, Nachinka.HideMenu);
        Nachinka.prices = new int[] { 250, 300, 400};


        Nachinka.PlayMenu = mainMenu;

        Menu Yarys = new Menu("=>");
        Yarys.Imya = "Количество ярусов";
        Yarys.AddItem(0, "Один ярус - 300", 300, Yarys.HideMenu);
        Yarys.AddItem(1, "Два яруса - 400", 400, Yarys.HideMenu);
        Yarys.AddItem(2, "Три яруса - 500", 500, Yarys.HideMenu);

        Yarys.PlayMenu = mainMenu;

        Menu Dekor = new Menu("=>");
        Dekor.Imya = "Декор";
        Dekor.AddItem(0, "Ягоды - 100", 100, Dekor.HideMenu);
        Dekor.AddItem(1, "Посыпка - 100", 100, Dekor.HideMenu);
        Dekor.AddItem(3, "Фрукты-100", 100, Dekor.HideMenu);

        Dekor.PlayMenu = mainMenu;

        mainMenu.Imya = " Тортики ждут вас) Сделай свой заказ";
        mainMenu.AddItem(0, "Форма", 0, Forma.ShowMenu);
        mainMenu.AddItem(1, "Размер", 0, Size.ShowMenu);
        mainMenu.AddItem(2, "Вкус Коржей", 0, Taste.ShowMenu);
        mainMenu.AddItem(3, "Начинка", 0, Nachinka.ShowMenu);
        mainMenu.AddItem(4, "Количество ярусов", 0, Yarys.ShowMenu);
        mainMenu.AddItem(5, "Дополнительный декор", 0, Dekor.ShowMenu);
        mainMenu.AddItem(6, "Конец оформления заказа", 0, Exit);

        mainMenu.ShowMenu();

    }

    private static void Exit()
    {
        Console.WriteLine("Ваш заказ оформляется)");

        string path = "C:\\Users\\rhali\\Desktop\\История действий.txt";
        StringBuilder orderDescription = new StringBuilder("Заказ:\n");

        foreach (string item in Global.Order)
        {
            orderDescription.AppendLine(item);
        }

        orderDescription.AppendLine("Цена: " + Global.Price);

        // File.AppendAllText() создаст файл, если он не существует.
        File.AppendAllText(path, orderDescription.ToString());

        // При Environment.Exit(0) все операции возвращения ресурсов, заключительные операции и завершающие операции не выполняются.Можно заменить на return.              Можно заменить его на return.
        Environment.Exit(0);
    }
    
}
}

