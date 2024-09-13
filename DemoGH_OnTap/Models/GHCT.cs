namespace DemoGH_OnTap.Models
{
    public class GHCT
    {
        public Guid Id { get; set; }

        public int Amount {  get; set; } 
        public Guid? SanPhamID { get; set; }
        public Guid? GioHangID { get; set; }

        public GioHang? GioHang { get; set; }  
        public SanPham? SanPham { get; set; }
    }
}
