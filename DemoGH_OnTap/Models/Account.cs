using System.ComponentModel.DataAnnotations;

namespace DemoGH_OnTap.Models
{
    public class Account
    {
        //Data Anotation Validation được sử dụng  để thực hiện validate dữ liệu ngay từ model
        [Key]
        public Guid Id { get; set; }
        [Required]// bắt buộc phải nhập
        [StringLength(256, MinimumLength =10, ErrorMessage ="Độ dài phải từ 10- 26 kí tự")] // nvarchar(256) 
       public string Name { get; set; }
        public string UserName { get; set; }
        [Required]// bắt buộc phải nhập
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Độ dài phải từ 10- 26 kí tự")] // nvarchar(256) 
        public string Password { get; set; }
         public string Email { get; set; }
        // xxx-xxx-xxxx   => về suy nghĩ hsau cô chữa
        public string Phone { get; set; }
        public string Address {get; set; }
        public virtual GioHang? GioHang { get; set; }
    }
}
