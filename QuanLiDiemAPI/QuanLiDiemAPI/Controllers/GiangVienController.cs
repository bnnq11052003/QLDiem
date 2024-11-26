using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiDiemAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLiDiemAPI.Controllers
{
    [Route("api/APIQLDIEM")]
    [ApiController]
    public class GiangVienController : ControllerBase
    {
        private readonly SqlnewContext _context;

        public GiangVienController(SqlnewContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GiangVien/GetAll")]
        public async Task<IActionResult> GetGiangVien()
        {
            var gv = await _context.Giangviens.ToListAsync();
            if (gv == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(gv);
            }

        }
        [HttpGet]
        [Route("GiangVien/{mGv}")]
        public async Task<IActionResult> GetGiangVien(string mGv)
        {
            if (mGv == null)
            {
                return BadRequest();
            }
            else
            {
                var gv = await _context.Giangviens.FindAsync(mGv);
                if (gv == null)
                {
                    return BadRequest();
                }

                return Ok(gv);
            }

        }
        [HttpPost]
        [Route("GiangVien/AddGv")]
        public async Task<IActionResult> ThemGiangVien([FromBody] Giangvien giangvien)
        {
            if (giangvien.MaGv != null)
            {
                var check = await _context.Giangviens.FindAsync(giangvien.MaGv);
                var listhp = await _context.Hocphans.Where(i => i.MaHp == giangvien.MaHp).ToListAsync();
                if (listhp.Count < 0)
                {
                    return BadRequest();
                }
                if (check == null)
                {
                    _context.Giangviens.Add(giangvien);
                    await _context.SaveChangesAsync();
                    return Ok();

                }
                else if (check != null)
                {
                    return BadRequest();
                }
            }
            else if (giangvien == null)
            {
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("GiangVien/Edit/{magv}")]
        public async Task<IActionResult> EditGv(string magv, Giangvien giangvien)
        {
            if (magv == null && magv == giangvien.MaGv)
            {
                return BadRequest();
            }

            else
            {
                var check = await _context.Giangviens.FindAsync(giangvien.MaGv);
                var check1 = await _context.Hocphans.FindAsync(giangvien.MaHp);
                if (check == null)
                {
                    return NotFound();
                }
                else
                {
                    if (check1 != null)
                    {
                        check.HoTen = giangvien.HoTen;
                        check.NgaySinh = giangvien.NgaySinh;
                        check.DiaChi = giangvien.DiaChi;
                        check.GioiTinh = giangvien.GioiTinh;
                        check.Sdt = giangvien.Sdt;
                        check.MaHp = giangvien.MaHp;
                        _context.Giangviens.Update(check);
                        await _context.SaveChangesAsync();
                        return Ok(check);
                    }
                    return BadRequest();

                }
            }

        }
        [HttpDelete]
        [Route("GiangVien/DeleteGv/{magv}")]
        public async Task<IActionResult> DeleteGv(string magv)
        {
            if (magv != null)
            {
                var giangvien = _context.Giangviens.FirstOrDefault(i => i.MaGv == magv);
                if (giangvien == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Giangviens.Remove(giangvien);
                    await _context.SaveChangesAsync();
                    return Ok(giangvien);
                }
            }
            return BadRequest();
        }

    }
}
