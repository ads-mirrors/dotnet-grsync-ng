using System;
namespace Grsyncng
{
    public class SwitchArgument : KeywordArgument
    {
        public string Description;

        public SwitchArgument(string keyword, string description) : base(keyword)
        {
            this.Description = description;
            this.Value = false;
        }

        public override string GetCommandPart()
        {
            if((bool) Value)
            {
                return " " + this.Keyword;
            }
            else
            {
                return "";
            }
        }
    }
}
