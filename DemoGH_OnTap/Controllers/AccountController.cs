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
                return RedirectToAction("Index", "Account");
            }

        }

        // hiển thị tất cả danh sánh account
        public IActionResult Index(string name)
        {
            // lấy giá trị có tên Account
            var session = HttpContext.Session.GetString("username");
            if(session == null)
            {
                ViewData["message"] = "bạn chưa đăng nhập hoặc đăng nhập hết hạn";
            }
            else
            {
                ViewData["message"] = $"Xin chào {session}";
            }
            // lấy toàn bộ account 
            var accountdata = _db.Accounts.ToList();
            // làm phần tìm kiếm 
            // nếu mà name tìm kiếm rỗng thì sẽ trả về toàn bộ dữ liệu
            if(string.IsNullOrEmpty(name))
            {
                return View(accountdata);
            }
            else
            {
                var seachdata = _db.Accounts.Where(x => x.Name.Contains(name)).ToList();
                // lưu số lượng kết quả tìm kiếm trong viewdata và view bang
                ViewData["count"]= seachdata.Count;
                ViewBag.Count = seachdata.Count;
                if(seachdata.Count == 0)
                {
                    return View(accountdata) ;
                }
                else
                {
                    return View(seachdata);
                }
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Account acc)
        {
            _db.Add(acc);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Deltai(Guid id)
        {
            var acc = _db.Accounts.FirstOrDefault(x => x.Id == id);
            return View(acc);
        }

        public IActionResult Update(Guid id)
        {
            var acc = _db.Accounts.FirstOrDefault(x => x.Id == id);
            return View(acc);
        }
        [HttpPost]
        public IActionResult Update(Account acc, Guid id)
        {
            var update = _db.Accounts.FirstOrDefault(x => x.Id == id);
            update.Name = acc.Name;
            update.UserName = acc.UserName;
            update.Password = acc.Password;
            update.Email = acc.Email;
            update.Phone  = acc.Phone;
            update.Address = acc.Address;
            _db.Accounts.Update(update);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }   

        
        public IActionResult remove(Guid id)
        {
            var remove = _db.Accounts.FirstOrDefault(y => y.Id == id);
            _db.Accounts.Remove(remove);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
