using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNovell
{
    class TextWorks
    {
        private string[] HistoryLines { get; set; }
        private int Line { get; set; }
        private string Name { get; set; }
        public TextWorks(string[] Lines, int line)
        {
            HistoryLines = Lines;
            Line = line;
        }
        public void PreparingLine(ref string text, ref string person, ref string[] choiceItems)
        {
            text = person = null;
            choiceItems = null;
            if (HistoryLines[Line].StartsWith("/")) person = HistoryLines[Line].Split(" ")[0].Split("/")[1];
            else if (HistoryLines[Line].StartsWith("*")) person = "Расказчик";
            Name = person;
            if (person == "Расказчик") text = HistoryLines[Line].Split("*")[1];
            else text = HistoryLines[Line].Split($"/{person}")[1];
            if (HistoryLines[Line].Contains("/gen"))
            {
                if (Name == "Алиса") HistoryLines[Line] = HistoryLines[Line].Replace("/gen", "а");
                else HistoryLines[Line] = HistoryLines[Line].Replace("/gen", "");
            }
            text = HistoryLines[Line];
        }
    }
}
