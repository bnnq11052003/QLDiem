using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiDiemAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLiDiemAPI.Controllers
{
    [Route("api/APIQLDIEM")]
    [ApiController]
    public class HocPhanController : ControllerBase
    {
        private readonly SqlnewContext _context;

        public HocPhanController(SqlnewContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("HocPhan/getAll")]
        public async Task<IActionResult> getallhocphan()
        {
            var hocphan = _context.Hocphans.ToList();
            if (hocphan.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(hocphan);
            }
        }
        [HttpGet]
        [Route("HocPhan/{mhp}")]
        public async Task<IActionResult> getmhp(string mhp)
        {
            var hocphan = _context.Hocphans.FirstOrDefault(x => x.MaHp == mhp);
            if (hocphan == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(hocphan);
            }
        }
        [HttpPost]
        [Route("HocPhan/them")]
        public async Task<IActionResult> themhocphan(Hocphan hp)
        {
            var check1 = await _context.Hocphans.FindAsync(hp.MaHp);
            var check = await _context.Hocphans.FindAsync(hp.TenHp);
            var allhp = _context.Hocphans.ToList();

            if (check1 != null || check == null)
            {
                return Content("Học phần này đã tồn tại");
            }
            else
            {
                _context.Hocphans.Add(hp);
                _context.SaveChanges();
                return Ok("sucess");
            }


        }

        [HttpPut]
        [Route("HocPhan/{sua}")]
        public async Task<IActionResult> updatehocphan(string mhp, Hocphan hp)
        {
            var check = await _context.Hocphans.FindAsync(mhp);
            if (check == null)
            {
                return Content("Học phần này không tồn tại");
            }
            else
            {
                check.TenHp = hp.TenHp;
                check.HeSoCc = hp.HeSoCc;
                check.HeSoGk = hp.HeSoGk;
                check.HeSoCc = hp.HeSoCc;
                check.HeSoCk = hp.HeSoCk;
                check.SoTc = hp.SoTc;
                check.KyHoc = hp.KyHoc;
                _context.Update(check);
                _context.SaveChanges();
                return Ok(check);
            }
        }
        [HttpDelete]
        [Route("HocPhan/delete/{mhp}")]
        public async Task<IActionResult> deletehocphan(string mhp)
        {
            var check = await _context.Hocphans.FindAsync(mhp);
            if (check == null)
            {
                return Content("Học phần này không tồn tại");
            }
            else
            {
                _context.Remove(check);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
