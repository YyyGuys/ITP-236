using SchoolModel;
using SchoolWpfApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolWpfApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private object _currentView;
        private readonly SchoolContext _context;
        public ICommand ShowStudentsCommand { get; }
        public ICommand ShowCoursesCommand { get; }
        public ICommand ShowTeachersCommand { get; }

        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            _context = new SchoolContextFactory().CreateDbContext();
            ShowStudentsCommand = new RelayCommand(_ => ShowStudentList());
            ShowCoursesCommand = new RelayCommand(_ => ShowCourseList(), _ => true); // enabled for now
            ShowTeachersCommand = new RelayCommand(_ => ShowTeacherList(), _ => true); // enabled for now
            ShowStudentList();
            //ShowCourseList();
        }
        public void ShowCourseList()
        {
            CurrentView = new CourseListViewModel(this, _context);
        }

        public void ShowStudentList()
        {
            CurrentView = new StudentListViewModel(this, _context);
        }
        public void ShowTeacherList()
        {
            CurrentView = new TeacherListViewModel(this, _context);
        }

        public void ShowStudentDetail(Student student)
        {
            CurrentView = new StudentDetailViewModel(this, _context, student);
        }
        public void ShowCourseDetail(Course course)
        {
            CurrentView = new CourseDetailViewModel(this, _context, course);
        }
        public void ShowTeacherDetail(Teacher teacher)
        {
            CurrentView = new TeacherDetailViewModel(this, _context, teacher);
        }
    }
}
