using Microsoft.AspNetCore.Mvc;
using QuanLiDiemAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLiDiemAPI.Controllers
{
    [Route("api/APIQLDIEM")]
    [ApiController]
    public class DiemController : ControllerBase
    {
        private readonly SqlnewContext _context;

        public DiemController(SqlnewContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Diem/getAll")]
        public async Task<IActionResult> getalldiem()
        {
            var diem = _context.Diems.OrderBy(diem => diem.MaHp).ToList();
            if (diem.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(diem);
            }
        }
        [HttpGet]
        [Route("Diem/getmsv/{msv}/{magv}")]
        public async Task<IActionResult> getdiemmsv(string msv, string magv)
        {
            var gv = _context.Giangviens.FirstOrDefault(s => s.MaGv == magv).MaHp;
            var diem = _context.Diems.Where(x => x.MaHp == gv && (x.MaSv.Contains(msv) || x.MaHp.Contains(msv) || x.XepLoai.Contains(msv))).ToList();
            if (diem.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(diem);
            }
        }
        [HttpGet]
        [Route("Diem/getmahp/{mahp}")]
        public async Task<IActionResult> getdiemmahp(string mahp)
        {
            var diem = _context.Diems.Where(x => x.MaHp == mahp).ToList();
            if (diem.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(diem);
            }
        }
        [HttpGet]
        [Route("Diem/getdiemmahp/{magv}")]
        public async Task<IActionResult> getmahp(string magv)
        {
            var gv = _context.Giangviens.FirstOrDefault(s => s.MaGv == magv).MaHp;
            var diemhp = _context.Diems.Where(x => x.MaHp == gv).ToList();
            if (diemhp == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(diemhp);
            }

        }
        [HttpPost]
        [Route("Diem/them")]
        public async Task<IActionResult> themdiem(Diem diemadd)
        {
            var checkmhp = await _context.Hocphans.FindAsync(diemadd.MaHp);
            var checkmsv = await _context.Sinhviens.FindAsync(diemadd.MaSv);
            var checkdiem = _context.Diems.Where(x => x.MaSv == diemadd.MaSv && x.MaHp == diemadd.MaHp).ToList();




            if (checkmhp == null || checkmsv == null)
            {
                return Content("Không có sinh viên hay học phần");
            }
            else if (checkdiem.Count != 0)
            {
                return Content("Sinh viên này đã có điểm này");
            }
            else
            {
                var diemtb = (diemadd.DiemCc * checkmhp.HeSoCc + diemadd.DiemGk * checkmhp.HeSoGk + diemadd.DiemCk * checkmhp.HeSoCk) / (checkmhp.HeSoCk + checkmhp.HeSoGk + checkmhp.HeSoCc);

                var xloai = "";
                if (diemtb < 6.5)
                {
                    xloai = "Trung bình";
                }
                else if (diemtb >= 6.5 && diemtb < 8)
                {
                    xloai = "Khá";
                }
                else if (diemtb >= 8 && diemtb < 9)
                {
                    xloai = "Giỏi";
                }
                else
                {
                    xloai = "Xuất sắc";
                }

                var add = new Diem
                {
                    MaHp = diemadd.MaHp,
                    MaSv = diemadd.MaSv,
                    DiemCc = diemadd.DiemCc,
                    DiemGk = diemadd.DiemGk,
                    DiemCk = diemadd.DiemCk,
                    DiemTb = diemtb,
                    XepLoai = xloai
                };
                _context.Diems.Add(add);
                _context.SaveChanges();
                return Ok("sucess");
            }
        }

        [HttpPut]
        [Route("Diem/edit")]
        public async Task<IActionResult> updatediem(Diem diem)
        {
            var check = await _context.Diems.FindAsync(diem.MaHp, diem.MaSv);


            if (check == null)
            {
                return Content("Chưa tồn tại điểm này");
            }
            else
            {
                var checkmhp = await _context.Hocphans.FindAsync(diem.MaHp);
                var diemtb = (diem.DiemCc * checkmhp.HeSoCc + diem.DiemGk * checkmhp.HeSoGk + diem.DiemCk * checkmhp.HeSoCk) / (checkmhp.HeSoCk + checkmhp.HeSoGk + checkmhp.HeSoCc);

                var xloai = "";
                if (diemtb < 6.5)
                {
                    xloai = "Trung bình";
                }
                else if (diemtb >= 6.5 && diemtb < 8)
                {
                    xloai = "Khá";
                }
                else if (diemtb >= 8 && diemtb < 9)
                {
                    xloai = "Giỏi";
                }
                else
                {
                    xloai = "Xuất sắc";
                }

                check.DiemCc = diem.DiemCc;
                check.DiemGk = diem.DiemGk;
                check.DiemCk = diem.DiemCk;
                check.DiemTb = diemtb;
                check.XepLoai = xloai;

                _context.Update(check);
                _context.SaveChanges();
                return Ok(check);
            }
        }


    }
}
