#define CRUD
using Microsoft.EntityFrameworkCore;
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
    public class CourseListViewModel : ObservableObject
    {
        private readonly MainViewModel _main;
        private readonly SchoolContext _context;

        public ObservableCollection<Course> Courses { get; set; }

        private Course? _selectedCourse;
        public Course? SelectedCourse
        {
            get => _selectedCourse;
            set { _selectedCourse = value; OnPropertyChanged(); }
        }

        public ICommand ViewDetailCommand { get; }
#if CRUD 
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
#endif


        public CourseListViewModel(MainViewModel main, SchoolContext context)
        {
            _main = main;
            _context = context;

            Courses = new ObservableCollection<Course>(_context.Courses
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                .Include(c => c.Teacher)
                .ToList());

            ViewDetailCommand = new RelayCommand(_ =>
            {
                if (SelectedCourse != null)
                    _main.ShowCourseDetail(SelectedCourse);
            });
#if CRUD
            AddCommand = new RelayCommand(_ =>
            {
                var newCourse = new Course() { Tag = "", Title= "" };
                _main.ShowCourseDetail(newCourse);
            });

            DeleteCommand = new RelayCommand(courseObj =>
            {
                if (courseObj is Course course)
                {
                    _context.Courses.Remove(course);
                    _context.SaveChanges();
                    Courses.Remove(course);
                }
            });
#endif
        }
    }
}
