using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public class AnimalsViewModel : ViewModelBase
{
    #region Properties And Ctor

    private readonly UniversityContext _context;
    private readonly IDialogService _dialogService;
    private readonly IAnimalService _animalService;

    private bool? _dialogResult = null;
    public bool? DialogResult
    {
        get
        {
            return _dialogResult;
        }
        set
        {
            _dialogResult = value;
        }
    }

    private ObservableCollection<Animal>? _animals = null;
    public ObservableCollection<Animal>? Animals
    {
        get
        {
            if (_animals is null)
            {
                _animals = new ObservableCollection<Animal>();
                return _animals;
            }
            return _animals;
        }
        set
        {
            _animals = value;
            OnPropertyChanged(nameof(Animals));
        }
    }

    private ICommand? _add = null;
    public ICommand? Add
    {
        get
        {
            if (_add is null)
            {
                _add = new RelayCommand<object>(AddNewAnimal);
            }
            return _add;
        }
    }

    private ICommand? _edit = null;
    public ICommand? Edit
    {
        get
        {
            if (_edit is null)
            {
                _edit = new RelayCommand<object>(EditAnimal);
            }
            return _edit;
        }
    }

    private ICommand? _remove = null;
    public ICommand? Remove
    {
        get
        {
            if (_remove is null)
            {
                _remove = new RelayCommand<object>(RemoveAnimal);
            }
            return _remove;
        }
    }

    public AnimalsViewModel(
        UniversityContext context, 
        IDialogService dialogService,
        IAnimalService animalService)
    {
        _context = context;
        _dialogService = dialogService;
        _animalService = animalService;

        _context.Database.EnsureCreated();
        _context.Animals.Load();
        Animals = _context.Animals.Local.ToObservableCollection();
        _animalService = animalService;
    }

    #endregion // Properties And Ctor

    #region Private Methods

    private void AddNewAnimal(object? obj)
    {
        var instance = MainWindowViewModel.Instance();
        if (instance is not null)
        {
            instance.AnimalsSubView = new AddAnimalViewModel(_context, _dialogService, _animalService);
        }
    }

    private void EditAnimal(object? obj)
    {
        if (obj is not null)
        {
            long animalId = (long)obj;
            EditAnimalViewModel editAnimalViewModel = new EditAnimalViewModel(_context, _dialogService, _animalService)
            {
                AnimalId = animalId
            };
            var instance = MainWindowViewModel.Instance();
            if (instance is not null)
            {
                instance.AnimalsSubView = editAnimalViewModel;
            }
        }
    }

    private void RemoveAnimal(object? obj)
    {
        if (obj is not null)
        {
            long animalId = (long)obj;
            _animalService.DeleteAnimal(animalId);
        }
    }

    #endregion // Private Methods
}
