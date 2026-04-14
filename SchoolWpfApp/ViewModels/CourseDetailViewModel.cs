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
    public class CourseDetailViewModel : ObservableObject
    {
        private readonly MainViewModel _main;
        private readonly SchoolContext _context;

        public Course Course { get; }
        public ObservableCollection<Student> Students { get; } = new ObservableCollection<Student>();


        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }
        public ObservableCollection<Teacher> Teachers { get; }


        public CourseDetailViewModel(MainViewModel main, SchoolContext context, Course course)
        {
            _main = main;
            _context = context;
            Course = course;
            Students = new ObservableCollection<Student>(
                _context.Enrollments
                    .Where(e => e.CourseId == course.CourseId)
                    .Select(e => e.Student)
                    .ToList());
            Teachers = new ObservableCollection<Teacher>(
               _context.Teachers.OrderBy(t => t.FullName).ToList());


           SaveCommand = new RelayCommand(_ =>
                {
                    _context.Update(Course);
                    _context.SaveChanges();
                    _main.ShowCourseList();
                });

            BackCommand = new RelayCommand(_ => _main.ShowCourseList());
        }
    }
}
