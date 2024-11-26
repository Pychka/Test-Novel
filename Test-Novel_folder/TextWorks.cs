using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace TestNovell
{
    class TextWorks
    {
        private string[] HistoryLines { get; set; }//Массив строк истории
        private int Line { get; set; }//Строка на который мы находимся по истории
        private string Name { get; set; }
        private bool MW { get; set; }//Пол игрока: мужской - true/женский - false
        public TextWorks(string[] Lines, int line)
        {
            HistoryLines = Lines;
            Line = line;
        }
        public void InspectionLine(ref string text, ref string person, ref int numChoice,ref string[] choiceItems)
        {
            Name = "Гоша";
            text = HistoryLines[Line];
            InsertCommands(ref text, "/name", Name);
            InsertCommands(ref text, "*");
            if (text.StartsWith("/choice{0}"))
            {

            }
            SearchCommand(ref text, @"/choice{\d*}", ref numChoice, ref choiceItems);
            SearchCommand(ref text, @"/choiseItem{\d*}+_(\w+)", ref numChoice, ref choiceItems);
            InsertCommands(ref text, "/gen", MW ? "" : "а");
            SearchPerson(ref text, ref person);
        }
        private void InsertCommands(ref string line, string command, string swap = "") => line = line.Replace(command, swap);
        private void SearchCommand(ref string text, string command, ref int numChoice, ref string[] choiceItems)
        {
            MatchCollection MC = Regex.Matches(text, command);
            choiceItems = new string[MC.Count];
            if (command == @"/choice{\d*}" && MC.Count>0) numChoice = Convert.ToInt32(MC[0].Value.Replace("/choice{","").Replace("}",""));
            for (int i = 0; i < MC.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(MC[i].Groups[1].Value)) choiceItems[i] = MC[i].Groups[1].Value;
            }
            command = command.Replace(@"(\w+)", "");
            text = Regex.Replace(text, command, "");
        }
        private void SearchPerson(ref string text, ref string person)
        {
            if (text.StartsWith("/"))
            {
                person = text.Split(' ', 2)[0].Replace("/", "");
                text = text.Replace("/" + person + " ", "");
                person += ":";
            }
            else person = "Рассказчик:";
        }
        public void TransitionSelectedBlock(int tag, int numChoice)
        {
            foreach (var line in HistoryLines)
            {
                if (line.Contains("/choice{" + numChoice + "}")) MessageBox.Show(tag.ToString()); break;
            }
        }
    }
}
