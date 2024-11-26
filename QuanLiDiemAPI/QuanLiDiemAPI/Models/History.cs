using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLiDiemAPI.Models;

public partial class History
{
    public string? Magv { get; set; }

    public string? Noidung { get; set; }

    public string? Thoigian { get; set; }

    [Key]
    public int Id { get; set; }
}
