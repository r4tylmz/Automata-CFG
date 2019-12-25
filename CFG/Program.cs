using System;
using System.Collections.Generic;
using System.Linq;

namespace CFG
{
    internal class Program
    {

        // S->aX|bb,X->cc
        // S->aXm|bb,X->cc
        // S->aa|bX|aXX,X->ab|b
        // S->aa|b,X->bb
        private static void Cfg(List<NonTerminal> ntList)
        {
            // NONTERMS LISTESI NONTERMINALLERI TUTAR
           List<string> nonTerms = ntList.Select(nonTerminal => nonTerminal.NonTerminalName).ToList();
           List<string> controlList = new List<string>();
           List<string> dupeList = new List<string>();
           bool control = true;
           while (control)
           {
               control = false;
               // LISTENIN ICINDE NON-TERMINAL IFADE VAR MI DIYE KONTROL EDILIYOR
               // VARSA CONTROL TEKRAR TRUE OLUYOR
               foreach (var term in ntList[0].Terminals)
               {
                   foreach (var nonTerm in nonTerms)
                   {
                       if (term.Contains(nonTerm))
                       {
                           control = true;
                           break;
                       }
                   }
               }
               
               // EGER USTTEKI FOR DONGUSU CONTROL DEGERINI TRUE YAPAMAZSA
               // NON-TERMINAL IFADE YOKTUR DEMEKTIR WHILE DONGUSUNU KIRAR
               if(control==false)
                   break;
                
               // 
               foreach (var terminal in ntList[0].Terminals.ToList())
               {
                   bool nonTerminalCheck = false;
                   // HER BIR TERMINAL ICIN TERMINALIN UZUNLUGU KADAR DONUYOR
                   for (int i = 0; i < terminal.Length; i++)
                   {
                       // NONTERMS LISTESI TERMINAL OLMAYANLARI TUTAR
                       // NONTERMS LISTESININ ICINDE TERMINAL'IN i. HARFI VAR MI? KONTROL EDER
                       if (nonTerms.Contains(terminal[i].ToString()))
                       {
                           // EGER BURAYA GIRDIYSE ARTIK TERMINAL[i]'nin NON-TERMINAL OLDUGUNU ANLARIZ
                           nonTerminalCheck = true;
                           // NTLISTTEN NONTERMINALIN ADINA GORE SORGU YAPILIR VE NonTerminal DONDURUR
                           var nonTerminal = ntList.Find(m => m.NonTerminalName.Equals(terminal[i].ToString()));
                           // BU NON-TERMINAL IFADENIN TERMINALLERI ICIN DONGU OLUSTURDUM
                           for (int j = 0; j < nonTerminal.Terminals.Count; j++)
                           {
                               // ORNEK: S->aXm, X->bb|cc ICIN 
                               // a(substring(0,i)) + bb(terminal[j]) + m(substring(i+1))
                               // SEKLINDE WORD'E ATILIR DONGU TEKRAR DONUNCE NON-TERMINAL IFADENIN DIGER TERMINALINE GECILIR
                               // a + cc + m SEKLINDE WORD'E ATILIR
                               // EGER BU IFADE KONTROL LISTESINDE MEVCUT DEGILSE KONTROL LISTESINE, MEVCUTSA TEKRARLANAN LISTESINE EKLENIR
                               string word = terminal.Substring(0,i)+nonTerminal.Terminals[j]+terminal.Substring(i+1);
                               if(!controlList.Contains(word))
                                   controlList.Add(word);
                               else
                                   dupeList.Add(word);
                           }
                       }
                   }
                   // EGER YUKARIDAKI YAPIDAN nonTerminalCheck DEGERI TRUE OLMAZSA
                   // TERMINAL IFADESI ICINDE NON-TERMINAL BARINDIRMAZ VE DIREK LISTELERE EKLENIR
                   // EGER BU IFADE KONTROL LISTESINDE MEVCUT DEGILSE KONTROL LISTESINE, MEVCUTSA TEKRARLANAN LISTESINE EKLENIR
                   if (nonTerminalCheck == false)
                   {
                       if (controlList.Contains(terminal) == false)
                           controlList.Add(terminal);
                       else
                           dupeList.Add(terminal);
                   }
               }
               // BASLANGIC NOKTASININ TERMINALLERINI KONTROL LISTESINDEKILERLE DEGISTIRDIM
               ntList[0].Terminals = controlList.ToList();
               // KONTROL LISTESI TEKRAR KULLANILMAK ICIN SIFIRLANDI
               controlList.Clear();
           }

           Console.WriteLine("------------ URETILEN KELIMELER ------------");
           foreach (var terminal in ntList[0].Terminals)
           {
               Console.WriteLine(terminal);
           }

           Console.WriteLine("------------ TEKRARLANANLAR ------------");
           foreach (var dupe in dupeList)
           {
               Console.WriteLine(dupe);
           }
        }

        public static void Main(string[] args)
        {
            Console.Write("Giris: ");
            var line = Console.ReadLine();
            var parser = new CFGParser();
            List<NonTerminal> nonTerminals = parser.GetNonTerminals(line);
            Cfg(nonTerminals);
        }
    }
}