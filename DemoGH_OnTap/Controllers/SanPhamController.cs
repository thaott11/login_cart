using DemoGH_OnTap.Models;
using Microsoft.AspNetCore.Mvc;

public class SanPhamController : Controller
{
    private readonly SD18406CartDbContext _db;
    public SanPhamController(SD18406CartDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var list = _db.SanPhams.ToList();
        return View(list);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(SanPham sp)
    {
        _db.SanPhams.Add(sp);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(SanPham sp, Guid id)
    {
        var delete = _db.SanPhams.Find(id);
        _db.Remove(id);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        var edit = _db.SanPhams.Find(id);
        return View(edit);
    }

    [HttpPost]
    public IActionResult Edit(SanPham sp, Guid id)
    {
        var edit = _db.SanPhams.Find(id);
        edit.Name = sp.Name;
        edit.Price = sp.Price;
        _db.SanPhams.Update(edit);
        _db.SaveChanges();
        return RedirectToAction("Index");

    }

    public IActionResult AddToCart(Guid id, int amount)
    {
        var user = HttpContext.Session.GetString("username");
        if (user == null)
        {
            return Content("Chưa đăng nhập hoặc phiên đăng nhập hết hạn");
        }

        var getUser = _db.Accounts.FirstOrDefault(x => x.UserName == user);
        if (getUser == null)
        {
            return Content("User không tồn tại.");
        }

        var gioHang = _db.GioHang.FirstOrDefault(x => x.AccountID == getUser.Id);
        if (gioHang == null)
        {
            return Content("Giỏ hàng không tồn tại.");
        }

        var userCart = _db.GHCTs.Where(x => x.GioHangID == gioHang.Id).ToList();
        bool check = false;
        Guid idGHCT = Guid.Empty;

        foreach (var item in userCart)
        {
            if (item.SanPhamID == id)
            {
                check = true;
                idGHCT = item.Id;
                break;
            }
        }

        if (!check)
        {
            GHCT ghct = new GHCT()
            {
                SanPhamID = id,
                GioHangID = gioHang.Id,
                Amount = amount,
            };
            _db.GHCTs.Add(ghct);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        else
        {
            var ghctUpdate = _db.GHCTs.FirstOrDefault(x => x.Id == idGHCT);
            ghctUpdate.Amount = ghctUpdate.Amount + amount;
            _db.GHCTs.Update(ghctUpdate);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
