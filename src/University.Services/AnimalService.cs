using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Windows.Controls;
using System.Xml.Linq;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.Services;

/// <summary>
/// Implementation of the IAnimalService.
/// </summary>
public class AnimalService: IAnimalService
{
    #region Properties And Ctor

    private readonly UniversityContext _context;
    private readonly IDialogService _dialogService;

    /// <summary>
    /// Constructor for the AnimalService.
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="dialogService">Dialog service</param>
    public AnimalService(
        UniversityContext context,
        IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;
    }

    #endregion // Properties And Ctor

    #region IAnimalService Implementation

    /// <inheritdoc/>
    public void AddAnimal(string species, string name, int age)
    {
        Animal animal = new Animal
        {
            Species = species,
            Name = name,
            Age = age
        };

        _context.Animals.Add(animal);
        var result = _context.SaveChanges();
        if (result != 1)
        {
            throw new Exception("Failed to add animal to the database");
        }
    }

    /// <inheritdoc/>
    public void EditAnimal(Animal animal, string species, string name, int age)
    {
        if (animal is null)
        {
            return;
        }

        animal.Species = species;
        animal.Name = name;
        animal.Age = age;

        _context.Entry(animal).State = EntityState.Modified;
        var result = _context.SaveChanges();
        if (result != 1)
        {
            throw new Exception("Failed to edit animal into the database");
        }
    }

    /// <inheritdoc/>
    public void DeleteAnimal(long animalId)
    {

        Animal? animal = _context.Animals.Find(animalId);
        if (animal is not null)
        {
            bool? dialogResult = _dialogService.Show(animal.Name + " " + animal.Age);
            if (dialogResult == false)
            {
                return;
            }

            _context.Animals.Remove(animal);

            var result = _context.SaveChanges();
            if (result != 1)
            {
                throw new Exception("Failed to remove the animal form the database");
            }
        }
    }

    #endregion // IAnimalService Implementation
}
