using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiDiemAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLiDiemAPI.Controllers
{
    [Route("api/APIQLDIEM")]
    [ApiController]
    public class SinhVientroller : ControllerBase
    {
        private readonly SqlnewContext _context;

        public SinhVientroller(SqlnewContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("SinhVien/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var sinhvien = await _context.Sinhviens.OrderBy(sv => sv.MaSv).ToListAsync();
            if (sinhvien == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sinhvien);
            }
        }
        [HttpGet]
        [Route("SinhVien/GetSVmgv/{masv}")]
        public async Task<IActionResult> GetsvMhp(string masv)
        {

            var sv = _context.Sinhviens.Where(s => s.MaSv == masv).ToList();
            if (sv == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sv);
            }
        }
        [HttpDelete]
        [Route("SinhVien/DeleteSv/{msv}")]
        public async Task<IActionResult> DeleteSv(string msv)
        {
            var sinhvien = _context.Sinhviens.FirstOrDefault(i => i.MaSv == msv);
            if (sinhvien != null)
            {

                if (sinhvien == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Remove(sinhvien);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("SinhVien/AddSv")]
        public async Task<IActionResult> ThemSinhVien(Sinhvien sinhvien)
        {
            if (sinhvien.MaSv != null)
            {
                var check = await _context.Sinhviens.FindAsync(sinhvien.MaSv);
                if (check == null)
                {
                    _context.Sinhviens.Add(sinhvien);
                    await _context.SaveChangesAsync();
                    return Ok();

                }
                else if (check != null)
                {
                    return NotFound();
                }
            }
            else if (sinhvien == null)
            {
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("SinhVien/Edit")]
        public async Task<IActionResult> Edit(Sinhvien sinhvien)
        {
            var check = await _context.Sinhviens.FindAsync(sinhvien.MaSv);
            if (check == null)
            {
                return BadRequest();
            }

            else
            {

                check.HoTen = sinhvien.HoTen;
                check.NgaySinh = sinhvien.NgaySinh;
                check.DiaChi = sinhvien.DiaChi;
                check.GioiTinh = sinhvien.GioiTinh;
                check.Sdt = sinhvien.Sdt;

                _context.Sinhviens.Update(check);
                await _context.SaveChangesAsync();
                return Ok(check);

            }

        }
        [HttpGet]
        [Route("GiangVien/Search/{magv}")]
        public async Task<IActionResult> search(string magv)
        {
            var giangvien = _context.Giangviens.Where(x => x.MaGv.Contains(magv) || x.HoTen.Contains(magv) || x.MaHp.Contains(magv));
            if (giangvien == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(giangvien);
            }
        }
        [HttpGet]
        [Route("GiangVien/Get/{magv}")]
        public async Task<IActionResult> getmagv(string magv)
        {
            var giangvien = await _context.Giangviens.Where(i => i.MaGv == magv).FirstAsync();
            if (giangvien == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(giangvien);
            }
        }
    }
}
