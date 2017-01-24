using System.Collections.Generic;
using System.Linq;

namespace CS3750P1.Models
{
    public class CategorySelectionViewModel
    {
        public List<SelectCategoryEditorViewModel> Category { get; set; }
        public CategorySelectionViewModel()
        {
            this.Category = new List<SelectCategoryEditorViewModel>();
        }
        public IEnumerable<int> getSelectedIds()
        {
            // Return an Enumerable containing the Id's of the selected category:
            return (from p in this.Category where p.Selected select p.id).ToList();
        }
    }
}