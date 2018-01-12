/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:40   创建人：Hujunyuan
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json;

using CoreLand.Framework.UnitTest;
using CoreLand.Framework;
using CoreLand.Framework.Aop;
using CoreLand.Framework.Authentication;
using EIP.Entity;
using EIP.Service;
using EIP.Model;
using EIP.Model.ViewModels;
using AutoMapper;
namespace EIP.ServiceTest
{
    [TestClass]
    public class GradeServiceTest : ServiceUnitTest
    {
        IGradeService service;
        public override void AfterInit()
        {
            // 应用消息配置导入
            CoreLand.Framework.Message.MessageManager.Load("Message.config");

            // AutoMapper映射配置导入
            EIP.Service.AutoMapper.Configuration.Configure();

            // 服务实例获取
            service = this.GetService<IGradeService>();
            AppContext.User = AppContext.ServiceFactory.GetMembershipAdapter().FindBy("eip");
        }


        [TestMethod]
        public void QueryGrade()
        {


            //查询数据
            QueryModel model = new QueryModel();
            int totalCount = 0;
            model.Key = "hujunyuan";
            model.PageIndex = 1;
            model.PageSize = 10;
            var grades = service.QueryGrade(model, out totalCount);
            Assert.IsTrue(totalCount >= 0);

            //插入数据
            Grade grade = new Grade();
            grade.StudentName = "hujunyuan";
            grade.StudentId = "2014121010";
            service.SaveGrade(grade);
            service.ServiceContext.Commit();


            QueryModel model1 = new QueryModel();
            int totalCount1 = 0;
            model1.Key = "hujunyuan";
            model1.PageIndex = 1;
            model1.PageSize = 10;
            model1.SortField = "SId";
            var grades1 = service.QueryGrade(model1, out totalCount1);
            Assert.IsTrue(totalCount1 == totalCount + 1);

            QueryModel model2 = new QueryModel();
            int totalCount2 = 0;
            model2.Key = "hjyhujunyuan";
            model2.PageIndex = 1;
            model2.PageSize = 10;
            model2.SortField = "SId";
            var grades2 = service.QueryGrade(model2, out totalCount2);

            service.Delete<Grade>(grade.SId);

        }

        [TestMethod]
        public void queryCountManAndWoman()
        {
            //第一次查询数据
            QueryModel querymodel = new QueryModel();
            int querytotalCount = 0;
            querymodel.PageIndex = 1;
            querymodel.PageSize = 10;
            var grades = service.QueryCountManAndWoman(querymodel, out querytotalCount);
            Assert.IsTrue(querytotalCount >= 0);

            //创建一个班级
            Remo remoModel = new Remo();
            remoModel.Remo_id = "10010";
            var remoService = this.GetService<IRemoService>();
            remoService.SaveRemo(remoModel);
            remoService.ServiceContext.Commit();

            //更新班级信息
            remoModel.Remo_id = "10011";
            remoService.SaveRemo(remoModel);
            remoService.ServiceContext.Commit();

            //构造学生信息         
            string[] name = { "hjy", "yyl", "jonathan" };
            string[] stuid = { "20121", "20122", "20123" };
            string[] sex = { "M", "M", "W" };
            Grade[] grade = new Grade[3];

            //获取学生服务
            var gradeService = this.GetService<IGradeService>();

            //插入学生数据
            for(int i = 0; i < name.Length; i++)
            {
                var item = new Grade();
                item.StudentName = name[i];
                item.StudentId = stuid[i];
                item.Sex = sex[i];
                item.RId = remoModel.RId;
                gradeService.SaveGrade(item);
                gradeService.ServiceContext.Commit();
                grade[i] = item;
            }        

            //第二次查询数据
            QueryModel querymodel1 = new QueryModel();
            int querytotalCount1 = 0;
            querymodel1.PageIndex = 1;
            querymodel1.PageSize = 10;
            var grades1 = service.QueryCountManAndWoman(querymodel1, out querytotalCount1);
            Assert.IsTrue(querytotalCount1 == querytotalCount + 1);

            //删除学生信息
           foreach(var item in grade)
            {
                gradeService.DeleteGrade(item);
                this.service.ServiceContext.Commit();
            }

           //重复删除学生信息
            gradeService.DeleteGrade(grade[0]);
            this.service.ServiceContext.Commit();

            //删除班级信息
            remoService.DeleteRemo(remoModel);
            remoService.ServiceContext.Commit();

            //重复删除班级信息
            remoService.DeleteRemo(remoModel);
            remoService.ServiceContext.Commit();

            //插入更新删除一条学生数据
            Grade stu = new Grade();
            stu.StudentName = "hujunyuan";
            stu.StudentId = "2014121011";
            gradeService.SaveGrade(stu);
            gradeService.ServiceContext.Commit();
            stu.StudentName = "hujunyuan1";
            stu.StudentId = "20141210121";
            gradeService.SaveGrade(stu);
            gradeService.ServiceContext.Commit();
            gradeService.DeleteGrade(stu);
            gradeService.ServiceContext.Commit();
        }





        [TestMethod]
        public void SaveGrade()
        {
            Grade model = new Grade();
            model.StudentName = "hujunyuan";
            model.StudentId = "2014121010";
            service.SaveGrade(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(model.SId > 0);

            //插入一条数据
            Grade model1 = new Grade();
            model1.SId = model.SId;
            model1.StudentName = "hujunyuan";
            model1.StudentId = "2014121011";
            service.SaveGrade(model1);
            service.ServiceContext.Commit();
            Assert.IsTrue(model1.StudentId == "2014121011");

            service.Delete<Grade>(model.SId);
        }

        [TestMethod]
        public void DeleteGrade()
        {
            //插入一条数据
            Grade model = new Grade();
            model.StudentName = "hujunyuan";
            model.StudentId = "2014121010";
            service.SaveGrade(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(model.SId > 0);

            //通过ID删除刚才插入的数据
            bool result = service.DeleteGrade(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(result);

            //通过ID检查是否删除成功
            bool result1 = service.DeleteGrade(model);
            service.ServiceContext.Commit();
            Assert.IsFalse(result1);
        }

        [TestMethod]
        public void SaveRemoAndStudents()
        {
            //创建一个StudentClassManagementModel实例
            StudentClassManagmentModel model = new StudentClassManagmentModel();
            model.Grades = new List<StudentManagmentModel>();
            model.Remo_id = "1001";
            model.CId = 0;

            //构造学生信息字典
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] name = { "hjy", "yyl", "jonathan" };
            string[] stuid = { "20121", "20122", "20123" };
            for (int j = 0; j < name.Length; j++)
            {
                dict.Add(name[j], stuid[j]);
            }

            //插入数据
            foreach (var sname in dict)
            {
                StudentManagmentModel grade = new StudentManagmentModel();
                grade.StudentName = sname.Key;
                grade.StudentId = sname.Value;
                grade.isRemove = false;
                model.Grades.Add(grade);
            }
            string str = "";

            //提交新增信息
            service.SaveClassAndStudents(model, out str);
            service.ServiceContext.Commit();
            Assert.IsNotNull(str);


            //更新数据

            //把之前插入的学生表返回的ID分割成数组
            str = str.Trim();
            string[] newstr = str.Split(' ');


            //构造学生信息
            Dictionary<string, string> newDict = new Dictionary<string, string>();
            StudentClassManagmentModel model1 = new StudentClassManagmentModel();
            model1 = model;
            model1.RId = int.Parse(newstr[0].Trim());
            model1.Grades = new List<StudentManagmentModel>();
            model1.Remo_id = "1002";


            //构造学生信息字典
            int i = 0;
            string[] newName = { "hjy1", "yyl1", "jonathan1" };
            string[] newStuid = { "201211", "311432", "23141" };
            for (i = 0; i < name.Length; i++)
            {
                newDict.Add(newName[i], newStuid[i]);
            }

            i = 1;
            //生成学生信息Model.grades这个list
            foreach (var sname in newDict)
            {
                StudentManagmentModel grade = new StudentManagmentModel();
                grade.StudentName = sname.Key;
                grade.StudentId = sname.Value;
                grade.SId = int.Parse(newstr[i].Trim());
                grade.isRemove = false;
                model1.Grades.Add(grade);
                i++;
            }

            //提交修改
            str = "";
            service.SaveClassAndStudents(model1, out str);
            service.ServiceContext.Commit();

            //如果删除成功str则存储了班级ID和学生ID
            Assert.IsNotNull(str);

            //逻辑删除学生信息
            foreach (var item in model1.Grades)
            {
                item.isRemove = true;
            }
            str = "";
            service.SaveClassAndStudents(model1, out str);
            service.ServiceContext.Commit();
            Assert.IsNotNull(str);

            //获取学生服务
            var gradeService = this.GetService<IGradeService>();

            //执行删除学生操作
            foreach (var item in model1.Grades)
            {
                var grade = new Grade();
                grade = Mapper.Map<StudentManagmentModel, Grade>(item, grade);
                gradeService.Delete<Grade>(item.SId);
                gradeService.ServiceContext.Commit();
            }

            //删除班级信息
            var remoService = this.GetService<IRemoService>();
            remoService.Delete<Remo>(int.Parse(newstr[0].Trim()));
            remoService.ServiceContext.Commit();



            //完成DeleteGrade（）的覆盖
            Grade removemodel = new Grade();
            bool result1 = service.DeleteGrade(removemodel);
            service.ServiceContext.Commit();
            Assert.IsFalse(result1);

        }

        [TestMethod]
        public void CountManAndWomanByProc()
        {
            QueryModel querymodel = new QueryModel();
            querymodel.PageIndex = 1;
            querymodel.PageSize = 10;
            int querytotalCount = 0;
            var result = this.service.QueryCountManAndWomanByProc(querymodel,out querytotalCount);
            Assert.IsTrue(querytotalCount >= 0);

            //创建一个班级
            Remo remoModel = new Remo();
            remoModel.Remo_id = "10010";
            var remoService = this.GetService<IRemoService>();
            remoService.SaveRemo(remoModel);
            remoService.ServiceContext.Commit();

            //更新班级信息
            remoModel.Remo_id = "10011";
            remoService.SaveRemo(remoModel);
            remoService.ServiceContext.Commit();

            //构造学生信息         
            string[] name = { "hjy", "yyl", "jonathan" };
            string[] stuid = { "20121", "20122", "20123" };
            string[] sex = { "M" ,"M", "W" };
            Grade[] grade = new Grade[3];

            //获取学生服务
            var gradeService = this.GetService<IGradeService>();

            //插入学生数据
            for (int i = 0; i < name.Length; i++)
            {
                var item = new Grade();
                item.StudentName = name[i];
                item.StudentId = stuid[i];
                item.Sex = sex[i];
                item.RId = remoModel.RId;
                gradeService.SaveGrade(item);
                gradeService.ServiceContext.Commit();
                grade[i] = item;
            }

            //第二次查询数据
            QueryModel querymodel1 = new QueryModel();
            int querytotalCount1 = 0;
            querymodel1.PageIndex = 1;
            querymodel1.PageSize = 10;
            var grades1 = service.QueryCountManAndWomanByProc(querymodel1, out querytotalCount1);
            Assert.IsTrue(querytotalCount1 == querytotalCount + 1);

            //删除学生信息
            foreach (var item in grade)
            {
                gradeService.DeleteGrade(item);
                this.service.ServiceContext.Commit();
            }

            //重复删除学生信息
            gradeService.DeleteGrade(grade[0]);
            this.service.ServiceContext.Commit();

            //删除班级信息
            remoService.DeleteRemo(remoModel);
            remoService.ServiceContext.Commit();

            //重复删除班级信息
            remoService.DeleteRemo(remoModel);
            remoService.ServiceContext.Commit();

            //插入更新删除一条学生数据
            Grade stu = new Grade();
            stu.StudentName = "hujunyuan";
            stu.StudentId = "2014121011";
            gradeService.SaveGrade(stu);
            gradeService.ServiceContext.Commit();
            stu.StudentName = "hujunyuan1";
            stu.StudentId = "20141210121";
            gradeService.SaveGrade(stu);
            gradeService.ServiceContext.Commit();
            gradeService.DeleteGrade(stu);
            gradeService.ServiceContext.Commit();
        }
    }
}
