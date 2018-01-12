using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EIP.SystemManage.Entity;
namespace EIP.Entity
{
    public class SampContext : DbContext, IDisposable
    {
        #region ctor
        public SampContext()
            : base(CoreLand.Framework.Helpers.DbHelperSQL.ConnectionString)
        {

            Database.SetInitializer<SampContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
        #endregion

        #region 基本实体模型定义

        public DbSet<AppSettingEntity> AppSetting { get; set; }

        public DbSet<CodeMaster> CodeMaster { get; set; }


        public DbSet<Navigation> Navigation { get; set; }


        /// <summary>
        /// 数据字典
        ///</summary>
        public DbSet<AppCodeMaster> AppCodeMaster { get; set; }


        /// <summary>
        /// 系统配置
        ///</summary>
        public DbSet<AppConfig> AppConfig { get; set; }


        #endregion

        #region 业务实体模型定义

        /// <summary>
        /// 教室信息
        ///    
        ///</summary>
        public DbSet<Classroom> Classroom { get; set; }


        /// <summary>
        /// 学院信息
        ///    
        ///</summary>
        public DbSet<College> College { get; set; }


        /// <summary>
        /// 学生信息表
        ///</summary>
        public DbSet<Grade> Grade { get; set; }


        /// <summary>
        /// 课程信息表
        ///</summary>
        public DbSet<LessonInfo> LessonInfo { get; set; }


        /// <summary>
        /// 课程安排
        ///</summary>
        public DbSet<LessonSchedule> LessonSchedule { get; set; }


        /// <summary>
        /// 教师
        ///</summary>
        public DbSet<entor> entor { get; set; }


        /// <summary>
        /// 用来记录每个班级的任课教师
        ///</summary>
        public DbSet<entorToRemo> entorToRemo { get; set; }


        /// <summary>
        /// 班级
        ///</summary>
        public DbSet<Remo> Remo { get; set; }


        /// <summary>
        /// 用来记录地域相关属性
        ///</summary>
        public DbSet<Local> Local { get; set; }

        #endregion



        void IDisposable.Dispose()
        {
            base.Dispose();
        }
    }
}
