using KioskServiceApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class AppDbContext : DbContext
{

    public DbSet<Device> Devices { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Assignment> Assignments { get; set; }

    public DbSet<ServiceAssignmentStats> vw_ServiceAssignmentStats { get; set; }
    public DbSet<UsedUnusedAssignments> vw_UsedUnusedAssignments { get; set; }
    public DbSet<KioskAssignmentCount> vw_KioskAssignmentCount { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=KioskServiceDB;Username=postgres;Password=1234");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceAssignmentStats>().HasNoKey().ToView("vw_ServiceAssignmentStats");
        modelBuilder.Entity<UsedUnusedAssignments>().HasNoKey().ToView("vw_UsedUnusedAssignments");
        modelBuilder.Entity<KioskAssignmentCount>().HasNoKey().ToView("vw_KioskAssignmentCount");
    }

    public void RefreshMaterializedViews()
    {
        using (var context = new AppDbContext())
        {
            // Refresh các Materialized View
            context.Database.ExecuteSqlRaw("REFRESH MATERIALIZED VIEW vw_ServiceAssignmentStats;");
            context.Database.ExecuteSqlRaw("REFRESH MATERIALIZED VIEW vw_UsedUnusedAssignments;");
            context.Database.ExecuteSqlRaw("REFRESH MATERIALIZED VIEW vw_KioskAssignmentCount;");
        }
    }
    public void AddNewAssignment(Assignment assignment)
    {
        using (var context = new AppDbContext())
        {
            // Thêm bản ghi mới vào bảng Assignments
            context.Assignments.Add(assignment);
            context.SaveChanges();

            // Cập nhật các Materialized Views
            RefreshMaterializedViews();
        }
    }

    public void UpdateDevice(Device device)
    {
        using (var context = new AppDbContext())
        {
            // Cập nhật thiết bị
            context.Devices.Update(device);
            context.SaveChanges();

            // Cập nhật các Materialized Views
            RefreshMaterializedViews();
        }
    }

    public void DeleteAssignment(string assignmentCode)
    {
        using (var context = new AppDbContext())
        {
            // Xóa bản ghi Assignments
            var assignment = context.Assignments.Find(assignmentCode);
            if (assignment != null)
            {
                context.Assignments.Remove(assignment);
                context.SaveChanges();
            }

            // Cập nhật các Materialized Views
            RefreshMaterializedViews();
        }
    }


}
