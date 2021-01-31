using System;
namespace Grsyncng
{
    public class StringArgument : CommandArgument
    {

        public override string GetCommandPart()
        {
            if((string) Value != "")
            {
                return " " + Value;
            }
            else
            {
                return "";
            }

        }
    }
}
