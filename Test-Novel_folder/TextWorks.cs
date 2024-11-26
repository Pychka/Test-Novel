using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestNovell
{
    class TextWorks
    {
        private string[] HistoryLines { get; set; }//Массив строк истории
        private int Line { get; set; }//Строка на который мы находимся по истории
        private string Name { get; set; }
        private bool MW { get; set; }//Пол игрока: мужской/женский
        public TextWorks(string[] Lines, int line)
        {
            HistoryLines = Lines;
            Line = line;
        }
        public void PreparingLine(ref string text, ref string person, out string[] choiceItems)
        {
            Regex reg = new Regex(@"/choice{\d*}");
            MatchCollection matches = reg.Matches(HistoryLines[Line]);
            if (matches.Count > 0)
            {
                HistoryLines[Line] = HistoryLines[Line].Replace(matches[0].Value, "");
                reg = new Regex(@"/choiseItem{\d*}_");
                matches = reg.Matches(HistoryLines[Line]);
                choiceItems = new string[matches.Count];
                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        choiceItems[i] = matches[i].Value;
                        HistoryLines[Line] = HistoryLines[Line].Replace(matches[i].Value, "");
                    }
                }
            }
            Name = "Алекс";
            text = person = null;
            choiceItems = null;
            if (HistoryLines[Line].StartsWith("/")) person = HistoryLines[Line].Split(" ")[0].Split("/")[1];
            else if (HistoryLines[Line].StartsWith("*")) person = "Расказчик";
            if (person == "Расказчик") text = HistoryLines[Line].Split("*")[1];
            else text = HistoryLines[Line].Split($"/{person}")[1];
            if (HistoryLines[Line].Contains("/gen"))
            {
                if (MW) HistoryLines[Line] = HistoryLines[Line].Replace("/gen", "а");
                else HistoryLines[Line] = HistoryLines[Line].Replace("/gen", "");
            }
            if (HistoryLines[Line].Contains("/name")) HistoryLines[Line] = HistoryLines[Line].Replace("/name", Name);
            text = HistoryLines[Line];
        }
    }
}
