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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для AddVlad.xaml
    /// </summary>
    public partial class AddVlad : Window
    {
        private Владельцы _currentVlad = new Владельцы();

        public AddVlad(Владельцы selectedVlad)
        {
            InitializeComponent();
            if (selectedVlad != null)
                _currentVlad = selectedVlad;

            DataContext = _currentVlad;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            if (string.IsNullOrEmpty(_currentVlad.Фамилия))
                errors.AppendLine("Укажите Фамилию");
            if (string.IsNullOrEmpty(_currentVlad.Имя))
                errors.AppendLine("Укажите Имя");
            if (string.IsNullOrEmpty(_currentVlad.Отчество))
                errors.AppendLine("Укажите Отчество");
            if (string.IsNullOrEmpty(_currentVlad.Телефон))
                errors.AppendLine("Укажите Телефон");
            if (string.IsNullOrEmpty(_currentVlad.Адрес))
                errors.AppendLine("Укажите Адрес");
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_currentVlad.Код_владельца == 0)
                agentnedvEntities.GetContext().Владельцы.Add(_currentVlad);

            try
            {
                agentnedvEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
