using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ежедневник
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DateTime dateTime = DateTime.Now;
            date_choose.SelectedDate = dateTime;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<note> list = files.MyDeserialize<List<note>>("notes.json");
            note new_note = new note();
            new_note.created = date_choose.SelectedDate.Value;
            new_note.description = note_description.Text;
            new_note.name = note_name.Text;
            list.Add(new_note);
            files.MySerialize(list, "notes.json");
            note_description.Text = "";
            note_name.Text = "";
            Color darky = Color.FromRgb(50, 50, 50);
            Color lighty = Color.FromRgb(200, 200, 200);
            Button button = new Button();
            button.Content = new_note.name;
            button.Background = new SolidColorBrush(darky);
            button.Foreground = new SolidColorBrush(lighty);
            button.Click += Button_Click1;
            stack.Children.Add(button);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            note_name.Text = (string)(sender as Button).Content;
            List<note> list = files.MyDeserialize<List<note>>("notes.json");
            foreach (note note in list)
            {
                if (note_name.Text == note.name)
                {
                    note_description.Text=note.description;
                }
            }
        }

        private void date_choose_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            stack.Children.Clear();
            List<note> list = files.MyDeserialize<List<note>>("notes.json");
            foreach (note x in list)
            {
                if (x.created == date_choose.SelectedDate)
                {
                    Color darky = Color.FromRgb(50, 50, 50);
                    Color lighty = Color.FromRgb(200, 200, 200);
                    Button button = new Button();
                    button.Content = x.name;
                    button.Background = new SolidColorBrush(darky);
                    button.Foreground = new SolidColorBrush(lighty);
                    button.Click += Button_Click1;
                    stack.Children.Add(button);
                }
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            List<note> list = files.MyDeserialize<List<note>>("notes.json");
            int y = 0;
            bool result = false;
            foreach(note x in list)
            {
                if (x.name == note_name.Text)
                {
                    note_name.Text = "";
                    note_description.Text = "";
                    result = true;
                    break;
                }
                y++;
            }
            if (result == true)
            {
                list.RemoveAt(y);
                stack.Children.Clear();
                foreach (note x in list)
                {
                    if (x.created == date_choose.SelectedDate)
                    {
                        Color darky = Color.FromRgb(50, 50, 50);
                        Color lighty = Color.FromRgb(200, 200, 200);
                        Button button = new Button();
                        button.Content = x.name;
                        button.Background = new SolidColorBrush(darky);
                        button.Foreground = new SolidColorBrush(lighty);
                        button.Click += Button_Click1;
                        stack.Children.Add(button);
                    }
                }
                files.MySerialize(list, "notes.json");
            }
        }
    }
}
