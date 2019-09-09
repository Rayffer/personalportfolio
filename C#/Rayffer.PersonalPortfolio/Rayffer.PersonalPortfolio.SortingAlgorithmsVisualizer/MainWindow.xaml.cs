using Rayffer.PersonalPortfolio.QueueManagers;
using Rayffer.PersonalPortfolio.Sorters;
using Rayffer.PersonalPortfolio.Sorters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Rayffer.PersonalPortfolio.SortingAlgorithmsVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Regex numericRegex = new Regex("^\\d{0,}$");

        private BackgroundWorkerActionQueueManager cocktailSortActionQueueManager;
        private BackgroundWorkerActionQueueManager cocktailSortVisualisationActionQueueManager;
        private BackgroundWorkerActionQueueManager bubbleSortActionQueueManager;
        private BackgroundWorkerActionQueueManager bubbleSortVisualisationActionQueueManager;
        private BackgroundWorkerActionQueueManager insertionSortActionQueueManager;
        private BackgroundWorkerActionQueueManager insertionSortVisualisationActionQueueManager;
        private BackgroundWorkerActionQueueManager mergeSortActionQueueManager;
        private BackgroundWorkerActionQueueManager mergeSortVisualisationActionQueueManager;
        private BackgroundWorkerActionQueueManager quickSortActionQueueManager;
        private BackgroundWorkerActionQueueManager quickSortVisualisationActionQueueManager;
        private BackgroundWorkerActionQueueManager selectionSortActionQueueManager;
        private BackgroundWorkerActionQueueManager selectionSortVisualisationActionQueueManager;
        private BackgroundWorkerActionQueueManager gnomeSortActionQueueManager;
        private BackgroundWorkerActionQueueManager gnomeSortVisualisationActionQueueManager;
        private BackgroundWorkerActionQueueManager uiActionQueueManager;

        private List<BackgroundWorkerActionQueueManager> sortingBackGroundWorkers;

        public MainWindow()
        {
            InitializeComponent();

            cocktailSortActionQueueManager = new BackgroundWorkerActionQueueManager();
            cocktailSortVisualisationActionQueueManager = new BackgroundWorkerActionQueueManager();
            bubbleSortActionQueueManager = new BackgroundWorkerActionQueueManager();
            bubbleSortVisualisationActionQueueManager = new BackgroundWorkerActionQueueManager();
            insertionSortActionQueueManager = new BackgroundWorkerActionQueueManager();
            insertionSortVisualisationActionQueueManager = new BackgroundWorkerActionQueueManager();
            mergeSortActionQueueManager = new BackgroundWorkerActionQueueManager();
            mergeSortVisualisationActionQueueManager = new BackgroundWorkerActionQueueManager();
            quickSortActionQueueManager = new BackgroundWorkerActionQueueManager();
            quickSortVisualisationActionQueueManager = new BackgroundWorkerActionQueueManager();
            selectionSortActionQueueManager = new BackgroundWorkerActionQueueManager();
            selectionSortVisualisationActionQueueManager = new BackgroundWorkerActionQueueManager();
            gnomeSortActionQueueManager = new BackgroundWorkerActionQueueManager();
            gnomeSortVisualisationActionQueueManager = new BackgroundWorkerActionQueueManager();
            uiActionQueueManager = new BackgroundWorkerActionQueueManager();

            sortingBackGroundWorkers = new List<BackgroundWorkerActionQueueManager>()
            {
                cocktailSortActionQueueManager,
                cocktailSortVisualisationActionQueueManager,
                bubbleSortActionQueueManager,
                bubbleSortVisualisationActionQueueManager,
                insertionSortActionQueueManager,
                insertionSortVisualisationActionQueueManager,
                mergeSortActionQueueManager,
                mergeSortVisualisationActionQueueManager,
                quickSortActionQueueManager,
                quickSortVisualisationActionQueueManager,
                selectionSortActionQueueManager,
                selectionSortVisualisationActionQueueManager,
                gnomeSortActionQueueManager,
                gnomeSortVisualisationActionQueueManager
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            performSortingsButton.IsEnabled = false;

            if (!int.TryParse(sortElementsTextbox.Text, out int sortElements))
                sortElements = 100;
            if (!int.TryParse(stepDelayTextbox.Text, out int stepDelay))
                stepDelay = 50;

            List<int> listToSort = Enumerable.Range(1, sortElements).ToList();

            int[] arrayToSort = Shuffle(listToSort, new Random()).ToArray();

            //StartSort(new CockTailSorter<int>(), stepDelay / 10, listToSort, arrayToSort, cocktailSortActionQueueManager, cocktailSortVisualisationActionQueueManager, cocktailSortStackPanelToDrawOn);
            //StartSort(new BubbleSorter<int>(), stepDelay / 10, listToSort, arrayToSort, bubbleSortActionQueueManager, bubbleSortVisualisationActionQueueManager, bubbleSortStackPanelToDrawOn);
            //StartSort(new GnomeSorter<int>(), stepDelay / 10, listToSort, arrayToSort, gnomeSortActionQueueManager, gnomeSortVisualisationActionQueueManager, gnomeSortStackPanelToDrawOn);
            //StartSort(new InsertionSorter<int>(), stepDelay, listToSort, arrayToSort, insertionSortActionQueueManager, insertionSortVisualisationActionQueueManager, insertionSortStackPanelToDrawOn);
            StartSort(new MergeSorter<int>(), stepDelay, listToSort, arrayToSort, mergeSortActionQueueManager, mergeSortVisualisationActionQueueManager, mergeSortStackPanelToDrawOn);
            StartSort(new QuickSorter<int>(Sorters.Types.QuickSortPivotTypes.LeftmostPivot),
                stepDelay, listToSort, arrayToSort, quickSortActionQueueManager, quickSortVisualisationActionQueueManager, quickSortStackPanelToDrawOn);
            StartSort(new SelectionSorter<int>(), stepDelay, listToSort, arrayToSort, selectionSortActionQueueManager, selectionSortVisualisationActionQueueManager, selectionSortStackPanelToDrawOn);

            uiActionQueueManager.EnqueueAction(() =>
            {
                while (sortingBackGroundWorkers.Any(backGroundWorker => backGroundWorker.IsBusy))
                {
                    Thread.Sleep(200);
                }
            });

            uiActionQueueManager.EnqueueAction(() =>
            {
                performSortingsButton.Dispatcher.Invoke(() =>
                {
                    performSortingsButton.IsEnabled = true;
                });
            });
        }

        private void StartSort(
            ISorter<int> sorter,
            int stepDelay,
            List<int> listToSort,
            int[] arrayToSort,
            BackgroundWorkerActionQueueManager sorterActionQueueManager,
            BackgroundWorkerActionQueueManager sorterVisualizerActionQueueManager,
            StackPanel visualisationStackPanel)
        {
            int sortVisualizationDelay = Math.Max(17, (int)(10 * listToSort.Count() / 300F));
            bool sorterHasEnded = false;
            int maxListValue = arrayToSort.Max();

            sorterActionQueueManager.EnqueueAction(() =>
            {
                sorter.SortAscending(arrayToSort.ToList().ToArray(), stepDelay);
                sorterHasEnded = true;
            });

            sorterVisualizerActionQueueManager.EnqueueAction(() =>
            {
                Thread.Sleep(50);
                double maxHeight = visualisationStackPanel.ActualHeight;
                double maxWidth = visualisationStackPanel.ActualWidth;
                double tickWidth = maxWidth / listToSort.Count;

                Semaphore semaphore = new Semaphore(1, 1);
                while (!sorterHasEnded)
                {
                    semaphore.WaitOne();
                    visualisationStackPanel.Dispatcher.Invoke(() =>
                    {
                        visualisationStackPanel.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                Brush colorBrush = GetSortingColorBrush(sorter, listToSort, j);

                                double pieceHeight = maxHeight * sorter.SortedList[j] / (float)maxListValue;
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;
                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        visualisationStackPanel.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                    visualisationStackPanel.Dispatcher.BeginInvoke(new Action(() => semaphore.Release()), DispatcherPriority.ContextIdle, null);
                }
                DateTime endingVisualisationStart = DateTime.Now;
                var timeBeforeSemaphore = DateTime.Now;
                for (int i = 0; i < listToSort.Count; i++)
                {
                    semaphore.WaitOne();
                    var timeAfterSemaphore = DateTime.Now;

                    double sempahoreWaitMiliseconds = (timeAfterSemaphore - timeBeforeSemaphore).TotalMilliseconds;

                    if (sempahoreWaitMiliseconds < (2500 / listToSort.Count()))
                    {
                        Thread.Sleep((int)(2500 / listToSort.Count() - sempahoreWaitMiliseconds));
                    }
                    else
                    {
                        var stepsToSkip = Math.Ceiling(sempahoreWaitMiliseconds / 2500 / listToSort.Count());
                        i += (int)stepsToSkip;
                    }

                    visualisationStackPanel.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        visualisationStackPanel.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                Brush colorBrush = GetCompletedSortingBrush(i, j);

                                double pieceHeight = maxHeight * sorter.SortedList[j] / maxListValue;
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;
                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        timeBeforeSemaphore = DateTime.Now;
                        visualisationStackPanel.Background = new DrawingBrush(drawingVisual.Drawing);
                    }), DispatcherPriority.Send);

                    visualisationStackPanel.Dispatcher.Invoke(new Action(() => semaphore.Release()), DispatcherPriority.ContextIdle, null);
                }
                sorter = null;
                DateTime endingVisualisationEnd = DateTime.Now;
            });
        }

        private Brush GetCompletedSortingBrush(int i, int j)
        {
            Brush colorBrush;
            if (i == j)
            {
                colorBrush = Brushes.OrangeRed;
            }
            else if (i > j)
            {
                colorBrush = Brushes.Aquamarine;
            }
            else
            {
                colorBrush = Brushes.Maroon;
            }

            return colorBrush;
        }

        private Brush GetSortingColorBrush(ISorter<int> sorter, List<int> listToSort, int j)
        {
            Brush colorBrush;
            if (sorter.CurrentSortedListIndex == j)
            {
                colorBrush = Brushes.OrangeRed;
            }
            else if (listToSort[j].CompareTo(sorter.SortedList[j]) == 0)
            {
                colorBrush = Brushes.Aquamarine;
            }
            else
            {
                colorBrush = Brushes.Maroon;
            }

            return colorBrush;
        }

        public IEnumerable<T> Shuffle<T>(IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            // Note i > 0 to avoid final pointless iteration
            for (int i = elements.Length - 1; i > 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                int swapIndex = rng.Next(i + 1);
                T tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }

            // Lazily yield (avoiding aliasing issues etc)
            return elements;
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !numericRegex.IsMatch(e.TextComposition.Text);
        }

        private void StepDelayTextbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (int.TryParse(stepDelayTextbox.Text, out int stepDelay)
                && stepDelay > 500)
            {
                stepDelayTextbox.Text = "500";
            }
        }

        private void SortElementsTextbox_TextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (int.TryParse(sortElementsTextbox.Text, out int sortElements)
                && sortElements > 800)
            {
                sortElementsTextbox.Text = "800";
            }
        }
    }
}