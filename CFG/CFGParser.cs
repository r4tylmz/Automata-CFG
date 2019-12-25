using System;
using System.Collections.Generic;
using System.IO;

namespace CFG
{
    public class CFGParser
    {
        public List<NonTerminal> GetNonTerminals(string parse)
        {
            var ntList = new List<NonTerminal>();
            
            var splitByComma = parse.Split(',');
            // S -> aX|bb,B->aa|bb
            foreach (var s in splitByComma) 
            {
                var termList = new List<string>();
                var splitByArrow = s.Split(new string[]{"->"},StringSplitOptions.None);
                var splitByOr = splitByArrow[1].Split('|'); // terminaller için
                foreach (var t in splitByOr)
                {
                    termList.Add(t);
                }
                ntList.Add(new NonTerminal(){NonTerminalName = splitByArrow[0],Terminals = termList});
            }
            
            return ntList;
        }
    }
}