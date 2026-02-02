using System.ComponentModel;
using System.Runtime.CompilerServices;
using AreaFilter.Enums;

namespace AreaFilter.Models
{
    public class CriteriaItem : INotifyPropertyChanged
    {
        private int _order;
        private CriteriaType _criteria;
        private RuleType _rule;
        private double _lowSpec;
        private double _highSpec;
        private bool _isHovered;

        public int Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged();
            }
        }

        public CriteriaType Criteria
        {
            get => _criteria;
            set
            {
                _criteria = value;
                OnPropertyChanged();
            }
        }

        public RuleType Rule
        {
            get => _rule;
            set
            {
                _rule = value;
                OnPropertyChanged();
            }
        }

        public double LowSpec
        {
            get => _lowSpec;
            set
            {
                _lowSpec = value;
                OnPropertyChanged();
            }
        }

        public double HighSpec
        {
            get => _highSpec;
            set
            {
                _highSpec = value;
                OnPropertyChanged();
            }
        }

        public bool IsHovered
        {
            get => _isHovered;
            set
            {
                _isHovered = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
