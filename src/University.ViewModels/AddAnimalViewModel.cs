using University.Data;
using University.Interfaces;

namespace University.ViewModels;

public class AddAnimalViewModel : AnimalBaseViewModel
{
    public AddAnimalViewModel(
        UniversityContext context, 
        IDialogService dialogService,
        IAnimalService animalService)
        : base(context, dialogService, animalService)
    {
    }

    public override void SaveData(object? obj)
    {
        if (!IsValid())
        {
            Response = "Please complete all required fields";
            return;
        }

        _animalService.AddAnimal(this.Species, this.Name, this.Age);

        Response = "Data Saved";
    }
}
