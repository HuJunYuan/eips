/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 17:22:15   创建人：Hujunyuan
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
using EIP.Model.ViewModels;
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
    /// 教师服务
    /// </summary>
    public class entorService : EntityBaseService, IentorService
    {
        #region 初始化

        /// <summary>
        /// 教师仓储
        /// </summary>
        private entorRepository entorRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="factory">仓储创建工厂</param>
        public entorService(IUnitOfWork uow, IRepositoryFactory factory)
            : base(uow, factory)
        {
            entorRepository = this.CreateRepository<entorRepository>();
        }

        #endregion

        #region IentorService method

        /// <summary>
        /// 查询教师
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<entor> Queryentor(QueryModel model, out int totalCount)
        {
            return entorRepository.Queryentor(model, out totalCount);
        }

        /// <summary>
        /// 查询教师并且使用数据字典修改性别
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<entorViewModel> QueryentorUseCodeManager(QueryModel model, out int totalCount)
        {
            var entorList = entorRepository.QueryentorByName(model, out totalCount);

            List<entorViewModel> list = new List<entorViewModel>();

            foreach(var item in entorList)
            {
                entorViewModel mvm = new entorViewModel();
                Mapper.Map<entor, entorViewModel>(item, mvm);
                mvm.SexName = CodeManger.GetCodeText(CommonConstant.CODETYPE_SEX, item.Sex);
                list.Add(mvm);
            }
            return list;
        }


        /// <summary>
        /// 查询姓名查询教师
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<entor> QueryentorByName(QueryModel model, out int totalCount)
        {
            return entorRepository.QueryentorByName(model, out totalCount);
        }
        
        /// <summary>
        /// 处理教师
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>处理是否成功</returns>
        public int Saveentor(entor model)
        {
            //检查是否存在重复数据
            var entity = this.Find<entor>(model.MId);

        //    entity = Mapper.Map<entor, entor>(model, entity);

            //更新数据 ， 否则新增数据
            if (entity != null)
            {
                //更新数据
                entity = Mapper.Map<entor, entor>(model, entity);
                this.entorRepository.Update(entity);
            }
            else
            {
                //录入数据
                this.entorRepository.Insert(model);
            }

            return model.MId;
        }

        /// <summary>
        /// 删除教师信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">教师信息    信息</param>
        /// <returns>处理是否成功</returns>
        public bool Deleteentor(entor model)
        {
            // 检查是否存在这条数据
            var entity = this.Find<entor>(model.MId);
            bool flag = false;
            //如果存在执行删除操作，否则不执行
            if (entity != null)
            {
                //执行删除操作
                this.Delete<entor>(model.MId);
                ServiceContext.Commit();
                //检查是否删除成功
                var isComplete = this.Find<entor>(model.MId);
                if (isComplete == null) flag = true;
            }
            else flag = false;
            return flag;
        }


        #endregion

        #region private method

        #endregion
    }
}