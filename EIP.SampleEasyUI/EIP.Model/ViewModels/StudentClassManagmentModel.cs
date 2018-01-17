using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIP.Entity;
using CoreLand.Framework.Code;
namespace EIP.Model.ViewModels
{
    public class StudentClassManagmentModel
    {

        /// <summary>
        /// 班级ID
        /// </summary>
        public int RId { get; set; }

       /// <summary>
       /// 班级代码
       /// </summary>
       public string Remo_id { get; set; }

        ///<summary>
        ///班主任编号
        /// </summary>
        /// 
        public string MasterNum { get; set; }

        ///<summary>
        ///所属学院ID
        /// </summary>
        /// 
        public int CId { set; get; }
       
        /// <summary>
        /// 学生信息
        /// </summary>
        public IList<StudentManagmentModel>Grades { get; set; }
    }

    /// <summary>
    /// 学生信息
    /// </summary>
    public class StudentManagmentModel
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        public int SId { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>

        public int RId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public String Sex { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone_number { get; set; }

        /// <summary>
        /// 户籍所在地
        /// </summary>
        public string Hometown { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        
        public string Email { get; set; }

        ///<summary>
        ///学号
        /// </summary>
        /// 
        public string StudentId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool isRemove { get; set; }
    }

    //public class studenViewModel:Student
    //{
    //    /// <summary>
    //    /// 性别中文名
    //    /// </summary>
    //    public string SexName { get { return CodeManger.GetCodeText(CommonConstant.CODETYPE_SEXX, this.Sex); } }
    //}

    public class GradeViewModel:Grade
    {
        ///<summary>
        ///所在地域中文名
        /// </summary>
        ///
        public string AreaName { get; set; }
    

    /// <summary>
    /// 性别中文名
    /// </summary>
    public string SexName { get { return CodeManger.GetCodeText(CommonConstant.CODETYPE_SEX, this.Sex); } }

        ///<summary>
        ///所在班级名字
        /// </summary>
        ///
        public string Remo_id { get; set; }
    }
    public class entorViewModel : entor
    {
        /// <summary>
        /// 性别中文名
        /// </summary>
        public string SexName;
    }

}
