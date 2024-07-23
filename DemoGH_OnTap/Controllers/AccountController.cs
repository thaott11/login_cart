using DemoGH_OnTap.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoGH_OnTap.Controllers
{
    public class AccountController : Controller
    {
        private readonly SD18406CartDbContext _db;
        public AccountController(SD18406CartDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DangKy() // tạo ra view đăng kí thui
        {
            return View();
        }
        [HttpPost]
        public IActionResult Dangky(Account account)
        {

            try
            {
                //tạo mới 1 account
                _db.Accounts.Add(account);
                //Đồng thời tạo luon 1 giỏ hàng
                GioHang gioHang = new GioHang()
                {
                    UserName = account.UserName
                };
                _db.GioHang.Add(gioHang);
                _db.SaveChanges();
                TempData["Status"] = "Tạo tài khoản thành công";
                return RedirectToAction("Login");
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if(userName == null || password == null)
            {
                return View();
            }
            //tìm ra kiếm tài khoản đc nhập
            var acc = _db.Accounts.ToList().FirstOrDefault(x=>x.UserName == userName && x.Password==password ) ;
            if (acc == null) // trong trường hợp không tìm thấy dữ liệu Account tương ứng
            {
                return Content("Đăng nhập thất bại mời kiểm tra lại");
            }
            else // trong trường hợp thành công sẽ trả về trang chủ
            {
                HttpContext.Session.SetString("username", userName); // Lưu dữ liệu login vào Session
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
