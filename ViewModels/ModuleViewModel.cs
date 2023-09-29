using Microsoft.AspNetCore.Mvc.Rendering;

namespace STMWeb.ViewModels
{
    public class ModuleViewModel
    {
        //display property to carry data to the create client view select (combo box)

        public List<SelectListItem> SemesterSelectList { set; get; } = null!;
        public string SelectedSemester { get; set; } = null!;

        public List<SelectListItem> DaySelectList { set; get; } = null!;
        public string SelectedDay { get; set; } = null!;
    }
}
