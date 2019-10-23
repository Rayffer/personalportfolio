using Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Rayffer.PersonalPortfolio.HttpRequestViewer.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<RequestSnifferControl> requestSnifferControls;
        private Regex excludedDirectoriesRegex;

        public MainWindow()
        {
            InitializeComponent();
            requestSnifferControls = new List<RequestSnifferControl>() { };
            excludedDirectoriesRegex = new Regex("([a-z])|([A-Z])|([0-9])");
            string pathDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Sniffers");
            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }
            foreach (var sniffer in Directory.GetDirectories(pathDirectory))
            {
                var snifferDirectory = Path.GetFileName(sniffer);
                TabItem tab = new TabItem();
                tab.Header = snifferDirectory;
                tab.Background = Brushes.White;
                RequestSnifferControl control = new RequestSnifferControl(snifferDirectory);
                tab.Content = control;
                requestSnifferControls.Add(control);
                mainTabControl.Items.Add(tab);
            }
            mainTabControl.SelectedIndex = 0;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            requestSnifferControls.AsParallel().ForAll(sniffer =>
            {
                sniffer.ControlShutdown();
            });
        }

        private void SnifferNameTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !excludedDirectoriesRegex.IsMatch(e.Text);
        }

        private void RemoveSnifferButton_Click(object sender, RoutedEventArgs e)
        {
            var tabItemToRemove = mainTabControl.SelectedItem as TabItem;
            mainTabControl.Items.Remove(tabItemToRemove);
            var snifferToRemove = requestSnifferControls.FirstOrDefault(sniffer => sniffer.Name.Equals(tabItemToRemove.Header.ToString()));
            snifferToRemove?.ControlShutdown();
        }

        private void AddSnifferButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(snifferNameTextBox.Text))
            {
                MessageBox.Show("Please specify a name for the sniffer");
                return;
            }
            var tabs = FindVisualChildren<TabItem>(mainTabControl);
            if (tabs.Any(searchingTab => searchingTab.Header.Equals(snifferNameTextBox.Text)))
            {
                MessageBox.Show("A tab with the same name already exists");
                return;
            }

            TabItem tab = new TabItem();
            tab.Header = snifferNameTextBox.Text;
            tab.Background = Brushes.White;
            RequestSnifferControl control = new RequestSnifferControl(snifferNameTextBox.Text);
            tab.Content = control;
            requestSnifferControls.Add(control);
            mainTabControl.Items.Add(tab);
            mainTabControl.SelectedItem = tab;
        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj != null)
            {
                if (obj is T)
                    yield return obj as T;

                foreach (DependencyObject child in LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>())
                    foreach (T c in FindVisualChildren<T>(child))
                        yield return c;
            }
        }

        private void DeleteSnifferButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("This will delete the sniffer directory and any response body will be lost, do you want to do this?", "Confirm sniffer deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                TabItem tabItemToDelete = mainTabControl.SelectedItem as TabItem;
                var snifferToDelete = requestSnifferControls.FirstOrDefault(sniffer => sniffer.Name.Equals(tabItemToDelete.Header.ToString()));
                snifferToDelete?.ControlShutdown();
                DeleteDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Sniffers", tabItemToDelete.Header.ToString()));
                mainTabControl.Items.Remove(tabItemToDelete);
                requestSnifferControls.Remove(snifferToDelete);
            }
        }

        public void DeleteDirectory(string targetDir)
        {
            File.SetAttributes(targetDir, FileAttributes.Normal);

            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir, false);
        }
    }
}