using System;
using System.Windows;
using System.Windows.Controls;

namespace AreaFilter.Controls
{
    public partial class NumericUpDown : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(0.1));

        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(1.0));

        public NumericUpDown()
        {
            InitializeComponent();
            UpButton.Click += UpButton_Click;
            DownButton.Click += DownButton_Click;
            ValueTextBox.TextChanged += ValueTextBox_TextChanged;
            ValueTextBox.LostFocus += ValueTextBox_LostFocus;
            UpdateTextBox();
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NumericUpDown control)
            {
                control.UpdateTextBox();
            }
        }

        private void UpdateTextBox()
        {
            ValueTextBox.TextChanged -= ValueTextBox_TextChanged;
            ValueTextBox.Text = Value.ToString("F2");
            ValueTextBox.TextChanged += ValueTextBox_TextChanged;
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            Value += Increment;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            var newValue = Value - Increment;
            if (newValue >= Minimum)
            {
                Value = newValue;
            }
        }

        private void ValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(ValueTextBox.Text, out double result))
            {
                if (result >= Minimum)
                {
                    Value = result;
                }
            }
        }

        private void ValueTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(ValueTextBox.Text, out double result) || result < Minimum)
            {
                UpdateTextBox();
            }
        }
    }
}
