using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Print_Management_System.Classes;

namespace Print_Management_System
{
    public partial class JournalWindow : Window
    {
        private List<JournalRecord> allRecords = new List<JournalRecord>();
        private MainWindow mainWindow;

        public JournalWindow(MainWindow mainWindow = null)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            UpdateAllTabs();
        }

        private void UpdateAllTabs()
        {
            UpdateTab(todayListView, GetTodayRecords());
            UpdateTab(yesterdayListView, GetYesterdayRecords());
            UpdateTab(weekListView, GetWeekRecords());
            UpdateTab(monthListView, GetMonthRecords());
            UpdateTab(quarterListView, GetQuarterRecords());
            UpdateTab(yearListView, GetYearRecords());
        }

        private void UpdateTab(ListView listView, List<JournalRecord> records)
        {
            listView.ItemsSource = null;
            listView.ItemsSource = records;
        }

        private List<JournalRecord> GetTodayRecords()
        {
            return allRecords.Where(r => r.Date.Date == DateTime.Today).ToList();
        }

        private List<JournalRecord> GetYesterdayRecords()
        {
            return allRecords.Where(r => r.Date.Date == DateTime.Today.AddDays(-1)).ToList();
        }

        private List<JournalRecord> GetWeekRecords()
        {
            DateTime weekStart = DateTime.Today.AddDays(-7);
            return allRecords.Where(r => r.Date >= weekStart).ToList();
        }

        private List<JournalRecord> GetMonthRecords()
        {
            DateTime monthStart = DateTime.Today.AddMonths(-1);
            return allRecords.Where(r => r.Date >= monthStart).ToList();
        }

        private List<JournalRecord> GetQuarterRecords()
        {
            DateTime quarterStart = DateTime.Today.AddMonths(-3);
            return allRecords.Where(r => r.Date >= quarterStart).ToList();
        }

        private List<JournalRecord> GetYearRecords()
        {
            DateTime yearStart = DateTime.Today.AddYears(-1);
            return allRecords.Where(r => r.Date >= yearStart).ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow != null)
            {
                mainWindow.Show();
                mainWindow.Focus();
                this.Hide();
            }
            else
            {
                new MainWindow().Show();
                this.Close();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ListView currentListView = GetActiveListView();

            if (currentListView != null && currentListView.SelectedItem != null)
            {
                JournalRecord selectedRecord = currentListView.SelectedItem as JournalRecord;

                if (selectedRecord != null)
                {
                    if (mainWindow == null)
                    {
                        mainWindow = new MainWindow();
                    }

                    mainWindow.LoadRecordForEditing(selectedRecord);

                    mainWindow.Show();
                    mainWindow.Focus();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования",
                              "Внимание",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ListView currentListView = GetActiveListView();

            if (currentListView != null && currentListView.SelectedItem != null)
            {
                JournalRecord selectedRecord = currentListView.SelectedItem as JournalRecord;

                if (selectedRecord != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"Удалить операцию для {selectedRecord.User}?",
                        "Подтверждение удаления",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        allRecords.Remove(selectedRecord);
                        UpdateAllTabs();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления",
                              "Внимание",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
            }
        }

        private ListView GetActiveListView()
        {
            if (periodTabControl.SelectedItem is TabItem selectedTab)
            {
                return selectedTab.Content as ListView;
            }
            return null;
        }

        public void AddRecordFromMainWindow(TypeOperationsWindow operation, string user)
        {
            JournalRecord newRecord = operation.ToJournalRecord(user);
            allRecords.Add(newRecord);
            allRecords = allRecords.OrderByDescending(r => r.Date).ToList();
            UpdateAllTabs();
        }
    }
}