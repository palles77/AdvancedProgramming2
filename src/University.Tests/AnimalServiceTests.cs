using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using University.Data;
using University.Interfaces;
using University.Models;
using University.Services;

namespace University.Tests;

[TestClass]
public class AnimalServiceTests
{
    private readonly Mock<IDialogService> _mockDialogService;
    private readonly UniversityContext _context;
    private readonly AnimalService _animalService;

    public AnimalServiceTests()
    {
        var options = new DbContextOptionsBuilder<UniversityContext>()
            .UseInMemoryDatabase(databaseName: "UniversityTestDb")
            .Options;

        _context = new UniversityContext(options);
        _mockDialogService = new Mock<IDialogService>();
        _animalService = new AnimalService(_context, _mockDialogService.Object);
    }

    [TestMethod]
    public void AddAnimal_ShouldAddAnimalToDatabase()
    {
        // Arrange
        string species = "Dog";
        string name = "Buddy Something";
        int age = 3;

        // Act
        _animalService.AddAnimal(species, name, age);

        // Assert
        var animal = _context.Animals.FirstOrDefault(a => a.Name == name);
        Assert.IsNotNull(animal);
        Assert.AreEqual(species, animal.Species);
        Assert.AreEqual(name, animal.Name);
        Assert.AreEqual(age, animal.Age);
    }

    [TestMethod]
    public void EditAnimal_ShouldUpdateAnimalInDatabase()
    {
        // Arrange
        var animal = new Animal { Species = "Cat", Name = "Whiskers", Age = 2 };
        _context.Animals.Add(animal);
        _context.SaveChanges();

        string newSpecies = "Parrot";
        string newName = "Polly";
        int newAge = 6;

        // Act
        _animalService.EditAnimal(animal, newSpecies, newName, newAge);

        // Assert
        var updatedAnimal = _context.Animals.FirstOrDefault(a => a.AnimalId == animal.AnimalId);
        Assert.IsNotNull(updatedAnimal);
        Assert.AreEqual(newSpecies, updatedAnimal.Species);
        Assert.AreEqual(newName, updatedAnimal.Name);
        Assert.AreEqual(newAge, updatedAnimal.Age);
    }

    [TestMethod]
    public void DeleteAnimal_ShouldRemoveAnimalFromDatabase()
    {
        // Arrange
        var animal = new Animal { Species = "Rabbit", Name = "Thumper", Age = 1 };
        _context.Animals.Add(animal);
        _context.SaveChanges();

        _mockDialogService.Setup(ds => ds.Show(It.IsAny<string>())).Returns(true);

        // Act
        _animalService.DeleteAnimal(animal.AnimalId);

        // Assert
        var deletedAnimal = _context.Animals.FirstOrDefault(a => a.AnimalId == animal.AnimalId);
        Assert.IsNull(deletedAnimal);
    }

    [TestMethod]
    public void DeleteAnimal_ShouldNotRemoveAnimalIfDialogCancelled()
    {
        // Arrange
        var animal = new Animal { Species = "Hamster", Name = "Nibbles", Age = 1 };
        _context.Animals.Add(animal);
        _context.SaveChanges();

        _mockDialogService.Setup(ds => ds.Show(It.IsAny<string>())).Returns(false);

        // Act
        _animalService.DeleteAnimal(animal.AnimalId);

        // Assert
        var existingAnimal = _context.Animals.FirstOrDefault(a => a.AnimalId == animal.AnimalId);
        Assert.IsNotNull(existingAnimal);
    }
}
