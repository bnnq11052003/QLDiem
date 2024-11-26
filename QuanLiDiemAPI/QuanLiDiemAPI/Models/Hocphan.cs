using System;
using System.Collections.Generic;

namespace QuanLiDiemAPI.Models;

public partial class Hocphan
{
    public string MaHp { get; set; } = null!;

    public string? TenHp { get; set; }

    public int? SoTc { get; set; }

    public double? HeSoCc { get; set; }

    public double? HeSoGk { get; set; }

    public double? HeSoCk { get; set; }

    public string? KyHoc { get; set; }


}
