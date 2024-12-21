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
    private readonly UniversityContext _context;
    private readonly IDialogService _dialogService;

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

    private void AddNewAnimal(object? obj)
    {
        var instance = MainWindowViewModel.Instance();
        if (instance is not null)
        {
            instance.AnimalsSubView = new AddAnimalViewModel(_context, _dialogService);

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

    private void EditAnimal(object? obj)
    {
        if (obj is not null)
        {
            long animalId = (long)obj;
            EditAnimalViewModel editAnimalViewModel = new EditAnimalViewModel(_context, _dialogService)
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

    private void RemoveAnimal(object? obj)
    {
        if (obj is not null)
        {
            long animalId = (long)obj;
            Animal? animal = _context.Animals.Find(animalId);
            if (animal is not null)
            {
                DialogResult = _dialogService.Show(animal.Name + " " + animal.Age);
                if (DialogResult == false)
                {
                    return;
                }

                _context.Animals.Remove(animal);
                _context.SaveChanges();
            }
        }
    }

    public AnimalsViewModel(UniversityContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;

        _context.Database.EnsureCreated();
        _context.Animals.Load();
        Animals = _context.Animals.Local.ToObservableCollection();
    }
}
