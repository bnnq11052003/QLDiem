//using Azure.Core;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualBasic;
//using QuanLiDiemAPI.Models;

//namespace QuanLiDiemAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]

//    public class APIQLDIEMController : ControllerBase
//    {
//        private readonly SqlnewContext _context;

//        public APIQLDIEMController(SqlnewContext context)
//        {
//            _context = context;
//        }

//        //  API về bảng Account

//        [HttpGet]
//        [Route("Account/GetAllAcount")]
//        public async Task<IActionResult> getLogin()
//        {
//            var list = await _context.Taikhoans.ToListAsync();
//            if (list == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(list);
//            }

//        }
//        [HttpGet]
//        [Route("Account/{username}&{password}")]
//        public async Task<IActionResult> Login(string username, string password)
//        {
//            if (username == null || password == null)
//            {
//                return BadRequest();
//            }
//            var check = await _context.Taikhoans.FirstAsync(s => s.MaGv == username & s.MatKhau == password);
//            if (check == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(check);
//            }

//        }
//        [HttpPut]
//        [Route("Account/Edit")]
//        public async Task<IActionResult> EditAccount(Taikhoan taikhoan)
//        {
//            if (taikhoan.MaGv == null)
//            {
//                return NotFound();
//            }
//            var account = await _context.Taikhoans.FindAsync(taikhoan.MaGv);
//            if (account == null)
//            {
//                return BadRequest();
//            }
//            else
//            {

//                account.MatKhau = taikhoan.MatKhau;
//                account.Loai = taikhoan.Loai;
//                _context.Taikhoans.Update(account);
//                _context.SaveChanges();
//                return Ok();
//            }

//        }
//        [HttpPost]
//        [Route("Account/Create")]
//        public async Task<IActionResult> CreateAccount(Taikhoan account)
//        {
//            var message = "Dữ liệu không hợp lệ.";
//            if (account == null)
//            {

//                return StatusCode(409, message);
//            }
//            else
//            {
//                var check = await _context.Giangviens.FindAsync(account.MaGv);
//                var check2 = await _context.Taikhoans.FindAsync(account.MaGv);
//                if (check != null && check2 == null)
//                {
//                    _context.Taikhoans.Add(account);
//                    await _context.SaveChangesAsync();
//                    return Ok(account);
//                }
//                else
//                {

//                    return StatusCode(409, message);
//                }
//            }

//        }



//        //  API về bảng SinhVien
//        [HttpGet]
//        [Route("SinhVien/GetAll")]
//        public async Task<IActionResult> GetAll()
//        {
//            var sinhvien = await _context.Sinhviens.OrderBy(sv => sv.MaSv).ToListAsync();
//            if (sinhvien == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(sinhvien);
//            }
//        }
//     /*   [HttpGet]
//        [Route("SinhVien/GetSV/{msv}/{magv}")]
//        public async Task<IActionResult> Getsvmsv(string msv, string magv)
//        {
//            var mahp = _context.Giangviens.FirstOrDefault(x => x.MaGv == magv).MaHp;
//            var msvarr = _context.Diems.Where(s => s.MaHp == mahp).Select(sv => sv.MaSv).ToList();
//            var sinhvien = _context.Sinhviens.Where(x => msvarr.Contains(x.MaSv) && (x.MaSv.Contains(msv) || x.HoTen.Contains(msv))).ToList();
//            if (sinhvien == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(sinhvien);
//            }
//        }
//          */  
//        [HttpGet]
//        [Route("SinhVien/GetSVmgv/{masv}")]
//        public async Task<IActionResult> GetsvMhp(string masv)
//        {

//            var sv = _context.Sinhviens.Where(s => s.MaSv ==masv).ToList();
//            if (sv == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(sv);
//            }
//        }
//        [HttpDelete]
//        [Route("SinhVien/DeleteSv/{msv}")]
//        public async Task<IActionResult> DeleteSv(string msv)
//        {
//            var sinhvien = _context.Sinhviens.FirstOrDefault(i => i.MaSv == msv);
//            if (sinhvien != null)
//            {

//                if (sinhvien == null)
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    _context.Remove(sinhvien);
//                    await _context.SaveChangesAsync();
//                    return Ok();
//                }
//            }
//            return BadRequest();
//        }
//        [HttpPost]
//        [Route("SinhVien/AddSv")]
//        public async Task<IActionResult> ThemSinhVien(Sinhvien sinhvien)
//        {
//            if (sinhvien.MaSv != null)
//            {
//                var check = await _context.Sinhviens.FindAsync(sinhvien.MaSv);
//                if (check == null)
//                {
//                    _context.Sinhviens.Add(sinhvien);
//                    await _context.SaveChangesAsync();
//                    return Ok();

//                }
//                else if (check != null)
//                {
//                    return NotFound();
//                }
//            }
//            else if (sinhvien == null)
//            {
//                return NotFound();
//            }
//            return BadRequest();
//        }
//        [HttpPut]
//        [Route("SinhVien/Edit")]
//        public async Task<IActionResult> Edit(Sinhvien sinhvien)
//        {
//            var check = await _context.Sinhviens.FindAsync(sinhvien.MaSv);
//            if (check == null)
//            {
//                return BadRequest();
//            }

//            else
//            {

//                check.HoTen = sinhvien.HoTen;
//                check.NgaySinh = sinhvien.NgaySinh;
//                check.DiaChi = sinhvien.DiaChi;
//                check.GioiTinh = sinhvien.GioiTinh;
//                check.Sdt = sinhvien.Sdt;

//                _context.Sinhviens.Update(check);
//                await _context.SaveChangesAsync();
//                return Ok(check);

//            }

//        }
//        [HttpGet]
//        [Route("GiangVien/Search/{magv}")]
//        public async Task<IActionResult> search(string magv)
//        {
//            var giangvien = _context.Giangviens.Where(x => x.MaGv.Contains(magv) || x.HoTen.Contains(magv) || x.MaHp.Contains(magv));
//            if (giangvien == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(giangvien);
//            }
//        }
//        [HttpGet]
//        [Route("GiangVien/Get/{magv}")]
//        public async Task<IActionResult> getmagv(string magv)
//        {
//            var giangvien = await _context.Giangviens.Where(i => i.MaGv == magv).FirstAsync();
//            if (giangvien == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(giangvien);
//            }
//        }






//        //  API về bảng  Giảng viên 
//        [HttpGet]
//        [Route("GiangVien/GetAll")]
//        public async Task<IActionResult> GetGiangVien()
//        {
//            var gv = await _context.Giangviens.ToListAsync();
//            if (gv == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(gv);
//            }

//        }
//        [HttpGet]
//        [Route("GiangVien/{mGv}")]
//        public async Task<IActionResult> GetGiangVien(string mGv)
//        {
//            if (mGv == null)
//            {
//                return BadRequest();
//            }
//            else
//            {
//                var gv = await _context.Giangviens.FindAsync(mGv);
//                if (gv == null)
//                {
//                    return BadRequest();
//                }

//                return Ok(gv);
//            }

//        }
//        [HttpPost]
//        [Route("GiangVien/AddGv")]
//        public async Task<IActionResult> ThemGiangVien([FromBody] Giangvien giangvien)
//        {
//            if (giangvien.MaGv != null)
//            {
//                var check = await _context.Giangviens.FindAsync(giangvien.MaGv);
//                var listhp = await _context.Hocphans.Where(i => i.MaHp == giangvien.MaHp).ToListAsync();
//                if (listhp.Count < 0)
//                {
//                    return BadRequest();
//                }
//                if (check == null)
//                {
//                    _context.Giangviens.Add(giangvien);
//                    await _context.SaveChangesAsync();
//                    return Ok();

//                }
//                else if (check != null)
//                {
//                    return BadRequest();
//                }
//            }
//            else if (giangvien == null)
//            {
//                return NotFound();
//            }
//            return BadRequest();
//        }
//        [HttpPut]
//        [Route("GiangVien/Edit/{magv}")]
//        public async Task<IActionResult> EditGv(string magv, Giangvien giangvien)
//        {
//            if (magv == null && magv == giangvien.MaGv)
//            {
//                return BadRequest();
//            }

//            else
//            {
//                var check = await _context.Giangviens.FindAsync(giangvien.MaGv);
//                var check1 = await _context.Hocphans.FindAsync(giangvien.MaHp);
//                if (check == null)
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    if (check1 != null)
//                    {
//                        check.HoTen = giangvien.HoTen;
//                        check.NgaySinh = giangvien.NgaySinh;
//                        check.DiaChi = giangvien.DiaChi;
//                        check.GioiTinh = giangvien.GioiTinh;
//                        check.Sdt = giangvien.Sdt;
//                        check.MaHp = giangvien.MaHp;
//                        _context.Giangviens.Update(check);
//                        await _context.SaveChangesAsync();
//                        return Ok(check);
//                    }
//                    return BadRequest();

//                }
//            }

//        }
//        [HttpDelete]
//        [Route("GiangVien/DeleteGv/{magv}")]
//        public async Task<IActionResult> DeleteGv(string magv)
//        {
//            if (magv != null)
//            {
//                var giangvien = _context.Giangviens.FirstOrDefault(i => i.MaGv == magv);
//                if (giangvien == null)
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    _context.Giangviens.Remove(giangvien);
//                    await _context.SaveChangesAsync();
//                    return Ok(giangvien);
//                }
//            }
//            return BadRequest();
//        }



//        //  API về bảng HocPhan

//        [HttpGet]
//        [Route("HocPhan/getAll")]
//        public async Task<IActionResult> getallhocphan()
//        {
//            var hocphan = _context.Hocphans.ToList();
//            if (hocphan.Count == 0)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(hocphan);
//            }
//        }
//        [HttpGet]
//        [Route("HocPhan/{mhp}")]
//        public async Task<IActionResult> getmhp(string mhp)
//        {
//            var hocphan = _context.Hocphans.FirstOrDefault(x => x.MaHp == mhp);
//            if (hocphan == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(hocphan);
//            }
//        }
//        [HttpPost]
//        [Route("HocPhan/them")]
//        public async Task<IActionResult> themhocphan(Hocphan hp)
//        {
//            var check1 = await _context.Hocphans.FindAsync(hp.MaHp);
//            var check = await _context.Hocphans.FindAsync(hp.TenHp);
//            var allhp = _context.Hocphans.ToList();

//            if (check1 != null || check == null)
//            {
//                return Content("Học phần này đã tồn tại");
//            }
//            else
//            {
//                _context.Hocphans.Add(hp);
//                _context.SaveChanges();
//                return Ok("sucess");
//            }


//        }

//        [HttpPut]
//        [Route("HocPhan/{sua}")]
//        public async Task<IActionResult> updatehocphan(string mhp, Hocphan hp)
//        {
//            var check = await _context.Hocphans.FindAsync(mhp);
//            if (check == null)
//            {
//                return Content("Học phần này không tồn tại");
//            }
//            else
//            {
//                check.TenHp = hp.TenHp;
//                check.HeSoCc = hp.HeSoCc;
//                check.HeSoGk = hp.HeSoGk;
//                check.HeSoCc = hp.HeSoCc;
//                check.HeSoCk = hp.HeSoCk;
//                check.SoTc = hp.SoTc;
//                check.KyHoc = hp.KyHoc;
//                _context.Update(check);
//                _context.SaveChanges();
//                return Ok(check);
//            }
//        }
//        [HttpDelete]
//        [Route("HocPhan/delete/{mhp}")]
//        public async Task<IActionResult> deletehocphan(string mhp)
//        {
//            var check = await _context.Hocphans.FindAsync(mhp);
//            if (check == null)
//            {
//                return Content("Học phần này không tồn tại");
//            }
//            else
//            {
//                _context.Remove(check);
//                _context.SaveChanges();
//                return Ok();
//            }
//        }




//        //  API về bảng Diem

//        [HttpGet]
//        [Route("Diem/getAll")]
//        public async Task<IActionResult> getalldiem()
//        {
//            var diem = _context.Diems.OrderBy(diem => diem.MaHp).ToList();
//            if (diem.Count == 0)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(diem);
//            }
//        }
//        [HttpGet]
//        [Route("Diem/getmsv/{msv}/{magv}")]
//        public async Task<IActionResult> getdiemmsv(string msv, string magv)
//        {
//            var gv = _context.Giangviens.FirstOrDefault(s => s.MaGv == magv).MaHp;
//            var diem = _context.Diems.Where(x => x.MaHp==gv &&(x.MaSv.Contains(msv) || x.MaHp.Contains(msv) || x.XepLoai.Contains(msv))).ToList();
//            if (diem.Count == 0)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(diem);
//            }
//        }
//        [HttpGet]
//        [Route("Diem/getmahp/{mahp}")]
//        public async Task<IActionResult> getdiemmahp(string mahp)
//        {
//            var diem = _context.Diems.Where(x => x.MaHp == mahp).ToList();
//            if (diem.Count == 0)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(diem);
//            }
//        }
//        [HttpGet]
//        [Route("Diem/getdiemmahp/{magv}")]
//        public async Task<IActionResult> getmahp(string magv)
//        {
//            var gv = _context.Giangviens.FirstOrDefault(s => s.MaGv == magv).MaHp;
//            var diemhp = _context.Diems.Where(x => x.MaHp == gv).ToList();
//            if (diemhp == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(diemhp);
//            }

//        }
//        [HttpPost]
//        [Route("Diem/them")]
//        public async Task<IActionResult> themdiem(Diem diemadd)
//        {
//            var checkmhp = await _context.Hocphans.FindAsync(diemadd.MaHp);
//            var checkmsv = await _context.Sinhviens.FindAsync(diemadd.MaSv);
//            var checkdiem = _context.Diems.Where(x => x.MaSv == diemadd.MaSv && x.MaHp == diemadd.MaHp).ToList();




//            if (checkmhp == null || checkmsv == null)
//            {
//                return Content("Không có sinh viên hay học phần");
//            }
//            else if (checkdiem.Count != 0)
//            {
//                return Content("Sinh viên này đã có điểm này");
//            }
//            else
//            {
//                var diemtb = (diemadd.DiemCc * checkmhp.HeSoCc + diemadd.DiemGk * checkmhp.HeSoGk + diemadd.DiemCk * checkmhp.HeSoCk) / (checkmhp.HeSoCk + checkmhp.HeSoGk + checkmhp.HeSoCc);

//                var xloai = "";
//                if (diemtb < 6.5)
//                {
//                    xloai = "Trung bình";
//                }
//                else if (diemtb >= 6.5 && diemtb < 8)
//                {
//                    xloai = "Khá";
//                }
//                else if (diemtb >= 8 && diemtb < 9)
//                {
//                    xloai = "Giỏi";
//                }
//                else
//                {
//                    xloai = "Xuất sắc";
//                }

//                var add = new Diem
//                {
//                    MaHp = diemadd.MaHp,
//                    MaSv = diemadd.MaSv,
//                    DiemCc = diemadd.DiemCc,
//                    DiemGk = diemadd.DiemGk,
//                    DiemCk = diemadd.DiemCk,
//                    DiemTb = diemtb,
//                    XepLoai = xloai
//                };
//                _context.Diems.Add(add);
//                _context.SaveChanges();
//                return Ok("sucess");
//            }
//        }

//        [HttpPut]
//        [Route("Diem/edit")]
//        public async Task<IActionResult> updatediem(Diem diem)
//        {
//            var check = await _context.Diems.FindAsync(diem.MaHp, diem.MaSv);


//            if (check == null)
//            {
//                return Content("Chưa tồn tại điểm này");
//            }
//            else
//            {
//                var checkmhp = await _context.Hocphans.FindAsync(diem.MaHp);
//                var diemtb = (diem.DiemCc * checkmhp.HeSoCc + diem.DiemGk * checkmhp.HeSoGk + diem.DiemCk * checkmhp.HeSoCk) / (checkmhp.HeSoCk + checkmhp.HeSoGk + checkmhp.HeSoCc);

//                var xloai = "";
//                if (diemtb < 6.5)
//                {
//                    xloai = "Trung bình";
//                }
//                else if (diemtb >= 6.5 && diemtb < 8)
//                {
//                    xloai = "Khá";
//                }
//                else if (diemtb >= 8 && diemtb < 9)
//                {
//                    xloai = "Giỏi";
//                }
//                else
//                {
//                    xloai = "Xuất sắc";
//                }

//                check.DiemCc = diem.DiemCc;
//                check.DiemGk = diem.DiemGk;
//                check.DiemCk = diem.DiemCk;
//                check.DiemTb = diemtb;
//                check.XepLoai = xloai;

//                _context.Update(check);
//                _context.SaveChanges();
//                return Ok(check);
//            }
//        }




//        //  API về bảng History



//        [HttpPost]
//        [Route("History/add")]
//        public async Task<IActionResult> AddHistory( History his)
//        {
//            if (his == null)
//            {
//                return BadRequest("History object is null.");
//            }

//            var history = new History
//            {
//                Magv = his.Magv,
//                Noidung = his.Noidung,
//                Thoigian = his.Thoigian
//            };

//            try
//            {
//                _context.Histories.Add(history);
//                await _context.SaveChangesAsync();
//                return Ok(history);
//            }
//            catch (Exception ex)
//            {
                
//                return BadRequest();
//            }
//        }

//        [HttpGet]
//        [Route("History/get")]
//        public async Task<IActionResult> gethistory()
//        {
//            var his = await _context.Histories.ToListAsync();
//            if(his == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(his);
//            }
//        }
//        [HttpGet]
//        [Route("History/get/{mgv}")]
//        public async Task<IActionResult> gethistory(string mgv)
//        {
//            var his = await _context.Histories.Where(x => x.Magv.Contains(mgv)).ToListAsync();
//            if (his == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                return Ok(his);
//            }
//        }
//        [HttpDelete]
//        [Route("History/delete/{id}")]
//        public async Task<IActionResult> deletehistory( int id)
//        {
//            var his = await _context.Histories.Where(i => i.Id ==id).FirstAsync();
//            if (his == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                 _context.Histories.Remove(his);
//                await _context.SaveChangesAsync();
//                return Ok(his);
//            }

//        }

//        [HttpDelete]
//        [Route("History/deleteall")]
//        public async Task<IActionResult> deleteallhistory()
//        {
//            var his = await _context.Histories.ToListAsync();
//            if (his == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                for (int i=0; i < his.Count; i++)
//                {
//                    _context.Histories.Remove(his[i]);
//                }
                
//                await _context.SaveChangesAsync();
//                return Ok(his);
//            }

//        }
//    }
//}
