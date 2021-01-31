using System;
namespace Grsyncng
{
    public abstract class KeywordArgument : CommandArgument
    {
        public string Keyword;

        public KeywordArgument(string keyword)
        {
            this.Keyword = keyword;
        }
    }
}
