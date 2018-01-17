/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:34   创建人：Hujunyuan
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data;
using CoreLand.Framework;
using CoreLand.Framework.Data;
using CoreLand.Framework.Code;
using EIP.Model;
using EIP.Entity;
using EIP.Model.ViewModels;
using System.Text.RegularExpressions;
namespace EIP.Repository
{
    /// <summary>
    /// 学生信息表仓储
    /// </summary>
    public class GradeRepository : DefaultRepository<Grade>
    {

        public GradeRepository(IUnitOfWork unitOfWork, IRepositoryFactory factory)
            : base(unitOfWork, factory)
        {

        }

        /// <summary>
        /// 查询学生信息表
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<Grade> QueryGrade(QueryModel model, out int totalCount)
        {
            //查询数据
            var searchKey = (string.IsNullOrEmpty(model.Key) ? "%" : "%" + model.Key.Trim() + "%");
            string sql = "select * from dbo.Grade where LogicDeleteFlag=0 and StudentName like @p0 ";

            //分页查询必须要有排序字段
            model.SortField = string.IsNullOrEmpty(model.SortField) ? "SId" : model.SortField;

            var grades = this.LoadPageEntitiesBySql<Grade>(
                       model.PageIndex,
                       model.PageSize,
                       out totalCount,
                       sql,
                       model.SortField + " " + model.SortOrder,
                       searchKey
                       ).ToList();
            
            return grades;
        }

        /// <summary>
        /// 查询学生信息表带班级名
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<GradeViewModel> QueryGradeWithRemo_id(QueryModel model, out int totalCount)
        {
            //查询数据
            var searchKey = (string.IsNullOrEmpty(model.Key) ? "StudentName?%" :  model.Key.Trim() );
            string[] str = searchKey.Split('?');
            str[1] = '%' + str[1]+'%';
            if (Regex.IsMatch(str[1],"(%{2,}?)"))
                str[1] = "%";

            //去除特殊字符
            str[0] = EncodeStr(str[0]);
            searchKey = str[1];
            //string sql = "select Grade.*,Remo_id from dbo.Grade left join Remo on Remo.RId = Grade.RId  where dbo.Grade.LogicDeleteFlag=0  and Grade."+str[0]+ " like "+"@p0";
            //string sql = "select T.*, Local.LocalName from(select Grade.*,Remo_id from dbo.Grade left join Remo on Remo.RId = Grade.RId  where dbo.Grade.LogicDeleteFlag = 0 and Grade." + str[0] + " like " + "@p0) as T left join Local on T.Other = Local.LocalNum";
            string sql = "select T2.*,(Local.LocalName + T2.LocalName) as AreaName from(select T1.*, Local.LocalName + T1.ln1 as LocalName from(select T0.*, Local.LocalName as ln1 from(select Grade.*, Remo_id from dbo.Grade left join Remo on Remo.RId = Grade.RId  where dbo.Grade.LogicDeleteFlag = 0 and Grade." + str[0] + " like " + "@p0) as T0 left join Local on  T0.Other = Local.LocalNum) as T1 left join Local on LEFT(T1.Other, 4) = Local.LocalNum) as T2 left join Local on LEFT(T2.Other, 2) = Local.LocalNum";


            //分页查询必须要有排序字段
            model.SortField = string.IsNullOrEmpty(model.SortField) ? "SId" : model.SortField;

            var grades = this.LoadPageEntitiesBySql<GradeViewModel>(
                       model.PageIndex,
                       model.PageSize,
                       out totalCount,
                       sql,
                       model.SortField + " " + model.SortOrder,
                       searchKey
                       ).ToList();

            return grades;
        }
        /// <summary>
        /// 查询男生女生人数，班级总人数
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<CountManAndWoman> QueryCountManAndWoman(QueryModel model, out int totalCount)
        {
            //创建查询用sql语句
            string sql = "select Remo.RId,Remo.Remo_id,SUM(case Sex when 'M' then 1 else 0 end) " +
                "as manCount,SUM(case Sex when 'W' then 1 else 0 end) " +
                "as womanCount, SUM(1) as totalCount from Remo inner " +
                "join Grade on Remo.RId = Grade.RId group by Remo.RId,Remo.Remo_id";

            //分页查询排序字段
            model.SortField = string.IsNullOrEmpty(model.SortField) ? "Remo.RId" : model.SortField;

            var grades = this.LoadPageEntitiesBySql<CountManAndWoman>(
                       model.PageIndex,
                       model.PageSize,
                       out totalCount,
                       sql,
                       model.SortField + " " + model.SortOrder
                      // searchKey
                       ).ToList();

            return grades;
        }

        /// <summary>
        /// 根据班级模糊查询男生女生人数，班级总人数
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<CountManAndWoman> QueryCountManAndWomanUseLike(QueryModel model, out int totalCount)
        {
            var searchKey = (string.IsNullOrEmpty(model.Key) ? "%" : "%" + model.Key.Trim() + "%");
            //创建查询用sql语句
            string sql = "select * from (select Remo.RId,Remo.Remo_id,SUM(case Sex when 'M' then 1 else 0 end) " +
                "as manCount,SUM(case Sex when 'W' then 1 else 0 end) " +
                "as womanCount, SUM(1) as totalCount from Remo inner " +
                "join Grade on Remo.RId = Grade.RId group by Remo.RId,Remo.Remo_id) as T where T.Remo_id  like @p0";

            //分页查询排序字段
            model.SortField = string.IsNullOrEmpty(model.SortField) ? "T.Remo_id" : model.SortField;

            var grades = this.LoadPageEntitiesBySql<CountManAndWoman>(
                       model.PageIndex,
                       model.PageSize,
                       out totalCount,
                       sql,
                       model.SortField + " " + model.SortOrder,
                       searchKey
                       ).ToList();

            return grades;
        }

        /// <summary>
        /// 用存储过程查询男生女生人数，班级总人数
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<CountManAndWoman> QueryCountManAndWomanByProc(QueryModel model, out int totalCount)
        {
            totalCount = 0;
            //分页查询排序字段
            var list = this.ExceuteProcedure<CountManAndWoman>("[dbo].[COUNT_MEN_AND_WOMAN]", model.PageIndex,model.PageSize).ToList();
            totalCount = this.ExceuteProcedure<CountManAndWoman>("[dbo].[GET_TOTALCOUNT]").ToList().Count();
            return list;
        }
        public static string EncodeStr(string str)
        {
            str = str.Replace("'", "’");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("\n", "");
            str = str.Replace("%", "");
            str = str.Replace("\\", "");
            str = str.Replace("@", "");
            str = str.Replace("&", "");
            str = str.Replace("=", "");
            str = str.Replace("-", "");
            str = str.Replace("+", "");
            str = str.Replace("|", "");
            str = str.Replace("$", "");
            str = str.Replace("!", "");
            return str;
        }
    }
}
