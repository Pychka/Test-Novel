using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text;

namespace TestNovell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextWorks tw;
        string text = null, person = null;
        string[] choiceItems = null;
        int line = 0, numChoice = 0;
        bool switchAnotherScene = true;
        public MainWindow()
        {
            InitializeComponent();
            Start(line);
        }
        private void CreateNewMessage(string text, string person)
        {
            DialogHeader.Text = person;
            DataDialog.Text = text;
        }
        private void CreatingNewSurvey(string[] choiceItems)
        {
            for (int i = 0; i < choiceItems.Length; i++)
            {
                Button choiceButton = new Button();
                choiceButton.Width = 100;
                choiceButton.Height = 30;
                choiceButton.Content = choiceItems[i];
                choiceButton.Tag = i;
                choiceButton.Click += ChoiceButton_Click;
                ChoicesBox.Children.Add(choiceButton);
            }
        }

        private void ChoiceButton_Click(object sender, RoutedEventArgs e)
        {
            Button changeButton = (Button)sender;
            tw.TransitionSelectedBlock((int)changeButton.Tag, numChoice);
            Start(line += 1);
            ChoicesBox.Children.Clear();
        }

        private void Background_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Start(line += 1);
        }

        private void Start(int line)
        {
            tw = new(File.ReadAllLines("Dialogs.txt"), line);
            tw.InspectionLine(ref text, ref person, ref numChoice, ref choiceItems);
            CreateNewMessage(text, person);
            string gay = "";
            if (choiceItems.Length > 0)
            {
                CreatingNewSurvey(choiceItems);
                Background.PreviewMouseLeftButtonDown -= Background_MouseDown;
            }
            else
            {
                Background.PreviewMouseLeftButtonDown += Background_MouseDown;
            }
        }
    }
}
