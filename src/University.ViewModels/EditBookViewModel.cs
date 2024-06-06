using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public class EditBookViewModel : ViewModelBase, IDataErrorInfo
{
    private readonly UniversityContext _context;
    private readonly IDialogService _dialogService;
    private Book? _Book = new Book();

    public string Error
    {
        get { return string.Empty; }
    }
    public string this[string columnName]
    {
        get
        {
            if (columnName == "Title")
            {
                if (string.IsNullOrEmpty(Title))
                {
                    return "Title is Required";
                }
            }
            if (columnName == "Author")
            {
                if (string.IsNullOrEmpty(Author))
                {
                    return "Author is Required";
                }
            }
            return string.Empty;
        }
    }

    private long _bookId = 0;
    public long BookId
    {
        get
        {
            return _bookId;
        }
        set
        {
            _bookId = value;
            OnPropertyChanged(nameof(BookId));
            LoadBookData();
        }
    }


    private string _title = string.Empty;
    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    private string _author = string.Empty;
    public string Author
    {
        get
        {
            return _author;
        }
        set
        {
            _author = value;
            OnPropertyChanged(nameof(Author));
        }
    }

    private string _response = string.Empty;
    public string Response
    {
        get
        {
            return _response;
        }
        set
        {
            _response = value;
            OnPropertyChanged(nameof(Response));
        }
    }

    // TODO: Uncomment when related entities are implemented
    //private ObservableCollection<Subject>? _assignedSubjects = null;
    //public ObservableCollection<Subject> AssignedSubjects
    //{
    //    get
    //    {
    //        if (_assignedSubjects is null)
    //        {
    //            _assignedSubjects = LoadSubjects();
    //            return _assignedSubjects;
    //        }
    //        return _assignedSubjects;
    //    }
    //    set
    //    {
    //        _assignedSubjects = value;
    //        OnPropertyChanged(nameof(AssignedSubjects));
    //    }
    //}

    private ICommand? _back = null;
    public ICommand Back
    {
        get
        {
            if (_back is null)
            {
                _back = new RelayCommand<object>(NavigateBack);
            }
            return _back;
        }
    }

    private void NavigateBack(object? obj)
    {
        var instance = MainWindowViewModel.Instance();
        if (instance is not null)
        {
            instance.BooksSubView = new BooksViewModel(_context, _dialogService);
        }
    }

    private ICommand? _save = null;
    public ICommand Save
    {
        get
        {
            if (_save is null)
            {
                _save = new RelayCommand<object>(SaveData);
            }
            return _save;
        }
    }

    private void SaveData(object? obj)
    {
        if (!IsValid())
        {
            Response = "Please complete all required fields";
            return;
        }

        if (_Book is null)
        {
            return;
        }
        _Book.Title = Title;
        _Book.Author = Author;
        // TODO: Uncomment when related entities are implemented
        //_Book.Subjects = AssignedSubjects.Where(s => s.IsSelected).ToList();

        _context.Entry(_Book).State = EntityState.Modified;
        _context.SaveChanges();

        Response = "Data Updated";
    }

    public EditBookViewModel(UniversityContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;
    }

    private ObservableCollection<Subject> LoadSubjects()
    {
        _context.Database.EnsureCreated();
        _context.Subjects.Load();
        return _context.Subjects.Local.ToObservableCollection();
    }

    private bool IsValid()
    {
        string[] properties = { "Title", "Author" };
        foreach (string property in properties)
        {
            if (!string.IsNullOrEmpty(this[property]))
            {
                return false;
            }
        }
        return true;
    }

    private void LoadBookData()
    {
        if (_context?.Books is null)
        {
            return;
        }
        _Book = _context.Books.Find(BookId);
        if (_Book is null)
        {
            return;
        }
        this.Title = _Book.Title;
        this.Author = _Book.Author;

        // TODO: Uncomment when related entities are implemented
        //if (_Book.Subjects is null)
        //{
        //    return;
        //}
        //foreach (Subject subject in _Book.Subjects)
        //{
        //    if (subject is not null && AssignedSubjects is not null)
        //    {
        //        var assignedSubject = AssignedSubjects
        //            .FirstOrDefault(s => s.SubjectId == subject.SubjectId);
        //        if (assignedSubject is not null)
        //        { 
        //            assignedSubject.IsSelected = true;
        //        }
        //    }
        //}
    }
}
