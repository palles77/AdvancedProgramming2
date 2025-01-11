using University.Models;

namespace University.Interfaces;

/// <summary>
/// Interface for the Animal Service
/// </summary>
public interface IAnimalService
{
    /// <summary>
    /// Add a new animal to the database
    /// </summary>
    /// <param name="species">Species of the animal</param>
    /// <param name="name">Name of the animal</param>
    /// <param name="age">Age of the animal</param>
    void AddAnimal(string species, string name, int age);

    /// <summary>
    /// Edit existing animal inside the database
    /// </summary>
    /// <param name="animal">Animal to be edited</param>
    /// <param name="species">Species of the animal</param>
    /// <param name="name">Name of the animal</param>
    /// <param name="age">Age of the animal</param>
    void EditAnimal(Animal animal, string species, string name, int age);

    /// <summary>
    /// Delete an animal from the database
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="animalId">Id of the animal to be deleted</param>
    void DeleteAnimal(long animalId);
}
