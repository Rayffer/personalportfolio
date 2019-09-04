using Rayffer.PersonalPortfolio.QueueManagers;
using Rayffer.PersonalPortfolio.Sorters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace Rayffer.PersonalPortfolio.TestWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorkerActionQueueManager sortActionQueueManager;
        private readonly BackgroundWorkerActionQueueManager sortVisualisationActionQueueManager;

        public MainWindow()
        {
            InitializeComponent();
            sortActionQueueManager = new BackgroundWorkerActionQueueManager();
            sortVisualisationActionQueueManager = new BackgroundWorkerActionQueueManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<int> listToSort = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                listToSort.Add(i + 1);
            }

            int[] arrayToSort = Shuffle(listToSort, new Random()).ToArray();
            CockTailSorter<int> cockTailSorter = new CockTailSorter<int>();
            bool hasEnded = false;
            sortActionQueueManager.EnqueueAction(() =>
            {
                cockTailSorter.SortAscending(arrayToSort, 100);
                hasEnded = true;
            });

            sortVisualisationActionQueueManager.EnqueueAction(() =>
            {
                Thread.Sleep(50);
                double maxHeight = stackPanelToDrawOn.ActualHeight;
                double maxWidth = stackPanelToDrawOn.ActualWidth;
                double tickWidth = maxWidth / 100;
                while (!hasEnded)
                {
                    stackPanelToDrawOn.Dispatcher.Invoke(() =>
                    {
                        stackPanelToDrawOn.Background = null;
                        DrawingVisual drawingVisual = new DrawingVisual();
                        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                        {
                            drawingContext.DrawRectangle(Brushes.LightGray, new Pen(Brushes.Black, 0.5F), new Rect(0, 0, maxWidth, maxHeight));
                            for (int j = 0; j < 100; j++)
                            {
                                double pieceHeight = maxHeight * cockTailSorter.sortedList[j] / arrayToSort.Length;

                                double pieceY = maxHeight - pieceHeight;
                                double pieceX = tickWidth * j;

                                drawingContext.DrawRectangle(Brushes.Aquamarine, new Pen(Brushes.Black, 0.5F), new Rect(pieceX, pieceY, tickWidth, pieceHeight));
                            }
                            drawingContext.Close();
                        }
                        stackPanelToDrawOn.Background = new DrawingBrush(drawingVisual.Drawing);
                    });
                }
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
    }
}