/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:38   创建人：Hujunyuan
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
using EIP.Model.ViewModels;

namespace EIP.Service
{
    /// <summary>
    /// 学生信息表服务
    /// </summary>
    public class GradeService : EntityBaseService, IGradeService
    {
        #region 初始化

        /// <summary>
        /// 学生信息表仓储
        /// </summary>
        private GradeRepository gradeRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="factory">仓储创建工厂</param>
        public GradeService(IUnitOfWork uow, IRepositoryFactory factory)
            : base(uow, factory)
        {
            gradeRepository = this.CreateRepository<GradeRepository>();
        }

        #endregion

        #region IGradeService method

        /// <summary>
        /// 查询学生信息表
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<Grade> QueryGrade(QueryModel model, out int totalCount)
        {
            return gradeRepository.QueryGrade(model, out totalCount);
        }

        /// <summary>
        /// 查询学生信息表,使用数据字典
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<GradeViewModel> QueryGradeUseCodeManager(QueryModel model, out int totalCount)
        {
            //使用数据字典把男女性别替换
            //List<Grade> grades = gradeRepository.QueryGrade(model, out totalCount);

            //List<GradeViewModel> gradeviewmodel = new List<GradeViewModel>();

            // foreach(var item in grades)
            // {
            //     GradeViewModel gvm = new GradeViewModel();
            //     Mapper.Map<Grade, GradeViewModel>(item, gvm);
            //     gvm.SexName = CodeManger.GetCodeText(CommonConstant.CODETYPE_SEX, item.Sex);
            //     gradeviewmodel.Add(gvm);
            // }

            List<GradeViewModel> gradeviewmodel = gradeRepository.QueryGradeWithRemo_id(model, out totalCount);
            return gradeviewmodel;
        }

        /// <summary>
        /// 查询男生女生人数，班级总人数
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<CountManAndWoman> QueryCountManAndWoman(QueryModel model, out int totalCount)
        {
            return gradeRepository.QueryCountManAndWoman(model, out totalCount);
        }


        /// <summary>
        /// 根据班级模糊查询男生女生人数，班级总人数
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<CountManAndWoman> QueryCountManAndWomanUseLike(QueryModel model, out int totalCount)
        {
            return gradeRepository.QueryCountManAndWomanUseLike(model, out totalCount);
        }

        /// <summary>
        /// 用存储过程查询男生女生人数，班级总人数
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<CountManAndWoman> QueryCountManAndWomanByProc(QueryModel model, out int totalCount)
        {
            return gradeRepository.QueryCountManAndWomanByProc(model, out totalCount);
        }

        /// <summary>
        /// 处理学生信息表
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">学生信息表信息</param>
        /// <returns>处理是否成功</returns>
        public int SaveGrade(Grade model)
        {
            //检查是否存在重复数据
            var entity = this.Find<Grade>(model.SId);



            //更新数据 ， 否则新增数据
            if (entity != null)
            {
                //更新数据
                entity = Mapper.Map<Grade, Grade>(model, entity);
                this.gradeRepository.Update(entity);
            }
            else
            {
                //录入数据
                this.gradeRepository.Insert(model);
            }

            return model.SId;
        }

        /// <summary>
        /// 删除学生信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">学生信息    信息</param>
        /// <returns>处理是否成功</returns>
        public bool DeleteGrade(Grade model)
        {
            // 检查是否存在这条数据
            var entity = this.Find<Grade>(model.SId);
            bool flag = false;
            //如果存在执行删除操作，否则不执行
            if (entity != null)
            {
                //执行删除操作
                this.Delete<Grade>(model.SId);
                ServiceContext.Commit();
                //检查是否删除成功
                var isComplete = this.Find<Grade>(model.SId);
                if (isComplete == null) flag = true;
            }
            else flag = false;
            return flag;
        }

        /// <summary>
        /// 添加班级及学生信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">班级学生信息    信息</param>
        /// <returns>处理是否成功</returns>
        public bool SaveClassAndStudents(StudentClassManagmentModel model, out string mdex)
        {
            //初始化标识位和结果字符串
            mdex = "";
            bool flag = false;

            // 把班级信息映射起来
            var remo = new Remo();
            remo = Mapper.Map<StudentClassManagmentModel, Remo>(model, remo);

            //标准化"所属学院(CId)"
            if (model.CId == 0)
            {
                remo.CId = null;
            }

            //创建service实例对象
            var remoService = this.GetService<IRemoService>();

            //获取班级服务 
            var gradeservice = this.GetService<IGradeService>();

            //创建或更新班级
            remoService.SaveRemo(remo);
            remoService.ServiceContext.Commit();

            //将"班级ID（RId）"写入返回字符串
            mdex = mdex + remo.RId + " ";

            //将需要执行插入或删除的学生信息进行处理
            foreach (var item in model.Grades)
            {

                //为学生信息中记录班级的字段赋值
                item.RId = remo.RId;

                //通过isRemove字段对学生信息进行处理
                if (item.isRemove)
                {
                    var grade = new Grade();

                    //把item中的信息映射到grade
                    grade = Mapper.Map<StudentManagmentModel, Grade>(item, grade);

                    //删除学生信息
                    this.LogicDelete<Grade>(grade.SId);
                    gradeservice.ServiceContext.Commit();
                }
                else
                {
                    var grade = new Grade();

                    //把item中的信息映射到grade
                    grade = Mapper.Map<StudentManagmentModel, Grade>(item, grade);

                    //插入或更新学生信息
                    SaveGrade(grade);
                    gradeservice.ServiceContext.Commit();

                    //执行成功，记录操作的学生的SId
                    if (grade.SId > 0)
                        mdex = mdex + grade.SId.ToString() + " ";
                }
            }

            return flag;
        }



        public void Bathremove(int[] ids)
        {
            var gradeservice = this.GetService<IGradeService>();

            foreach (var id in ids)
            {
                gradeservice.LogicDelete<Grade>(id);
            }
        }


       
        #endregion

        #region private method

        #endregion
    }
}