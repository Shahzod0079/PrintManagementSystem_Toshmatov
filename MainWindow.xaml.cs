using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Print_Management_System.Classes;

namespace Print_Management_System
{
    public partial class MainWindow : Window
    {

        public List<TypeOperation> typeOperationList = TypeOperation.AllTypeOperation();

        public List<Format> formatsList = Format.AllFormats();
        public MainWindow()
        {
            InitializeComponent();

            LoadData();
        }
        void LoadData()
        {
            foreach (TypeOperation items in typeOperationList)
            {
                typeOperation.Items.Add(items.name);
            }

            foreach (Format item in formatsList)
            {
                formats.Items.Add(item.format);
            }
        }


        // 1. Удаление операции
        private void DeleteOperation(object sender, RoutedEventArgs e)
        {
            if (Operations.SelectedIndex != -1)
            {
                Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]);
                CalculationsAllPrice();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите операцию для удаления.");
            }
        }

        // 2. Редактирование операции
        private void EditOperation(object sender, RoutedEventArgs e)
        {
            if (Operations.SelectedIndex != -1)
            {
                TypeOperationsWindow newTOW = Operations.Items[Operations.SelectedIndex] as TypeOperationsWindow;

                typeOperation.SelectedItem = typeOperationList.Find(x => x.id == newTOW.typeOperation).name;
                formats.SelectedItem = formatsList.Find(x => x.id == newTOW.format).format;

                if (newTOW.side == 1) TwoSides.IsChecked = false;
                else if (newTOW.side == 2) TwoSides.IsChecked = true;

                Colors.IsChecked = newTOW.color;

                string[] resultColor = newTOW.colorText.Split(' ');
                if (resultColor.Length == 1) LotOfColor.IsChecked = false;
                else if (resultColor.Length == 2) LotOfColor.IsChecked = true;

                textBoxCount.Text = newTOW.count.ToString();
                textBoxPrice.Text = newTOW.price.ToString();

                addOperationButton.Content = "Изменить";

                Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]);
            }
            else MessageBox.Show("Пожалуйста, выберите операцию для редактирования.");
        }

        // 3. Изменение вида работы
        private void SelectedType(object sender, SelectionChangedEventArgs e)
        {
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as String == "Сканирование")
                {
                    formats.SelectedIndex = -1;
                    TwoSides.IsChecked = false;
                    Colors.IsChecked = false;
                    LotOfColor.IsChecked = false;

                    formats.IsEnabled = false;
                    TwoSides.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfColor.IsEnabled = false;
                }
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    formats.IsEnabled = true;
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                }

                if (formats.SelectedItem as String == "A4")
                {
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = false;
                }
                else if (formats.SelectedItem as String == "A3")
                {
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = false;
                }
                else if (formats.SelectedItem as String == "A2" || formats.SelectedItem as String == "A1")
                {
                    TwoSides.IsEnabled = false;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = true;
                }
                else if (typeOperation.SelectedItem as String == "Размер")
                {
                    formats.SelectedIndex = 0;
                    formats.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfColor.IsEnabled = false;
                }
            }

            if (textBoxCount.Text.Length == 0)
                textBoxCount.Text = "1";

            CostCalculations();
        }

        // 4. Изменение формата бумаги
        private void SelectedFormat(object sender, SelectionChangedEventArgs e)
        {
            if (formats.SelectedItem as String == "A4")
            {
                TwoSides.IsEnabled = true;
                Colors.IsEnabled = true;
                LotOfColor.IsEnabled = false;
            }
            else if (formats.SelectedItem as String == "A3")
            {
                TwoSides.IsEnabled = true;
                Colors.IsEnabled = false;
                LotOfColor.IsEnabled = false;
            }
            else
            {
                TwoSides.IsEnabled = false;
                Colors.IsEnabled = true;
                LotOfColor.IsEnabled = true;
            }

            if (textBoxCount.Text.Length == 0)
                textBoxCount.Text = "1";

            CostCalculations();
        }

        // 5. Изменение количества
        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e) =>

            CostCalculations();


        // 6. Проверка ввода (только цифры)
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }

        }

        // 7. Добавление операции
        private void AddOperation(object sender, RoutedEventArgs e)
        {

            TypeOperationsWindow newTOW = new TypeOperationsWindow();

            newTOW.typeOperationText = typeOperation.SelectedItem as String;
            newTOW.typeOperation = typeOperationList.Find(x => x.name == newTOW.typeOperationText).id;

            if (formats.SelectedIndex != -1)
            {
                newTOW.formatText = formats.SelectedItem as String;
                newTOW.format = formatsList.Find(x => x.format == newTOW.formatText).id;
            }

            if (TwoSides.IsEnabled == true)
            {
                if (TwoSides.IsChecked == false)
                    newTOW.side = 1;
                else
                    newTOW.side = 2;
            }
            if (Colors.IsChecked == false)
            {
                newTOW.colorText = "Ч/Б";
                newTOW.color = false;
            }
            else
            {
                newTOW.colorText = "ЦВ";
                newTOW.color = true;
            }

            if (LotOfColor.IsChecked == true)
            {
                newTOW.colorText += "(> 50%)";
                newTOW.occupancy = true;
            }

            newTOW.count = int.Parse(textBoxCount.Text);
            newTOW.price = float.Parse(textBoxPrice.Text);
            addOperationButton.Content = "Добавить";
            Operations.Items.Add(newTOW);
            CalculationsAllPrice();


            if (journalWindow != null)
            {
                string user = users.SelectedItem?.ToString() ?? usersName.Text;
                journalWindow.AddRecordFromMainWindow(newTOW, user);
            }
        }


        // 8. Изменение параметров цвета/сторон
        private void ColorsChange(object sender, RoutedEventArgs e) =>

            CostCalculations();

        public void CostCalculations()
        {
            float price = 0;
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as String == "Сканирование")
                    price = 10;
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    if (formats.SelectedItem as String == "A4")
                    {
                        if (TwoSides.IsChecked == false)
                        {
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 4;
                                else price = 3;
                            }
                            else
                                price = 20;
                        }
                        else
                        {
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 6;
                                else price = 4;
                            }
                            else
                                price = 35;
                        }
                    }
                    else if (formats.SelectedItem as String == "A3")
                    {
                        if (TwoSides.IsChecked == false)
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 8;
                            else price = 6;
                        }
                        else
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 12;
                            else price = 10;
                        }
                    }
                    else if (formats.SelectedItem as String == "A2")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 35;
                            else
                                price = 50;
                        }
                        else
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 120;
                            else
                                price = 170;
                        }
                    }
                    else if (formats.SelectedItem as String == "A1")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 75;
                            else
                                price = 120;
                        }
                        else
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 170;
                            else
                                price = 250;
                        }
                    }
                }
                else if (typeOperation.SelectedItem as String == "Ризограф")
                {
                    if (TwoSides.IsChecked == false)
                    {
                        if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                            price = 1.40f;
                        else if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 200 && int.Parse(textBoxCount.Text) >= 100)
                            price = 1.10f;
                        else price = 1;
                    }
                    else
                    {
                        if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                            price = 1.80f;
                        else if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 200 && int.Parse(textBoxCount.Text) >= 100)
                            price = 1.40f;
                        else price = 1.10f;
                    }
                }
            }
            if (textBoxCount.Text.Length > 0)
                textBoxPrice.Text = (float.Parse(textBoxCount.Text) * price).ToString();
        }

        public void CalculationsAllPrice()
        {
            float allPrice = 0;

            for (int i = 0; i < Operations.Items.Count; i++)
            {
                TypeOperationsWindow newTOW = Operations.Items[i] as TypeOperationsWindow;
                allPrice += newTOW.price;
            }
            labelAllPrice.Content = "Общая сумма: " + allPrice;
        }

        private JournalWindow journalWindow;

        private void JournalButton_Click(object sender, RoutedEventArgs e)
        {
            if (journalWindow == null || !journalWindow.IsVisible)
            {
                journalWindow = new JournalWindow(this);
                journalWindow.Closed += (s, args) => journalWindow = null;
            }

            foreach (TypeOperationsWindow operation in Operations.Items)
            {
                string user = users.SelectedItem?.ToString() ?? usersName.Text;
                journalWindow.AddRecordFromMainWindow(operation, user);
            }

            this.Hide();
            journalWindow.Show();
        }

        public void LoadRecordForEditing(JournalRecord record)
        {

            usersName.Text = record.User;

            typeOperation.SelectedItem = record.OperationType;

            formats.SelectedItem = record.Format;

            if (record.Side == 1)
                TwoSides.IsChecked = false;
            else if (record.Side == 2)
                TwoSides.IsChecked = true;

            if (record.Color == "Ч/Б" || record.Color.Contains("Ч/Б"))
                Colors.IsChecked = false;
            else
                Colors.IsChecked = true;

            if (record.Color.Contains("> 50%") || record.Color.Contains("(> 50%)"))
                LotOfColor.IsChecked = true;
            else
                LotOfColor.IsChecked = false;

            textBoxCount.Text = record.Count.ToString();

            textBoxPrice.Text = record.Price.ToString();

            addOperationButton.Content = "Изменить";
        }


        private void AddAllToJournal_Click(object sender, RoutedEventArgs e)
        {
            if (Operations.Items.Count == 0)
            {
                MessageBox.Show("Нет операций для добавления в журнал!");
                return;
            }

            if (journalWindow != null)
            {

                foreach (TypeOperationsWindow operation in Operations.Items)
                {
                    string user = users.SelectedItem?.ToString() ?? usersName.Text;
                    journalWindow.AddRecordFromMainWindow(operation, user);
                }
                MessageBox.Show($"Добавлено {Operations.Items.Count} операций в журнал!");
            }
            else
            {
                MessageBox.Show("Сначала откройте журнал!");
            }
        }
    }
}

