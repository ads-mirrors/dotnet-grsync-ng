using System;
using Gtk;

namespace Grsyncng
{
    public class MainClass
    {
        public static MainWindow mainWindow;
        public static CommandGenerator commandGenerator = new CommandGenerator();

        static CommandArgument[] commandArguments = new CommandArgument[]
        {
            new BoolArgument("-v", "increase verbosity"),
            new BoolArgument("-a", "archive mode"),
            new BoolArgument("-z", "compress data during file transfer"),
            new BoolArgument("--list-only", "only list files instead of copying"),
        };


        public static void Main(string[] args)
        {
            Application.Init();
            mainWindow = new MainWindow();

            mainWindow.LoadCommandArguments(commandArguments);
            UpdateCommand();
            mainWindow.Show();

            Application.Run();
        }


        public static void UpdateCommand()
        {
            mainWindow.UpdateCommand(commandGenerator.GenerateCommand(commandArguments));
        }
    }

}
