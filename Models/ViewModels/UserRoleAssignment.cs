using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuickReservation.Models.ViewModels
{
    public class UserRoleAssignment
    {
        public User? User { get; set; }
        public Role? Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
