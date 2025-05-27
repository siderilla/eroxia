namespace eroxia
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var storage = new DBStorage();
            var logic = new BusinessLogic(storage);
            var tui = new Tui(logic);
            tui.Start();
        }
    }
}

// fare un print dei prodotti
// fare un print degli employee

//la tui non dipende dallo storage, ma dalla logica di business