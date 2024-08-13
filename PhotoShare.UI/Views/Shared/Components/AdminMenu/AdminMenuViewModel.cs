namespace PhotoShare.Views.Shared.Components.AdminMenu
{
    public class AdminMenuViewModel
    {
        public string focusedItem { get; set; }
        public AdminMenuViewModel()
        {
                
        }

        public AdminMenuViewModel(string focusedItem)
        {
            this.focusedItem = focusedItem;
        }
    }
}
