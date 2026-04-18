using LibraryManagementSystem.Helper;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly LmsContext _context;

    public AccountController(LmsContext context)
    {
        _context = context;
    }

    // ================= LOGIN GET =================
    public IActionResult Login()
    {
        return View();
    }

    // ================= LOGIN POST =================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(VmLoginRequest model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Member খুঁজে বের করা
        var member = _context.Members
            .Include(x => x.MemberRoles)
                .ThenInclude(mr => mr.Role)  // Role তথ্যও নিয়ে আসা
            .FirstOrDefault(x => x.Email == model.Email);

        // Member exists কিনা চেক করা
        if (member == null)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // Account Active কিনা চেক করা (Optional)
        if ((bool)!member.IsActive)
        {
            ModelState.AddModelError("", "Your account is deactivated. Please contact administrator.");
            return View(model);
        }

        // পাসওয়ার্ড ভেরিফাই করা
        var hashedInputPassword = SecurityHelper.HashPassword(model.Password, member.Salt.ToString());

        if (member.PasswordHash != hashedInputPassword)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // ইউজার ইনফরমেশন সেশন এর জন্য প্রস্তুত করা
        var user = new VmLoginResponse
        {
            MemberId = member.Id,
            MemberName = member.Name,
            Email = member.Email,
            RoleId = member.MemberRoles?.FirstOrDefault()?.RoleId ?? 0,  // Null handling
            RoleName = member.MemberRoles?.FirstOrDefault()?.Role?.RoleName ?? "Member"
        };

        // সেশনে ইউজার ডাটা স্টোর করা
        SessionHelper.SetUser(HttpContext, user);

      

        // ডিফল্ট Redirect
        return RedirectToAction("Index", "Home");
    }
    // ================= LOGOUT =================
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
