namespace StudentGradeManager
{
    class Program
    {
        private static Student[] students;
        static void Main(string[] args)
        {
            students = new Student[5];

            Student student1 = new Student("David", "Rico", 100);
            Student student2 = new Student("Juan", "Perez", 80);
            Student student3 = new Student("Maria", "Gonzalez", 80);
            Student student4 = new Student("Ana", "Lopez", 90);
            Student student5 = new Student("Luis", "Martinez", 20);

            students[0] = student1;
            students[1] = student2;
            students[2] = student3;
            students[3] = student4;
            students[4] = student5;

            int averageGrade = 0;
            int highestGrade = 0;
            String highestGradeStudent = "";

            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine($"Student {i + 1}, Name: {students[i].GetName()} {students[i].GetSurname()}, Grade: {students[i].GetGrade()}");
                averageGrade += students[i].GetGrade();

                if (students[i].GetGrade() > highestGrade)
                {
                    highestGrade = students[i].GetGrade();
                    highestGradeStudent = students[i].GetName() + " " + students[i].GetSurname();
                }
            }

            averageGrade = averageGrade / students.Length;
            Console.WriteLine($"Average Grade: {averageGrade}");
            Console.WriteLine($"Highest Grade: {highestGrade}");
            Console.WriteLine($"Student with Highest Grade: {highestGradeStudent}");
        }
    }

    class Student
    {
        private string name;
        private string surname;
        private int grade;

        public Student(string name, string surname, int grade)
        {
            this.name = name;
            this.surname = surname;
            this.grade = grade;
        }

        public string GetName()
        {
            return name;
        }

        public string GetSurname()
        {
            return surname;
        }

        public int GetGrade()
        {
            return grade;
        }

    }
}