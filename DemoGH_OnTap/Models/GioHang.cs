using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace DemoGH_OnTap.Models
{
    public class GioHang
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public int Status { get; set; }
        public Account? Account { get; set; }
        public List<GHCT> GHCTs { get; set; }

        public Guid? AccountID { get; set; }
    }
}
