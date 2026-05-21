using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetDemo;

public class Student : IComparable<Student>
{
    public Student(int id, string name, Gender gender)
    {
        StudentId = id;
        Name = name;
        Gender = gender;
    }

    public int StudentId { get; private set; }
    public string Name { get; private set; }
    public Gender Gender { get; private set; }

    public int CompareTo(Student? obj)
    {
        return StudentId.CompareTo(obj?.StudentId);
    }
}
