using SchoolModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolWpfApp.ViewModels
{
    public class TeacherDetailViewModel : ObservableObject
    {
        private readonly MainViewModel _main;
        private readonly SchoolContext _context;

        public Teacher Teacher { get; }


        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }
        public ObservableCollection<Course> Courses { get; } = new ObservableCollection<Course>();


        public TeacherDetailViewModel(MainViewModel main, SchoolContext context, Teacher teacher)
        {
            _main = main;
            _context = context;
            Teacher = teacher;
            Courses = new ObservableCollection<Course>(
                _context.Courses
                    .Where(c => c.TeacherId == teacher.TeacherId)
                    .ToList());

            SaveCommand = new RelayCommand(_ =>
            {
                _context.Update(Teacher);
                _context.SaveChanges();
                _main.ShowTeacherList();
            });

            BackCommand = new RelayCommand(_ => _main.ShowTeacherList());
        }
    }
}
