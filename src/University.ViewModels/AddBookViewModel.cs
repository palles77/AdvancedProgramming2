using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using University.Data;
using University.Extensions;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public class AddBookViewModel : ViewModelBase, IDataErrorInfo
{
    private readonly UniversityContext _context;
    private readonly IDialogService _dialogService;

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
    //public ObservableCollection<Subject>? AssignedSubjects
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
    public ICommand? Back
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
    public ICommand? Save
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

        Book Book = new Book
        {
            Title = this.Title,
            Author = this.Author,
            // TODO: Uncomment when related entities are implemented
            //Subjects = AssignedSubjects?.Where(s => s.IsSelected).ToList()
        };

        _context.Books.Add(Book);
        _context.SaveChanges();

        Response = "Data Saved";
    }

    public AddBookViewModel(UniversityContext context, IDialogService dialogService)
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
        string[] properties = { "Title", "Author"};
        foreach (string property in properties)
        {
            if (!string.IsNullOrEmpty(this[property]))
            {
                return false;
            }
        }
        return true;
    }
}
