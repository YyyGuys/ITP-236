using SchoolModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolWpfApp.ViewModels
{
    public class StudentDetailViewModel : ObservableObject
    {
        private readonly MainViewModel _main;
        private readonly SchoolContext _context;

        public Student Student { get; }

        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }

        public StudentDetailViewModel(MainViewModel main, SchoolContext context, Student student)
        {
            _main = main;
            _context = context;
            Student = student;

            SaveCommand = new RelayCommand(_ =>
            {
                _context.Update(Student);
                _context.SaveChanges();
                _main.ShowStudentList();
            });

            BackCommand = new RelayCommand(_ => _main.ShowStudentList());
        }
    }
}
