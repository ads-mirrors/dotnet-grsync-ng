using System;
namespace Grsyncng
{
    public class CommandArgument
    {
        public string Argument;
        public string Description;

        public CommandArgument(string argument, string description)
        {
            this.Argument = argument;
            this.Description = description;
        }

        public virtual string GetCommandPart()
        {
            throw new NotImplementedException();
        }

        public virtual void SetValue(object value)
        {
            throw new NotImplementedException();
        }
    }
}
