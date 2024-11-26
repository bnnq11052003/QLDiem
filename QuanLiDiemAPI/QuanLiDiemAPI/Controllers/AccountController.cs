using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiDiemAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLiDiemAPI.Controllers
{
    [Route("api/APIQLDIEM")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SqlnewContext _context;

        public AccountController(SqlnewContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Account/GetAllAcount")]
        public async Task<IActionResult> getLogin()
        {
            var list = await _context.Taikhoans.ToListAsync();
            if (list == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(list);
            }

        }
        [HttpGet]
        [Route("Account/{username}&{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == null || password == null)
            {
                return BadRequest();
            }
            var check = await _context.Taikhoans.FirstAsync(s => s.MaGv == username & s.MatKhau == password);
            if (check == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(check);
            }

        }
        [HttpPut]
        [Route("Account/Edit")]
        public async Task<IActionResult> EditAccount(Taikhoan taikhoan)
        {
            if (taikhoan.MaGv == null)
            {
                return NotFound();
            }
            var account = await _context.Taikhoans.FindAsync(taikhoan.MaGv);
            if (account == null)
            {
                return BadRequest();
            }
            else
            {

                account.MatKhau = taikhoan.MatKhau;
                account.Loai = taikhoan.Loai;
                _context.Taikhoans.Update(account);
                _context.SaveChanges();
                return Ok();
            }

        }
        [HttpPost]
        [Route("Account/Create")]
        public async Task<IActionResult> CreateAccount(Taikhoan account)
        {
            var message = "Dữ liệu không hợp lệ.";
            if (account == null)
            {

                return StatusCode(409, message);
            }
            else
            {
                var check = await _context.Giangviens.FindAsync(account.MaGv);
                var check2 = await _context.Taikhoans.FindAsync(account.MaGv);
                if (check != null && check2 == null)
                {
                    _context.Taikhoans.Add(account);
                    await _context.SaveChangesAsync();
                    return Ok(account);
                }
                else
                {

                    return StatusCode(409, message);
                }
            }

        }

    }
}
