﻿using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public class BooksViewModel : ViewModelBase
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

    private ObservableCollection<Book>? _Books = null;
    public ObservableCollection<Book>? Books
    {
        get
        {
            if (_Books is null)
            {
                _Books = new ObservableCollection<Book>();
                return _Books;
            }
            return _Books;
        }
        set
        {
            _Books = value;
            OnPropertyChanged(nameof(Books));
        }
    }

    private ICommand? _add = null;
    public ICommand? Add
    {
        get
        {
            if (_add is null)
            {
                _add = new RelayCommand<object>(AddNewBook);
            }
            return _add;
        }
    }

    private void AddNewBook(object? obj)
    {
        var instance = MainWindowViewModel.Instance();
        if (instance is not null)
        {
            instance.BooksSubView = new AddBookViewModel(_context, _dialogService);

        }
    }

    private ICommand? _edit = null;
    public ICommand? Edit
    {
        get
        {
            if (_edit is null)
            {
                _edit = new RelayCommand<object>(EditBook);
            }
            return _edit;
        }
    }

    private void EditBook(object? obj)
    {
        if (obj is not null)
        {
            long bookId = (long) obj;
            EditBookViewModel editBookViewModel = new EditBookViewModel(_context, _dialogService)
            {
                BookId = bookId
            };
            var instance = MainWindowViewModel.Instance();
            if (instance is not null)
            {
                instance.BooksSubView = editBookViewModel;
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
                _remove = new RelayCommand<object>(RemoveBook);
            }
            return _remove;
        }
    }

    private void RemoveBook(object? obj)
    {
        if (obj is not null)
        {
            long BookId = (long)obj;
            Book? Book = _context.Books.Find(BookId);
            if (Book is not null)
            {
                DialogResult = _dialogService.Show(Book.Title + " " + Book.Author);
                if (DialogResult == false)
                {
                    return;
                }

                _context.Books.Remove(Book);
                _context.SaveChanges();
            }
        }
    }

    public BooksViewModel(UniversityContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;

        _context.Database.EnsureCreated();
        _context.Books.Load();
        Books = _context.Books.Local.ToObservableCollection();
    }
}
