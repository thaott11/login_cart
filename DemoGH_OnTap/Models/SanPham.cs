using System.ComponentModel.DataAnnotations;

namespace DemoGH_OnTap.Models
{
    public class SanPham
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }    
        public string Price {  get; set; }
        public List<GHCT> GHCTs { get; set; }
    }
}
