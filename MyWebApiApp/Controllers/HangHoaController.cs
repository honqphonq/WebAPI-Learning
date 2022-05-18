using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> HangHoas = new List<HangHoa>()
        {
            new HangHoa{TenHangHoa = "Thuốc phiện", DonGia = 1000, MaHangHoa = Guid.NewGuid()},
            new HangHoa{TenHangHoa = "Thuốc phiện 2", DonGia = 1000,  MaHangHoa = Guid.NewGuid()},
            new HangHoa{TenHangHoa = "Thuốc phiện 3", DonGia = 1000,  MaHangHoa = Guid.NewGuid()},
        };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(HangHoas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var hanghoa = HangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound();
                }
                return Ok(hanghoa);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public IActionResult Create(HangHoaVM hanghoaVM)
        {
            var hanghoa = new HangHoa()
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hanghoaVM.TenHangHoa,
                DonGia = hanghoaVM.DonGia
            };
            HangHoas.Add(hanghoa);
            return Ok(new { Success = true, Data = hanghoa });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa HangHoaEdit)
        {
            try
            {
                var hanghoa = HangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound();
                }

                if(id != hanghoa.MaHangHoa.ToString())
                {
                    return BadRequest();
                }
                //Update
                hanghoa.TenHangHoa = HangHoaEdit.TenHangHoa;
                hanghoa.DonGia = HangHoaEdit.DonGia;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                var hanghoa = HangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound();
                }
                HangHoas.Remove(hanghoa);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
