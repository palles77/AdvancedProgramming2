using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Interfaces;

namespace University.ViewModels;

public class EditAnimalViewModel : AnimalBaseViewModel
{
    public EditAnimalViewModel(UniversityContext context, IDialogService dialogService):
        base(context, dialogService)
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
        _animal.Species = Species;
        _animal.Name = Name;
        _animal.Age = Age;

        _context.Entry(_animal).State = EntityState.Modified;
        _context.SaveChanges();

        Response = "Data Updated";
    }
}
