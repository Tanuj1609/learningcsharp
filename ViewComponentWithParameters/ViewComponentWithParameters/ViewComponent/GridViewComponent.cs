using Microsoft.AspNetCore.Mvc;
using ViewDataWithViewComponents.Models;
namespace ViewComponentsExample.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PersonGridModel grid)
        {
            PersonGridModel personGridModel = new PersonGridModel()
            {
                GridTitle = "Persons List",
                Persons = new List<Person>() {
          new Person() { PersonName = "John", JobTitle = "Manager" },
          new Person() { PersonName = "Jones", JobTitle = "Asst. Manager" },
          new Person() { PersonName = "William", JobTitle = "Clerk" },
        }
            };
           
            return View("Sample",grid); //invoked a partial view Views/Shared/Components/Grid/sample.cshtml
        }
    }
}