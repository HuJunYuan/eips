/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:39   创建人：Hujunyuan
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using Newtonsoft.Json;
using AutoMapper;

using CoreLand.Framework;
using CoreLand.Framework.Data;
using CoreLand.Framework.Service;
using CoreLand.Framework.Code;
using CoreLand.Framework.Authentication;
using CoreLand.Framework.Helpers;
using EIP.Model;
using EIP.Repository;
using EIP.Entity;

namespace EIP.Service
{
    /// <summary>
    /// 班级服务
    /// </summary>
    public class RemoService : EntityBaseService, IRemoService
    {
        #region 初始化

        /// <summary>
        /// 班级仓储
        /// </summary>
        private RemoRepository remoRepository = null;

        /// <summary>
        /// 班级任课教师仓储
        /// </summary>
        private entorToRemoRepository mentorToRemoRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="factory">仓储创建工厂</param>
        public RemoService(IUnitOfWork uow, IRepositoryFactory factory)
            : base(uow, factory)
        {
            remoRepository = this.CreateRepository<RemoRepository>();
            mentorToRemoRepository = this.CreateRepository<entorToRemoRepository>();
        }

        #endregion

        #region IRemoService method

        /// <summary>
        /// 查询班级
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<Remo> QueryRemo(QueryModel model, out int totalCount)
        {
            return remoRepository.QueryRemo(model, out totalCount);
        }

        /// <summary>
        /// 处理班级
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">班级信息</param>
        /// <returns>处理是否成功</returns>
        public int SaveRemo(Remo model)
        {
            //检查是否存在重复数据
            var entity = this.Find<Remo>(model.RId);

            //更新数据 ， 否则新增数据
            if (entity != null)
            {
                //更新数据
                entity = Mapper.Map<Remo, Remo>(model, entity);
                this.remoRepository.Update(entity);
            }
            else
            {
                //录入数据
                this.remoRepository.Insert(model);
            }

            return model.RId;
        }

        /// <summary>
        /// 删除班级信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">信息    信息</param>
        /// <returns>处理是否成功</returns>
        public bool DeleteRemo(Remo model)
        {
            // 检查是否存在这条数据
            var entity = this.Find<Remo>(model.RId);
            bool flag = false;
            //如果存在执行删除操作，否则不执行
            if (entity != null)
            {
                //执行删除操作
                this.Delete<Remo>(model.RId);
                ServiceContext.Commit();
                //检查是否删除成功
                var isComplete = this.Find<Remo>(model.RId);
                if (isComplete == null) flag = true;
            }
            else flag = false;
            return flag;
        }

        /// <summary>
        /// 多行删除班级信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">信息    信息</param>
        public void MultiLineRemove(int[] ids)
        {
            foreach(var id in ids)
            {
                // 检查是否存在这条数据
                var entity = this.Find<Remo>(id);

                //如果存在执行删除操作，否则不执行
                if (entity != null)
                {
                    var remoService = GetService<IRemoService>();
                    //执行删除操作
                    remoService.LogicDelete<Remo>(id);
                    ServiceContext.Commit();
                }
            }
           
        }

        

        #endregion

        #region IentorToRemoService method
        /// <summary>
        /// 处理班级任课教师表
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">班级任课教师信息</param>
        /// <returns>处理是否成功</returns>
        public int SaveMentorToRemo(entorToRemo model)
        {
            //检查是否存在重复数据
            var entity = this.Find<entorToRemo>(model.MTRId);

            //更新数据 ， 否则新增数据
            if (entity != null)
            {
                //更新数据
                entity = Mapper.Map<entorToRemo, entorToRemo>(model, entity);
                this.mentorToRemoRepository.Update(entity);
            }
            else
            {
                //录入数据
                this.mentorToRemoRepository.Insert(model);
            }

            return model.MTRId;
        }

        /// <summary>
        /// 删除班级任课教师信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">信息    信息</param>
        /// <returns>处理是否成功</returns>
        public bool LogicDeleteMentorToRemo(entorToRemo model)
        {
            // 检查是否存在这条数据
            var entity = this.Find<entorToRemo>(model.MTRId);
            bool flag = false;
            //如果存在执行删除操作，否则不执行
            if (entity != null)
            {
                //执行删除操作
                this.LogicDelete<entorToRemo>(model.MTRId);
                ServiceContext.Commit();
                flag = true;
            }
            else flag = false;
            return flag;
        }

       
        /// <summary>
        /// 修改班级任课教师信息
        /// 根据 id 编号调用相应的方法进行处理
        /// </summary>
        /// <param name="oldone">旧教师对象</param>
        /// <param name="newone">新教师对象</param>
        /// <param name="remo">指定的班级对象</param>
        /// <param name="alteredMentorToRemo">用来返回修改后的班级任课教师信息的对象</param>
        /// <returns>处理是否成功</returns>
        /// 
        public bool AlterMentorToRemoByOrder(entor oldone, entor newone, Remo remo, out entorToRemo alteredMentorToRemo)
        {
            //创建标识符
            //查询旧信息是否成功
            bool flag = false;

            //逻辑删除旧的信息是否成功
            bool flag1 = false;

            //创建新的信息是否成功
            bool flag2 = false;

            //通过班级和旧任课教师的信息获取班级任课教师信息
            var mentorToRemo = mentorToRemoRepository.QueryentorToRemoByMentorAndRemo(oldone, remo);
            if (mentorToRemo.Count() > 0)
            {
                flag = true;
            }

            //获取教师信息服务
            var mentorService = this.GetService<IentorService>();

            //查询新的教师信息是否存在在数据库中
            QueryModel querymodel = new QueryModel();
            int totalCount = 0;
            querymodel.Key = newone.MId.ToString();
            querymodel.PageIndex = 1;
            querymodel.PageSize = 10;
            var remos = mentorService.Queryentor(querymodel, out totalCount);

            //创建新的班级任课教师对象
            entorToRemo model = new entorToRemo();
            if (totalCount > 0)
            {

                //逻辑删除旧信息
                foreach (var item in mentorToRemo)
                {
                    this.LogicDeleteMentorToRemo(item);
                    flag1 = true;
                }

                //创建新的班级任课教师对象
                //entorToRemo model = new entorToRemo();
                model.MId = newone.MId;
                model.RId = remo.RId;
                model.LogicDeleteFlag = false;

                //把新的班级任课教师信息写入数据库
                this.SaveMentorToRemo(model);
                this.ServiceContext.Commit();
                if (model.MTRId > 0)
                {
                    flag2 = true;

                }
            }



            //返回数据
            alteredMentorToRemo = model;
            return flag && flag1 && flag2;

        }


        /// <summary>
        /// 修改班级任课教师信息
        /// 根据 id 编号调用相应的方法进行处理
        /// </summary>
        /// <param name="oldone">旧教师对象</param>
        /// <param name="newone">新教师对象</param>
        /// <returns>处理是否成功</returns>
        /// 
        public bool AlterMentorToRemoWithoughtOrder(entor oldone, entor newone)
        {
            //设置标志
            bool flag = false;

            //通过旧任课教师的信息获取班级任课教师信息
            var oldmentorToRemo = mentorToRemoRepository.QueryentorToRemoByMentor(oldone);

            //获取教师信息服务
            var mentorService = this.GetService<IentorService>();

            //查询新的教师信息是否存在在数据库中
            QueryModel querymodel = new QueryModel();
            int totalCount = 0;
            querymodel.Key = newone.MId.ToString();
            querymodel.PageIndex = 1;
            querymodel.PageSize = 10;
            var remos = mentorService.Queryentor(querymodel, out totalCount);

            //如果新教师信息存在则进行下面操作
            if (totalCount > 0)
            {
                //逻辑删除旧信息
                foreach (var item in oldmentorToRemo)
                {
                    this.LogicDeleteMentorToRemo(item);
                }


                //创建新的班级任课教师对象
                foreach (var item in oldmentorToRemo)
                {
                    //创建新的班级任课教师对象
                    entorToRemo model = new entorToRemo();
                    model.MId = newone.MId;
                    model.RId = item.RId;

                    //把新的班级任课教师信息写入数据库
                    this.SaveMentorToRemo(model);
                    this.ServiceContext.Commit();
                    flag = true;
                }

            }
            return flag;
        }
        #endregion
        #region private method

        #endregion
    }
}
