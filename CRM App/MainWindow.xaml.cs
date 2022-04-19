using System;
using System.Collections.Generic;
using System.Linq;

using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Windows.Storage;
using Windows.Storage.Pickers;

using Backend;
using Backend.ViewModels;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CRM_App
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        MainViewModel _viewModel;

        public MainWindow()
        {
            this.InitializeComponent();
            _viewModel = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel));
            MainGrid.DataContext = _viewModel;
        }

        private async void MenuFlyoutExport_Click(object sender, RoutedEventArgs e)
        {
            var savePicker = new FileSavePicker();
            savePicker.DefaultFileExtension = ".csv";
            savePicker.SuggestedFileName = "crm-export.csv";
            savePicker.FileTypeChoices.Add("CSV UTF-8 (comma delimited)", new List<string>() { ".csv" });
            var hwnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);
            var stFile = await savePicker.PickSaveFileAsync();

            await FileIO.WriteTextAsync(stFile, _viewModel.Export());
        }

        private void DataGrid_Sorting(object sender, DataGridColumnEventArgs e)
        {
            var dataGrid = (DataGrid)sender;

            // Clear previous sorted column if we start sorting a different column
            string previousSortedColumn = _viewModel.SortedColumn;
            if (previousSortedColumn != string.Empty)
            {
                foreach (DataGridColumn dataGridColumn in dataGrid.Columns)
                {
                    if (dataGridColumn.Tag != null && dataGridColumn.Tag.ToString() == previousSortedColumn &&
                        (e.Column.Tag == null || previousSortedColumn != e.Column.Tag.ToString()))
                    {
                        dataGridColumn.SortDirection = null;
                    }
                }
            }

            // Toggle clicked column's sorting method
            if (e.Column.Tag != null)
            {
                if (e.Column.SortDirection == null || e.Column.SortDirection == DataGridSortDirection.Descending)
                {
                    _viewModel.SortCustomers(e.Column.Tag.ToString(), SortDirection.Ascending);
                    e.Column.SortDirection = DataGridSortDirection.Ascending;
                }
                else
                {
                    _viewModel.SortCustomers(e.Column.Tag.ToString(), SortDirection.Descending);
                    e.Column.SortDirection = DataGridSortDirection.Descending;
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            _viewModel.SelectCustomers(((DataGrid)sender).SelectedItems.Cast<CustomerOverview>());
        }
    }
}
