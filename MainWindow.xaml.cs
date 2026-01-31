using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Print_Management_System
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 1. Удаление операции
        private void DeleteOperation(object sender, RoutedEventArgs e)
        {

        }

        // 2. Редактирование операции
        private void EditOperation(object sender, RoutedEventArgs e)
        {

        }

        // 3. Изменение вида работы
        private void SelectedType(object sender, SelectionChangedEventArgs e)
        {
            
        }

        // 4. Изменение формата бумаги
        private void SelectedFormat(object sender, SelectionChangedEventArgs e)
        {
            
        }

        // 5. Изменение количества
        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Обработчик есть, но пустой
        }

        // 6. Проверка ввода (только цифры)
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только цифры
            
        }

        // 7. Добавление операции
        private void AddOperation(object sender, RoutedEventArgs e)
        {
        }

        // 8. Изменение параметров цвета/сторон
        private void ColorsChange(object sender, RoutedEventArgs e)
        {
        }
    }
}