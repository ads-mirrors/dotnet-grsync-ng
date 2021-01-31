using System;
namespace Grsyncng
{
    public class CommandGenerator
    {
        public CommandGenerator()
        {
        }

        public string GenerateCommand(CommandArgument[] arguments)
        {
            string output = "rsync";

            foreach(CommandArgument argument in arguments)
            {
                output += argument.GetCommandPart();
            }

            return output;
        }
    }
}
