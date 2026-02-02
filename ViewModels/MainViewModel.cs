using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AreaFilter.Commands;
using AreaFilter.Enums;
using AreaFilter.Models;

namespace AreaFilter.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private CriteriaType _selectedCriteria;
        private RuleType _selectedRule;
        private double _lowSpec = 1;
        private double _highSpec = 1;

        public MainViewModel()
        {
            CriteriaItems = new ObservableCollection<CriteriaItem>();
            AddNewCommand = new RelayCommand(AddNew);
            DeleteCommand = new RelayCommand(Delete);
            
            _selectedCriteria = CriteriaType.Area;
            _selectedRule = RuleType.BiggerThen;
        }

        public ObservableCollection<CriteriaItem> CriteriaItems { get; set; }

        public Array CriteriaTypes => Enum.GetValues(typeof(CriteriaType));
        public Array RuleTypes => Enum.GetValues(typeof(RuleType));

        public CriteriaType SelectedCriteria
        {
            get => _selectedCriteria;
            set
            {
                _selectedCriteria = value;
                OnPropertyChanged();
            }
        }

        public RuleType SelectedRule
        {
            get => _selectedRule;
            set
            {
                _selectedRule = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLowSpecVisible));
                OnPropertyChanged(nameof(IsHighSpecVisible));
            }
        }

        public double LowSpec
        {
            get => _lowSpec;
            set
            {
                if (value > 0)
                {
                    _lowSpec = value;
                    OnPropertyChanged();
                }
            }
        }

        public double HighSpec
        {
            get => _highSpec;
            set
            {
                if (value > 0)
                {
                    _highSpec = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLowSpecVisible
        {
            get
            {
                return SelectedRule == RuleType.LessThen || 
                       SelectedRule == RuleType.InBetween || 
                       SelectedRule == RuleType.OutOfSpec;
            }
        }

        public bool IsHighSpecVisible
        {
            get
            {
                return SelectedRule == RuleType.BiggerThen || 
                       SelectedRule == RuleType.InBetween || 
                       SelectedRule == RuleType.OutOfSpec;
            }
        }

        public ICommand AddNewCommand { get; }
        public ICommand DeleteCommand { get; }

        private void AddNew(object parameter)
        {
            var newItem = new CriteriaItem
            {
                Order = CriteriaItems.Count + 1,
                Criteria = SelectedCriteria,
                Rule = SelectedRule,
                LowSpec = LowSpec,
                HighSpec = HighSpec
            };

            CriteriaItems.Add(newItem);
        }

        private void Delete(object parameter)
        {
            if (parameter is CriteriaItem item)
            {
                CriteriaItems.Remove(item);
                ReorderItems();
            }
        }

        private void ReorderItems()
        {
            for (int i = 0; i < CriteriaItems.Count; i++)
            {
                CriteriaItems[i].Order = i + 1;
            }
        }

        public void MoveItem(int oldIndex, int newIndex)
        {
            if (oldIndex < 0 || oldIndex >= CriteriaItems.Count ||
                newIndex < 0 || newIndex >= CriteriaItems.Count)
                return;

            var item = CriteriaItems[oldIndex];
            CriteriaItems.RemoveAt(oldIndex);
            CriteriaItems.Insert(newIndex, item);
            ReorderItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
