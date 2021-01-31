using System;
using System.Collections.Generic;
using System.Linq;

namespace Grsyncng
{
    public class CommandGenerator
    {
        public CommandGenerator()
        {
        }

        public string GenerateCommand(Dictionary<Argument,CommandArgument> arguments)
        {
            string output = "rsync";

            output += GetSwitchPart(arguments);
            output += GetStringPart(arguments);

            return output;
        }

        private string GetStringPart(Dictionary<Argument, CommandArgument> arguments)
        {
            string source = arguments[Argument.Source].GetCommandPart();
            string destination = arguments[Argument.Destination].GetCommandPart();

            return source + destination;
        }

        private string GetSwitchPart(Dictionary<Argument, CommandArgument> arguments)
        {
            string output = "";

            bool archiveValue = (bool)arguments[Argument.Archive].Value;

            foreach (var kvp in arguments)
            {
                Argument arg = kvp.Key;

                // if we have archive mode enabled dont add -rltpgoD
                if (archiveValue && (arg == Argument.Recursive
                    || arg == Argument.BigD
                    || arg == Argument.PreserveGroup
                    || arg == Argument.PreserveOwner
                    || arg == Argument.PreserveModTime
                    || arg == Argument.PreservePermissions
                    || arg == Argument.Symlinks))
                {
                    continue;
                }

                if (kvp.Value is SwitchArgument)
                {
                    output += kvp.Value.GetCommandPart();
                }
            }

            return output;
        }
    }
}
