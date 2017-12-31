/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:35   创建人：Hujunyuan
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

namespace EIP.Repository
{
    /// <summary>
    /// 用来记录每个班级的任课教师仓储
    /// </summary>
    public class entorToRemoRepository : DefaultRepository<entorToRemo>
    {

        public entorToRemoRepository(IUnitOfWork unitOfWork, IRepositoryFactory factory)
            : base(unitOfWork, factory)
        {

        }

        /// <summary>
        /// 查询用来记录每个班级的任课教师
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<entorToRemo> QueryentorToRemo(QueryModel model, out int totalCount)
        {
            //查询数据
            var searchKey = (string.IsNullOrEmpty(model.Key) ? "%" : "%" + model.Key.Trim() + "%");
            string sql = "select * from dbo.MentorToRemo where LogicDeleteFlag=0 and MTRId like @p0 ";

            //分页查询必须要有排序字段
            model.SortField = string.IsNullOrEmpty(model.SortField) ? "MTRId" : model.SortField;

            var entorToRemos = this.LoadPageEntitiesBySql<entorToRemo>(
                       model.PageIndex,
                       model.PageSize,
                       out totalCount,
                       sql,
                       model.SortField + " " + model.SortOrder,
                       searchKey
                       ).ToList();

            return entorToRemos;
        }

        /// <summary>
        /// 通过教师和班级的ID查询记录
        /// </summary>
        /// <param name="mentor">教师</param>
        /// <param name="remo">班级</param>
        /// <returns></returns>
        public List<entorToRemo> QueryentorToRemoByMentorAndRemo(entor mentor,Remo remo)
        {
            //构建查询数据sql
            string sql = "select * from dbo.MentorToRemo where LogicDeleteFlag=0 and MId = @p0 and RId = @p1 ";
        
            var entorToRemos = this.LoadEntitiesBySql<entorToRemo>(
                       sql,
                        mentor.MId,
                        remo.RId
                       ).ToList();

            return entorToRemos;
        }



        /// <summary>
        /// 通过教师ID查询记录
        /// </summary>
        /// <param name="mentor">教师</param>
        /// <param name="remo">班级</param>
        /// <returns></returns>
        public List<entorToRemo> QueryentorToRemoByMentor(entor mentor)
        {
            //构建查询数据sql
            string sql = "select * from dbo.MentorToRemo where LogicDeleteFlag=0 and MId = @p0";

            var entorToRemos = this.LoadEntitiesBySql<entorToRemo>(
                       sql,
                        mentor.MId
                       ).ToList();

            return entorToRemos;
        }
    }
}
