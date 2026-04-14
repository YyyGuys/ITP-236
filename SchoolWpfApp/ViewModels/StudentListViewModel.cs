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
    public class StudentListViewModel : ObservableObject
    {
        private readonly MainViewModel _main;
        private readonly SchoolContext _context;

        public ObservableCollection<Student> Students { get; set; }

        private Student? _selectedStudent;
        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set { _selectedStudent = value; OnPropertyChanged(); }
        }

        public ICommand ViewDetailCommand { get; }

        public StudentListViewModel(MainViewModel main, SchoolContext context)
        {
            _main = main;
            _context = context;

            Students = new ObservableCollection<Student>(_context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .ToList());

            ViewDetailCommand = new RelayCommand(_ =>
            {
                if (SelectedStudent != null)
                    _main.ShowStudentDetail(SelectedStudent);
            });
        }
    }
}
