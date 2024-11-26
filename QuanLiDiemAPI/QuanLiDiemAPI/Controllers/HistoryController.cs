using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiDiemAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLiDiemAPI.Controllers
{
    [Route("api/APIQLDIEM")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly SqlnewContext _context;

        public HistoryController(SqlnewContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("History/add")]
        public async Task<IActionResult> AddHistory(History his)
        {
            if (his == null)
            {
                return BadRequest("History object is null.");
            }

            var history = new History
            {
                Magv = his.Magv,
                Noidung = his.Noidung,
                Thoigian = his.Thoigian
            };

            try
            {
                _context.Histories.Add(history);
                await _context.SaveChangesAsync();
                return Ok(history);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("History/get")]
        public async Task<IActionResult> gethistory()
        {
            var his = await _context.Histories.ToListAsync();
            if (his == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(his);
            }
        }
        [HttpGet]
        [Route("History/get/{mgv}")]
        public async Task<IActionResult> gethistory(string mgv)
        {
            var his = await _context.Histories.Where(x => x.Magv.Contains(mgv)).ToListAsync();
            if (his == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(his);
            }
        }
        [HttpDelete]
        [Route("History/delete/{id}")]
        public async Task<IActionResult> deletehistory(int id)
        {
            var his = await _context.Histories.Where(i => i.Id == id).FirstAsync();
            if (his == null)
            {
                return NotFound();
            }
            else
            {
                _context.Histories.Remove(his);
                await _context.SaveChangesAsync();
                return Ok(his);
            }

        }

        [HttpDelete]
        [Route("History/deleteall")]
        public async Task<IActionResult> deleteallhistory()
        {
            var his = await _context.Histories.ToListAsync();
            if (his == null)
            {
                return NotFound();
            }
            else
            {
                for (int i = 0; i < his.Count; i++)
                {
                    _context.Histories.Remove(his[i]);
                }

                await _context.SaveChangesAsync();
                return Ok(his);
            }

        }
    }
}
