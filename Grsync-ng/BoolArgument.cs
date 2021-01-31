using System;
namespace Grsyncng
{
    public class BoolArgument : CommandArgument
    {
        public bool Value;

        public BoolArgument(string argument, string description) : base(argument, description)
        {

        }

        public override void SetValue(object value)
        {
            this.Value = (bool) value;
        }

        public override string GetCommandPart()
        {
            return Value ? " " + this.Argument : "";
        }
    }
}
