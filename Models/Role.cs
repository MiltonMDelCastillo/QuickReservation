namespace QuickReservation.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        // Navegación
        public ICollection<User> Users { get; set; }
    }

}
