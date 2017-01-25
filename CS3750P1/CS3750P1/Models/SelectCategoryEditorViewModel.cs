namespace CS3750P1.Models
{
    public class SelectCategoryEditorViewModel
    {
        public bool Selected { get; set; }
        public int id { get; set; }
        public string categoryName { get; set; }

        public int listIDforButton { get; set; }

        public SelectCategoryEditorViewModel()
        {

        }

        public SelectCategoryEditorViewModel(string catName, int id)
        {
            this.categoryName = catName;
            this.id = id;
            this.Selected = false;
        }
    }
}
