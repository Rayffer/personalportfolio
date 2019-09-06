using Rayffer.PersonalPortfolio.QueueManagers;
using Rayffer.PersonalPortfolio.Sorters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Media;

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
                selectionSortVisualisationActionQueueManager
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            performSortingsButton.IsEnabled = false;

            if (!int.TryParse(sortElementsTextbox.Text, out int sortElements))
                sortElements = 100;
            if (!int.TryParse(stepDelayTextbox.Text, out int stepDelay))
                stepDelay = 50;

            List<int> listToSort = new List<int>();
            for (int i = 0; i < sortElements; i++)
            {
                listToSort.Add(i + 1);
            }

            int[] arrayToSort = Shuffle(listToSort, new Random()).ToArray();

            StartCockTailSort(stepDelay, listToSort, arrayToSort);
            StartBubbleSort(stepDelay, listToSort, arrayToSort);
            StartInsertionSort(stepDelay, listToSort, arrayToSort);
            StartMergeSort(stepDelay, listToSort, arrayToSort);
            StartQuickSorting(stepDelay, listToSort, arrayToSort);
            StartSelectionSorting(stepDelay, listToSort, arrayToSort);
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

        private void StartCockTailSort(int stepDelay, List<int> listToSort, int[] arrayToSort)
        {
            CockTailSorter<int> cockTailSorter = new CockTailSorter<int>();
            bool cocktailSortHasEnded = false;

            cocktailSortActionQueueManager.EnqueueAction(() =>
            {
                cockTailSorter.SortAscending(arrayToSort.ToList().ToArray(), stepDelay / 10);
                cocktailSortHasEnded = true;
            });

            cocktailSortVisualisationActionQueueManager.EnqueueAction(() =>
            {
                Thread.Sleep(550);
                double maxHeight = cocktailSortStackPanelToDrawOn.ActualHeight;
                double maxWidth = cocktailSortStackPanelToDrawOn.ActualWidth;
                double tickWidth = maxWidth / listToSort.Count;
                while (!cocktailSortHasEnded)
                {
                    Thread.Sleep(10);
                    cocktailSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        cocktailSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * cockTailSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;

                                if (cockTailSorter.CurrentSortedListIndex == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (listToSort[j] == cockTailSorter.SortedList[j])
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                else
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        cocktailSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                for (int i = 0; i < listToSort.Count; i++)
                {
                    Thread.Sleep(2500 / listToSort.Count);

                    cocktailSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        cocktailSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * cockTailSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (i == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (i < j)
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                else
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        cocktailSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                cockTailSorter = null;
            });
        }

        private void StartBubbleSort(int stepDelay, List<int> listToSort, int[] arrayToSort)
        {
            BubbleSorter<int> bubbleSorter = new BubbleSorter<int>();
            bool bubbleSortHasEnded = false;

            bubbleSortActionQueueManager.EnqueueAction(() =>
            {
                bubbleSorter.SortAscending(arrayToSort.ToList().ToArray(), stepDelay / 10);
                bubbleSortHasEnded = true;
            });

            bubbleSortVisualisationActionQueueManager.EnqueueAction(() =>
            {
                Thread.Sleep(550);
                double maxHeight = bubbleSortStackPanelToDrawOn.ActualHeight;
                double maxWidth = bubbleSortStackPanelToDrawOn.ActualWidth;
                double tickWidth = maxWidth / listToSort.Count;
                while (!bubbleSortHasEnded)
                {
                    Thread.Sleep(10);
                    bubbleSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        bubbleSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * bubbleSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (bubbleSorter.CurrentSortedListIndex == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (listToSort[j] == bubbleSorter.SortedList[j])
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                else
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        bubbleSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                for (int i = 0; i < listToSort.Count; i++)
                {
                    Thread.Sleep(2500 / listToSort.Count);

                    bubbleSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        bubbleSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * bubbleSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (i == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (i < j)
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                else
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        bubbleSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                bubbleSorter = null;
            });
        }

        private void StartInsertionSort(int stepDelay, List<int> listToSort, int[] arrayToSort)
        {
            InsertionSorter<int> insertionSorter = new InsertionSorter<int>();
            bool insertionSortHasEnded = false;

            insertionSortActionQueueManager.EnqueueAction(() =>
            {
                insertionSorter.SortAscending(arrayToSort.ToList().ToArray(), stepDelay);
                insertionSortHasEnded = true;
            });

            insertionSortVisualisationActionQueueManager.EnqueueAction(() =>
            {
                Thread.Sleep(550);
                double maxHeight = insertionSortStackPanelToDrawOn.ActualHeight;
                double maxWidth = insertionSortStackPanelToDrawOn.ActualWidth;
                double tickWidth = maxWidth / listToSort.Count;

                while (!insertionSortHasEnded)
                {
                    Thread.Sleep(10);
                    insertionSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        insertionSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * insertionSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (insertionSorter.CurrentSortedListIndex == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (listToSort[j] == insertionSorter.SortedList[j])
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                else
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        insertionSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                for (int i = 0; i < listToSort.Count; i++)
                {
                    Thread.Sleep(2500 / listToSort.Count);

                    insertionSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        insertionSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * insertionSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (i == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (i < j)
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                else
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        insertionSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                insertionSorter = null;
            });
        }

        private void StartMergeSort(int stepDelay, List<int> listToSort, int[] arrayToSort)
        {
            MergeSorter<int> mergeSorter = new MergeSorter<int>();
            bool mergeSortHasEnded = false;

            mergeSortActionQueueManager.EnqueueAction(() =>
            {
                mergeSorter.SortAscending(arrayToSort.ToList().ToArray(), stepDelay);
                mergeSortHasEnded = true;
            });

            mergeSortVisualisationActionQueueManager.EnqueueAction(() =>
            {
                Thread.Sleep(550);
                double maxHeight = mergeSortStackPanelToDrawOn.ActualHeight;
                double maxWidth = mergeSortStackPanelToDrawOn.ActualWidth;
                double tickWidth = maxWidth / listToSort.Count;

                while (!mergeSortHasEnded)
                {
                    Thread.Sleep(10);
                    mergeSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        mergeSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * mergeSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (mergeSorter.CurrentSortedListIndex == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (listToSort[j] == mergeSorter.SortedList[j])
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                else
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        mergeSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                for (int i = 0; i < listToSort.Count; i++)
                {
                    Thread.Sleep(2500 / listToSort.Count);

                    mergeSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        mergeSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * mergeSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (i == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (i < j)
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                else
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        mergeSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                mergeSorter = null;
            });
        }

        private void StartQuickSorting(int stepDelay, List<int> listToSort, int[] arrayToSort)
        {
            QuickSorter<int> quickSorter = new QuickSorter<int>(Sorters.Types.QuickSortPivotTypes.RandomPivot);
            bool quickSorterHasEnded = false;

            quickSortActionQueueManager.EnqueueAction(() =>
            {
                quickSorter.SortAscending(arrayToSort.ToList().ToArray(), stepDelay);
                quickSorterHasEnded = true;
            });

            quickSortVisualisationActionQueueManager.EnqueueAction(() =>
            {
                Thread.Sleep(50);
                double maxHeight = quickSortStackPanelToDrawOn.ActualHeight;
                double maxWidth = quickSortStackPanelToDrawOn.ActualWidth;
                double tickWidth = maxWidth / listToSort.Count;

                while (!quickSorterHasEnded)
                {
                    Thread.Sleep(10);
                    quickSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        quickSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * quickSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (quickSorter.CurrentSortedListIndex == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (listToSort[j] == quickSorter.SortedList[j])
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                else
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        quickSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                for (int i = 0; i < listToSort.Count; i++)
                {
                    Thread.Sleep(2500 / listToSort.Count);

                    quickSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        quickSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * quickSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (i == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (i < j)
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                else
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        quickSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                quickSorter = null;
            });
        }

        private void StartSelectionSorting(int stepDelay, List<int> listToSort, int[] arrayToSort)
        {
            SelectionSorter<int> selectionSorter = new SelectionSorter<int>();
            bool selectionSorterHasEnded = false;

            selectionSortActionQueueManager.EnqueueAction(() =>
            {
                selectionSorter.SortAscending(arrayToSort.ToList().ToArray(), stepDelay);
                selectionSorterHasEnded = true;
            });

            selectionSortVisualisationActionQueueManager.EnqueueAction(() =>
            {
                Thread.Sleep(50);
                double maxHeight = selectionSortStackPanelToDrawOn.ActualHeight;
                double maxWidth = selectionSortStackPanelToDrawOn.ActualWidth;
                double tickWidth = maxWidth / listToSort.Count;

                while (!selectionSorterHasEnded)
                {
                    Thread.Sleep(10);
                    selectionSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        selectionSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * selectionSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (selectionSorter.CurrentSortedListIndex == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (listToSort[j] == selectionSorter.SortedList[j])
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                else
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        selectionSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                for (int i = 0; i < listToSort.Count; i++)
                {
                    Thread.Sleep(2500 / listToSort.Count);

                    selectionSortStackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        selectionSortStackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < listToSort.Count; j++)
                            {
                                double pieceHeight = maxHeight * selectionSorter.SortedList[j] / arrayToSort.Length;

                                Brush colorBrush = null;
                                if (i == j)
                                {
                                    colorBrush = Brushes.OrangeRed;
                                }
                                else if (i < j)
                                {
                                    colorBrush = Brushes.Maroon;
                                }
                                else
                                {
                                    colorBrush = Brushes.Aquamarine;
                                }
                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(colorBrush, null, new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        selectionSortStackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
                selectionSorter = null;
            });
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
                stepDelayTextbox.Text = "800";
            }
        }
    }
}