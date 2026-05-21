using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SetDemo;
using SetProject;

namespace MySetWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySet<Student> _men = new MySet<Student>();
        MySet<Student> _women = new MySet<Student>();

        MySet<Student> _reading = new MySet<Student>();
        MySet<Student> _arithmetic = new MySet<Student>();
        MySet<Student> _writing = new MySet<Student>();

        Dictionary<string, MySet<Student>> allSets = new Dictionary<string, MySet<Student>>();

        public MainWindow()
        {
            Student james = new Student(1, "James", Gender.Male);
            Student robert = new Student(2, "Robert", Gender.Male);
            Student john = new Student(3, "John", Gender.Male);
            Student mark = new Student(4, "Mark", Gender.Male);
            Student otherMark = new Student(5, "Mark", Gender.Male);
            _men.AddRange(new Student[] { james, robert, john, mark, otherMark });

            Student liz = new Student(6, "Elizabeth", Gender.Female);
            Student amy = new Student(7, "Amy", Gender.Female);
            Student eve = new Student(8, "Evelyn", Gender.Female);
            _women.AddRange(new Student[] {liz, amy, eve });

            _reading.AddRange(new Student[] { james, robert, liz });
            _writing.AddRange(new Student[] { robert, mark, amy, eve, liz });
            _arithmetic.AddRange(new Student[] { john, mark, otherMark, amy });

            allSets.Add("Men", _men);
            allSets.Add("Women", _women);
            allSets.Add("Reading", _reading);
            allSets.Add("Writing", _writing);
            allSets.Add("Arithmetic", _arithmetic);

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string student in allSets.Keys)
            {
                leftSet.Items.Add(student);
                rightSet.Items.Add(student);
            }

            operation.Items.Add("UNION");
            operation.Items.Add("INTERSECTION");
            operation.Items.Add("DIFFERENCE");
            operation.Items.Add("SYMETRIC DIFF");
        }


        private void leftSet_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            leftMembers.Items.Clear();

            if (e.AddedItems.Count > 0)
            {
                DisplaySetData(GetSetByName(e.AddedItems[0].ToString()), leftMembers);
            }
        }

        private void rightSet_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            rightMembers.Items.Clear();

            if (e.AddedItems.Count > 0)
            {
                DisplaySetData(GetSetByName(e.AddedItems[0].ToString()), rightMembers);
            }
        }

        MySet<Student> GetSetByName(string name)
        {
            return allSets[name];
        }

        MySet<Student> GetSetByName(string name)
        {
            return allSets[name];
        }

        void DisplaySetData(MySet<Student> set, ListBox list)
        {
            list.Items.Clear();

            foreach (Student s in set.OrderBy(student => student.StudentId))
            {
                list.Items.Add(string.Format("{0}: {1}", s.StudentId, s.Name));
            }
        }

        private void evaluateButton_Click(object sender, RoutedEventArgs e)
        {
            resultSet.Items.Clear();
            if(operation.SelectedItem != null)
            {
                MySet<Student> results = UpdateResultSet(GetSetByName(leftSet.SelectedItem.ToString()),
                    GetSetByName(rightSet.SelectedItem.ToString()), operation.SelectedItem.ToString());
                DisplaySetData(results, resultSet);

            }
        }

        private MySet<Student> UpdateResultSet(MySet<Student> left, MySet<Student> right, string op)
        {
            switch(op)
            {
                case "UNION":
                    return left.Union(right);
                case "INTERSECTION":
                    return left.Intersection(right);
                case "DIFFERENCE":
                    return left.Difference(right);
                case "SYMETRIC DIFF":
                    return left.SymmetricDifference(right);
                default:
                    MySet<Student> resultSet = new MySet<Student>();
                    resultSet.Add(new Student(-1, "ERROR", Gender.Unknown));
                    return resultSet;
            }
        }
    }
}
