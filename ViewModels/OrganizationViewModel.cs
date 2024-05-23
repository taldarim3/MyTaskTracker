using MyTaskTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyTaskTracker.ViewModels
{
    public class IndexOrganizationViewModel
    {
        public IEnumerable<Organization> Organizations { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalOrganizations { get; set; }
        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;
    }
}
