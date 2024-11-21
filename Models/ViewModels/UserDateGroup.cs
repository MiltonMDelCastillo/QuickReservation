using System.ComponentModel.DataAnnotations;

namespace QuickReservation.Models.ViewModels
{
    public class UserDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime UserCreationDate { get; set; }
        public int UserCount { get; set; }
    }
}