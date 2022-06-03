using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class ItemData
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public bool Done { get; set; }
        // bao thay fix o day
    }
}
