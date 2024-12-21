using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public class AddAnimalViewModel : AnimalBaseViewModel
{
    public AddAnimalViewModel(UniversityContext context, IDialogService dialogService)
        : base(context, dialogService)
    {
    }
    public override void SaveData(object? obj)
    {
        if (!IsValid())
        {
            Response = "Please complete all required fields";
            return;
        }

        Animal animal = new Animal
        {
            Species = this.Species,
            Name = this.Name,
            Age = this.Age
        };

        _context.Animals.Add(animal);
        _context.SaveChanges();

        Response = "Data Saved";
    }
}
