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
    public class TeacherListViewModel : ObservableObject
    {
        private readonly MainViewModel _main;
        private readonly SchoolContext _context;

        public ObservableCollection<Teacher> Teachers { get; set; }

        private Teacher? _selectedTeacher;
        public Teacher? SelectedTeacher
        {
            get => _selectedTeacher;
            set { _selectedTeacher = value; OnPropertyChanged(); }
        }

        public ICommand ViewDetailCommand { get; }

        public TeacherListViewModel(MainViewModel main, SchoolContext context)
        {
            _main = main;
            _context = context;

            Teachers = new ObservableCollection<Teacher>(_context.Teachers
                .Include(c => c.Courses)
                .ToList());

            ViewDetailCommand = new RelayCommand(_ =>
            {
                if (SelectedTeacher != null)
                    _main.ShowTeacherDetail(SelectedTeacher);
            });
        }
    }
}
