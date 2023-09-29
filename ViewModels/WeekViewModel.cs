using Microsoft.AspNetCore.Mvc.Rendering;

namespace STMWeb.ViewModels
{
    public class WeekViewModel
    {
        //display property to carry data to the create client view select (combo box)

        public List<SelectListItem> SemesterSelectList { set; get; } = null!;
        public string SelectedSemester { get; set; } = null!;

        public List<SelectListItem> ModuleSelectList { set; get; } = null!;
        public string SelectedModule { get; set; } = null!;
    }
}
