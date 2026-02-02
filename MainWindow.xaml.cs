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
using AreaFilter.Models;
using AreaFilter.ViewModels;

namespace AreaFilter
{
    public partial class MainWindow : Window
    {
        private int _draggedIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                _draggedIndex = row.GetIndex();
                if (_draggedIndex >= 0)
                {
                    DragDrop.DoDragDrop(row, row.DataContext, DragDropEffects.Move);
                }
            }
        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            if (_draggedIndex < 0) return;

            var target = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);
            if (target != null)
            {
                int targetIndex = target.GetIndex();
                if (targetIndex >= 0 && targetIndex != _draggedIndex)
                {
                    var viewModel = DataContext as MainViewModel;
                    viewModel?.MoveItem(_draggedIndex, targetIndex);
                }
            }
            _draggedIndex = -1;
        }

        private void DataGridRow_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is DataGridRow row && row.DataContext is CriteriaItem item)
            {
                item.IsHovered = true;
            }
        }

        private void DataGridRow_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is DataGridRow row && row.DataContext is CriteriaItem item)
            {
                item.IsHovered = false;
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T ancestor)
                {
                    return ancestor;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }
    }
}
