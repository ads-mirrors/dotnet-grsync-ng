using System;
namespace Grsyncng
{
    public class ValueArgument : KeywordArgument
    {

        public ValueArgument(string keyword) : base(keyword)
        {
        }

        public override string GetCommandPart()
        {
            if((string) Value != "")
            {
                return " " + Keyword + "=" + Value;
            }
            else
            {
                return "";
            }
        }
    }
}
