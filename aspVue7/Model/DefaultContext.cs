using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aspVue7.Model
{
    public partial class DefaultContext : DbContext
    {
        public DefaultContext()
        {
        }

        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        {
        }

        public virtual DbSet<QforOrderReportAs> QforOrderReportAs { get; set; }
        public virtual DbSet<TblAndonInfor> TblAndonInfor { get; set; }
        public virtual DbSet<TblAndonPermissions> TblAndonPermissions { get; set; }
        public virtual DbSet<TblAndonPushTime> TblAndonPushTime { get; set; }
        public virtual DbSet<TblAndonStatus> TblAndonStatus { get; set; }
        public virtual DbSet<TblAndonType> TblAndonType { get; set; }
        public virtual DbSet<TblAndonUser> TblAndonUser { get; set; }
        public virtual DbSet<TblAndonUserPermissions> TblAndonUserPermissions { get; set; }
        public virtual DbSet<TblEffChart> TblEffChart { get; set; }
        public virtual DbSet<TblFttchart> TblFttchart { get; set; }
        public virtual DbSet<TblOeechart> TblOeechart { get; set; }
        public virtual DbSet<TblOrderReportTarget> TblOrderReportTarget { get; set; }
        public virtual DbSet<TblOrdersub1> TblOrdersub1 { get; set; }
        public virtual DbSet<TblPdunit1> TblPdunit1 { get; set; }
        public virtual DbSet<TestTbl> TestTbl { get; set; }
        public virtual DbSet<Testfalsk> Testfalsk { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NBELTOFF0007;Database=BorgWarnerMisSQL;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QforOrderReportAs>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qforOrderReportAS");

                entity.Property(e => e.DtRTarget).HasColumnName("DtR_Target");

                entity.Property(e => e.EaTarget).HasColumnName("EA_Target");

                entity.Property(e => e.EffTarget).HasColumnName("Eff_Target");

                entity.Property(e => e.FttTarget).HasColumnName("FTT_Target");

                entity.Property(e => e.Oee).HasColumnName("OEE");

                entity.Property(e => e.OeeTarget).HasColumnName("OEE_Target");

                entity.Property(e => e.UpphTarget).HasColumnName("Upph_Target");

                entity.Property(e => e.作业完工时间).HasColumnType("datetime");

                entity.Property(e => e.加工单元).HasMaxLength(16);

                entity.Property(e => e.工作组).HasMaxLength(25);

                entity.Property(e => e.报工类别).HasMaxLength(8);

                entity.Property(e => e.报工编号)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.日)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.班次日期).HasColumnType("datetime");

                entity.Property(e => e.调试结束时间).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblAndonInfor>(entity =>
            {
                entity.HasKey(e => e.序号);

                entity.ToTable("tblAndonInfor");

                entity.Property(e => e.不合格品单号).HasMaxLength(50);

                entity.Property(e => e.不能解决原因).HasMaxLength(200);

                entity.Property(e => e.关闭人).HasMaxLength(16);

                entity.Property(e => e.关闭时间).HasColumnType("datetime");

                entity.Property(e => e.处理人).HasMaxLength(16);

                entity.Property(e => e.处理时间).HasColumnType("datetime");

                entity.Property(e => e.安灯编号).HasMaxLength(16);

                entity.Property(e => e.工作中心).HasMaxLength(50);

                entity.Property(e => e.报修单号).HasMaxLength(50);

                entity.Property(e => e.报工单号).HasMaxLength(50);

                entity.Property(e => e.按灯人).HasMaxLength(16);

                entity.Property(e => e.按灯时间).HasColumnType("datetime");

                entity.Property(e => e.提醒时间).HasColumnType("datetime");

                entity.Property(e => e.是否停线).HasMaxLength(16);

                entity.Property(e => e.确认人).HasMaxLength(16);

                entity.Property(e => e.确认时间).HasColumnType("datetime");

                entity.Property(e => e.行动计划).HasMaxLength(200);

                entity.Property(e => e.计划完成日期).HasColumnType("datetime");

                entity.Property(e => e.设备编号).HasMaxLength(16);

                entity.Property(e => e.退回人).HasMaxLength(16);

                entity.Property(e => e.退回原因).HasMaxLength(200);

                entity.Property(e => e.退回时间).HasColumnType("datetime");

                entity.Property(e => e.问题描述).HasMaxLength(200);
            });

            modelBuilder.Entity<TblAndonPermissions>(entity =>
            {
                entity.HasKey(e => e.序号);

                entity.ToTable("tblAndonPermissions");

                entity.Property(e => e.权限名称).HasMaxLength(50);

                entity.Property(e => e.权限编号).HasMaxLength(50);
            });

            modelBuilder.Entity<TblAndonPushTime>(entity =>
            {
                entity.HasKey(e => e.序号);

                entity.ToTable("tblAndonPushTime");

                entity.Property(e => e.推送人员职级).HasMaxLength(100);

                entity.Property(e => e.操作流程).HasMaxLength(10);

                entity.Property(e => e.是否邮件通知).HasMaxLength(10);

                entity.Property(e => e.状态).HasMaxLength(50);
            });

            modelBuilder.Entity<TblAndonStatus>(entity =>
            {
                entity.HasKey(e => e.优先级);

                entity.ToTable("tblAndonStatus");

                entity.Property(e => e.优先级).ValueGeneratedNever();

                entity.Property(e => e.状态描述).HasMaxLength(8);
            });

            modelBuilder.Entity<TblAndonType>(entity =>
            {
                entity.HasKey(e => e.优先级);

                entity.ToTable("tblAndonType");

                entity.Property(e => e.优先级).ValueGeneratedNever();

                entity.Property(e => e.安灯图片a)
                    .HasColumnName("安灯图片A")
                    .HasColumnType("image");

                entity.Property(e => e.安灯图片m)
                    .HasColumnName("安灯图片M")
                    .HasColumnType("image");

                entity.Property(e => e.安灯图片p)
                    .HasColumnName("安灯图片P")
                    .HasColumnType("image");

                entity.Property(e => e.类别描述).HasMaxLength(8);

                entity.Property(e => e.类别英文描述).HasMaxLength(50);
            });

            modelBuilder.Entity<TblAndonUser>(entity =>
            {
                entity.HasKey(e => e.序号);

                entity.ToTable("tblAndonUser");

                entity.Property(e => e.创建时间).HasColumnType("datetime");

                entity.Property(e => e.姓名).HasMaxLength(20);

                entity.Property(e => e.安灯类别).HasMaxLength(20);

                entity.Property(e => e.密码).HasMaxLength(20);

                entity.Property(e => e.工号).HasMaxLength(20);

                entity.Property(e => e.所属车间).HasMaxLength(50);

                entity.Property(e => e.手机号).HasMaxLength(20);

                entity.Property(e => e.是否相关人员).HasMaxLength(20);

                entity.Property(e => e.电子邮箱).HasMaxLength(50);

                entity.Property(e => e.职务).HasMaxLength(20);

                entity.Property(e => e.职级).HasMaxLength(20);

                entity.Property(e => e.账号).HasMaxLength(20);

                entity.Property(e => e.部门名称).HasMaxLength(20);
            });

            modelBuilder.Entity<TblAndonUserPermissions>(entity =>
            {
                entity.HasKey(e => e.序号);

                entity.ToTable("tblAndonUserPermissions");
            });

            modelBuilder.Entity<TblEffChart>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblEffChart");

                entity.Property(e => e.EffTarget).HasColumnName("Eff_target");

                entity.Property(e => e.周).HasMaxLength(4);

                entity.Property(e => e.日).HasMaxLength(4);

                entity.Property(e => e.月).HasMaxLength(4);
            });

            modelBuilder.Entity<TblFttchart>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblFTTChart");

                entity.Property(e => e.FttTarget).HasColumnName("FTT_Target");

                entity.Property(e => e.周).HasMaxLength(4);

                entity.Property(e => e.日)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.月).HasMaxLength(4);
            });

            modelBuilder.Entity<TblOeechart>(entity =>
            {
                entity.ToTable("tblOEEChart");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OeeTarget).HasColumnName("OEE_Target");

                entity.Property(e => e.周).HasMaxLength(4);

                entity.Property(e => e.日).HasMaxLength(4);

                entity.Property(e => e.日期).HasColumnType("date");

                entity.Property(e => e.月).HasMaxLength(4);
            });

            modelBuilder.Entity<TblOrderReportTarget>(entity =>
            {
                entity.ToTable("tblOrderReportTarget");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DtRTarget).HasColumnName("DtR_Target");

                entity.Property(e => e.EaTarget).HasColumnName("EA_Target");

                entity.Property(e => e.EffTarget).HasColumnName("Eff_Target");

                entity.Property(e => e.FttTarget).HasColumnName("FTT_Target");

                entity.Property(e => e.OeeTarget).HasColumnName("OEE_Target");

                entity.Property(e => e.UpphTarget).HasColumnName("Upph_Target");

                entity.Property(e => e.所属车间).HasMaxLength(20);

                entity.Property(e => e.生产单元).HasMaxLength(20);
            });

            modelBuilder.Entity<TblOrdersub1>(entity =>
            {
                entity.HasKey(e => e.报工编号)
                    .HasName("PK_dbo_tblOrdersub1");

                entity.ToTable("tblOrdersub1");

                entity.Property(e => e.报工编号).HasMaxLength(25);

                entity.Property(e => e.不合格单编号).HasMaxLength(16);

                entity.Property(e => e.作业单号).HasMaxLength(16);

                entity.Property(e => e.作业完工时间).HasColumnType("datetime");

                entity.Property(e => e.作业开始时间).HasColumnType("datetime");

                entity.Property(e => e.创建人).HasMaxLength(8);

                entity.Property(e => e.创建日期).HasColumnType("datetime");

                entity.Property(e => e.加工人员).HasMaxLength(8);

                entity.Property(e => e.加工单元).HasMaxLength(16);

                entity.Property(e => e.回用批次号).HasMaxLength(25);

                entity.Property(e => e.工作组).HasMaxLength(30);

                entity.Property(e => e.工序名称).HasMaxLength(25);

                entity.Property(e => e.所属班组).HasMaxLength(16);

                entity.Property(e => e.所属车间).HasMaxLength(16);

                entity.Property(e => e.报工类别).HasMaxLength(8);

                entity.Property(e => e.物料描述).HasMaxLength(100);

                entity.Property(e => e.物料编号).HasMaxLength(16);

                entity.Property(e => e.班次日期).HasColumnType("datetime");

                entity.Property(e => e.生产日期).HasColumnType("date");

                entity.Property(e => e.补贴金额).HasColumnType("money");

                entity.Property(e => e.调试开始时间).HasColumnType("datetime");

                entity.Property(e => e.调试结束时间).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPdunit1>(entity =>
            {
                entity.HasKey(e => e.编号)
                    .HasName("PK_dbo_tblPDUnit1");

                entity.ToTable("tblPDUnit1");

                entity.Property(e => e.编号).ValueGeneratedNever();

                entity.Property(e => e.Oee).HasColumnName("OEE");

                entity.Property(e => e.工作组).HasMaxLength(25);

                entity.Property(e => e.所属车间).HasMaxLength(16);

                entity.Property(e => e.生产线单元)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TestTbl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("testTbl");

                entity.Property(e => e.Column1).HasColumnName("column1");

                entity.Property(e => e.Column2)
                    .HasColumnName("column2")
                    .HasMaxLength(50);

                entity.Property(e => e.Column3)
                    .HasColumnName("column3")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Testfalsk>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("testfalsk");

                entity.Property(e => e.Addr)
                    .HasColumnName("addr")
                    .HasMaxLength(255);

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Birthdate2)
                    .HasColumnName("birthdate2")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasColumnName("First name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("Last name")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Pwd)
                    .HasColumnName("pwd")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Usrname)
                    .HasColumnName("usrname")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
