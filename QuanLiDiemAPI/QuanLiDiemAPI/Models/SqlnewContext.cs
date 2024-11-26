using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLiDiemAPI.Models;

public partial class SqlnewContext : DbContext
{
    public SqlnewContext()
    {
    }

    public SqlnewContext(DbContextOptions<SqlnewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Diem> Diems { get; set; }

    public virtual DbSet<Giangvien> Giangviens { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Hocphan> Hocphans { get; set; }

    public virtual DbSet<Sinhvien> Sinhviens { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=sqlver;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => new { e.MaHp, e.MaSv }).HasName("PK__DIEM__2D9C33343F976705");

            entity.ToTable("DIEM");

            entity.Property(e => e.MaHp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maHP");
            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maSV");
            entity.Property(e => e.DiemCc).HasColumnName("diemCC");
            entity.Property(e => e.DiemCk).HasColumnName("diemCK");
            entity.Property(e => e.DiemGk).HasColumnName("diemGK");
            entity.Property(e => e.DiemTb).HasColumnName("diemTB");
            entity.Property(e => e.XepLoai)
                .HasMaxLength(20)
                .HasColumnName("xepLoai");

           
        });

        modelBuilder.Entity<Giangvien>(entity =>
        {
            entity.HasKey(e => e.MaGv).HasName("PK__GIANGVIE__7A3E2D7591356048");

            entity.ToTable("GIANGVIEN");

            entity.Property(e => e.MaGv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maGV");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(500)
                .HasColumnName("diaChi");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(3)
                .HasColumnName("gioiTinh");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("hoTen");
            entity.Property(e => e.MaHp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maHP");
            entity.Property(e => e.NgaySinh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ngaySinh");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");

           
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__History__3213E83FC1659EC5");

            entity.ToTable("History");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Magv)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Noidung)
                .HasMaxLength(50)
                .HasColumnName("noidung");
            entity.Property(e => e.Thoigian)   
                .HasMaxLength(500)
                .HasColumnName("thoigian");
        });

        modelBuilder.Entity<Hocphan>(entity =>
        {
            entity.HasKey(e => e.MaHp).HasName("PK__HOCPHAN__7A3E1492AA7AAFBC");

            entity.ToTable("HOCPHAN");

            entity.Property(e => e.MaHp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maHP");
            entity.Property(e => e.HeSoCc).HasColumnName("heSoCC");
            entity.Property(e => e.HeSoCk).HasColumnName("heSoCK");
            entity.Property(e => e.HeSoGk).HasColumnName("heSoGK");
            entity.Property(e => e.KyHoc)
                .HasMaxLength(10)
                .HasColumnName("kyHoc");
            entity.Property(e => e.SoTc).HasColumnName("soTC");
            entity.Property(e => e.TenHp)
                .HasMaxLength(200)
                .HasColumnName("tenHP");
        });

        modelBuilder.Entity<Sinhvien>(entity =>
        {
            entity.HasKey(e => e.MaSv).HasName("PK__SINHVIEN__7A227A648F48CAD2");

            entity.ToTable("SINHVIEN");

            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maSV");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(500)
                .HasColumnName("diaChi");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(3)
                .HasColumnName("gioiTinh");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("hoTen");
            entity.Property(e => e.NgaySinh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ngaySinh");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.MaGv).HasName("PK__TAIKHOAN__7A3E2D75D0C0A5DF");

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.MaGv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("maGV");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("matKhau");

           
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
