using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Interfaces;

namespace University.ViewModels;

public class EditAnimalViewModel : AnimalBaseViewModel
{
    public EditAnimalViewModel(
        UniversityContext context, 
        IDialogService dialogService,
        IAnimalService animalService):
        base(context, dialogService, animalService)
    {
    }

    public override void SaveData(object? obj)
    {
        if (!IsValid())
        {
            Response = "Please complete all required fields";
            return;
        }

        if (_animal is null)
        {
            return;
        }

        _animalService.EditAnimal(_animal, this.Species, this.Name, this.Age);

        Response = "Data Updated";
    }
}
