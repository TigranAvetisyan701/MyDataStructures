using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SetProject;
using SetDemo;

namespace MySetWPF
{
    public partial class MainWindow : Window
    {
        MySet<Student> _men = new MySet<Student>();
        MySet<Student> _women = new MySet<Student>();
        MySet<Student> _reading = new MySet<Student>();
        MySet<Student> _writing = new MySet<Student>();
        MySet<Student> _arithmetic = new MySet<Student>();

        Dictionary<string, MySet<Student>> allSets = new Dictionary<string, MySet<Student>>();

        public MainWindow()
        {
            InitializeComponent();

            Student james = new Student(1, "James", Gender.Male);
            Student robert = new Student(2, "Robert", Gender.Male);
            Student john = new Student(3, "John", Gender.Male);
            Student mark = new Student(4, "Mark", Gender.Male);
            Student otherMark = new Student(5, "Mark", Gender.Male);
            _men.AddRange(new Student[] { james, robert, john, mark, otherMark });

            Student liz = new Student(6, "Elizabeth", Gender.Female);
            Student amy = new Student(7, "Amy", Gender.Female);
            Student eve = new Student(8, "Evelyn", Gender.Female);
            _women.AddRange(new Student[] { liz, amy, eve });

            _reading.AddRange(new Student[] { james, robert, liz });
            _writing.AddRange(new Student[] { robert, mark, amy, eve, liz });
            _arithmetic.AddRange(new Student[] { john, mark, otherMark, amy });

            allSets.Add("Men", _men);
            allSets.Add("Women", _women);
            allSets.Add("Reading", _reading);
            allSets.Add("Writing", _writing);
            allSets.Add("Arithmetic", _arithmetic);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string name in allSets.Keys)
            {
                leftSet.Items.Add(name);
                rightSet.Items.Add(name);
            }

            operation.Items.Add("UNION");
            operation.Items.Add("INTERSECTION");
            operation.Items.Add("DIFFERENCE");
            operation.Items.Add("SYMETRIC DIFF");
        }

        private void Set_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox combo) return;
            if (e.AddedItems.Count == 0) return;

            var selectedName = e.AddedItems[0] as string;
            if (selectedName == null) return;

            if (!allSets.TryGetValue(selectedName, out var selectedSet)) return;

            if (combo.Name == "leftSet")
            {
                leftMembers.Items.Clear();
                foreach (var s in selectedSet)
                    leftMembers.Items.Add(s.Name);
            }
            else if (combo.Name == "rightSet")
            {
                rightMembers.Items.Clear();
                foreach (var s in selectedSet)
                    rightMembers.Items.Add(s.Name);
            }
        }

        private void evaluateButton_Click(object sender, RoutedEventArgs e)
        {
            var leftName = leftSet.SelectedItem as string;
            var rightName = rightSet.SelectedItem as string;
            var op = operation.SelectedItem as string;

            if (leftName == null || rightName == null || op == null)
            {
                MessageBox.Show("Please select both sets and an operation.");
                return;
            }

            if (!allSets.TryGetValue(leftName, out var left) ||
                !allSets.TryGetValue(rightName, out var right))
            {
                MessageBox.Show("Invalid selection.");
                return;
            }

            MySet<Student> result = op switch
            {
                "UNION" => left.Union(right),
                "INTERSECTION" => left.Intersection(right),
                "DIFFERENCE" => left.Difference(right),
                "SYMETRIC DIFF" => left.SymmetricDifference(right),
                _ => new MySet<Student>()
            };

            resultSet.Items.Clear();
            foreach (var s in result)
                resultSet.Items.Add(s.Name);
        }
    }
}
