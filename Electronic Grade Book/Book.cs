using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronic_Grade_Book
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;

    }

    public class DiskBook : IBook
    {
        public string Name => throw new NotImplementedException();

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade)
        {
            throw new NotImplementedException();
        }

        public abstract Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Book : NamedObject, IBook
    {
        protected Book(string name) : base(name)
        {
        }

        public abtract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics()
    
    }


    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public void AddLetterGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                default:
                    AddGrade(0);
                    break;
            }
        }

        public override void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
                //Console.WriteLine("Invalid Value");
            }
            
        }

        public event GradeAddedDelegate GradeAdded; 

        public Statistics GetStatistics()
        {
            var result = new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            var index = 0;
            if (grades.Count > 0)
            do
            {
                result.Low = Math.Min(grades[index], result.Low);
                result.High = Math.Max(grades[index], result.High);
                result.Average += grades[index];
                index++;
            } while (index < grades.Count);
            result.Average /= grades.Count;

            switch(result.Average)
            {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                        break;

                case var d when d >= 80.0:
                    result.Letter = 'B';
                        break;

                case var d when d >= 70.0:
                    result.Letter = 'C';
                        break;

                case var d when d >= 60.0:
                    result.Letter = 'D';
                        break;

                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }

       private List<double> grades;
       public string Name;
    }
}

