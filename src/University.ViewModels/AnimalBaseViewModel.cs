using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public abstract class AnimalBaseViewModel : ViewModelBase, IDataErrorInfo
{
    #region Properties And Ctor

    protected readonly UniversityContext _context;
    protected readonly IDialogService _dialogService;
    protected readonly IAnimalService _animalService;
    protected Animal? _animal = new Animal();

    protected AnimalBaseViewModel(
        UniversityContext context,
        IDialogService dialogService,
        IAnimalService animalService)
    {
        _context = context;
        _dialogService = dialogService;
        _animalService = animalService;
    }

    public string Error
    {
        get { return string.Empty; }
    }

    public string this[string columnName]
    {
        get
        {
            if (columnName == "Species")
            {
                if (string.IsNullOrEmpty(Species))
                {
                    return "Species is Required";
                }
            }
            if (columnName == "Name")
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "Name is Required";
                }
            }
            if (columnName == "Age")
            {
                if (Age <= 0)
                {
                    return "Age must be greater than 0";
                }
            }
            return string.Empty;
        }
    }

    private long _animalId = 0;
    public long AnimalId
    {
        get
        {
            return _animalId;
        }
        set
        {
            _animalId = value;
            OnPropertyChanged(nameof(AnimalId));
            LoadAnimalData();
        }
    }

    private string _species = string.Empty;
    public string Species
    {
        get
        {
            return _species;
        }
        set
        {
            _species = value;
            OnPropertyChanged(nameof(Species));
        }
    }

    private string _name = string.Empty;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private int _age = 0;
    public int Age
    {
        get
        {
            return _age;
        }
        set
        {
            _age = value;
            OnPropertyChanged(nameof(Age));
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
            instance.AnimalsSubView = new AnimalsViewModel(_context, _dialogService, _animalService);
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

    #endregion // Properties And Ctor

    #region Public Methods  

    public virtual void SaveData(object? obj)
    {
        // TODO: Implement SaveData in derived classes
    }

    #endregion // Public Methods

    #region Protected Methods

    protected bool IsValid()
    {
        string[] properties = { "Species", "Name", "Age" };
        foreach (string property in properties)
        {
            if (!string.IsNullOrEmpty(this[property]))
            {
                return false;
            }
        }
        return true;
    }

    protected void LoadAnimalData()
    {
        if (_context?.Animals is null)
        {
            return;
        }
        _animal = _context.Animals.Find(AnimalId);
        if (_animal is null)
        {
            return;
        }
        this.Name = _animal.Name;
        this.Species = _animal.Species;
        this.Age = _animal.Age;
    }

    #endregion // Protected Methods
}
