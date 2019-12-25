using System.Collections.Generic;

namespace CFG
{
    public class NonTerminal
    {
        public string NonTerminalName { get; set; }
        public List<string> Terminals;
    }
}