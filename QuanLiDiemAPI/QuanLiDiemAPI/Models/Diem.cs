using System;
using System.Collections.Generic;

namespace QuanLiDiemAPI.Models;

public partial class Diem
{
    public string MaHp { get; set; } = null!;

    public string MaSv { get; set; } = null!;

    public double? DiemCc { get; set; }

    public double? DiemGk { get; set; }

    public double? DiemCk { get; set; }

    public double? DiemTb { get; set; }

    public string? XepLoai { get; set; }


}
