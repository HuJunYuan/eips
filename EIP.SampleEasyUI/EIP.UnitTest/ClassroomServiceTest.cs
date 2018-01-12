/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:39   创建人：Hujunyuan
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

namespace EIP.ServiceTest
{
    [TestClass]
    public class ClassroomServiceTest : ServiceUnitTest
    {
        IClassroomService service;
        public override void AfterInit()
        {
            // 应用消息配置导入
            CoreLand.Framework.Message.MessageManager.Load("Message.config");

            // AutoMapper映射配置导入
            EIP.Service.AutoMapper.Configuration.Configure();

            // 服务实例获取
            service = this.GetService<IClassroomService>();
            AppContext.User = AppContext.ServiceFactory.GetMembershipAdapter().FindBy("eip");
        }


        [TestMethod]
        public void QueryClassroom()
        {
            
            //查询数据
            QueryModel model = new QueryModel();
            int totalCount = 0;
            model.Key = "hujunyuan";
            model.PageIndex = 1;
            model.PageSize = 10;
            var classrooms = service.QueryClassroom(model, out totalCount);
            Assert.IsTrue(totalCount>=0);

            //插入数据
            Classroom classroom = new Classroom();
            classroom.ClassroomName = "hujunyuan";
            service.SaveClassroom(classroom);
            service.ServiceContext.Commit();
            //Assert.IsTrue(classroom.ClassroomId > 0);

            QueryModel model1 = new QueryModel();
            int totalCount1 = 0;
            model1.Key = "";
            model1.PageIndex = 1;
            model1.PageSize = 10;
            model1.SortField = "ClassroomId";
            var classrooms1 = service.QueryClassroom(model1, out totalCount1);
            //Assert.IsTrue(totalCount1== totalCount+1);

            service.Delete<Classroom>(classroom.ClassroomId);
           

        }

        [TestMethod]
        public void SaveClassroom()
        {
            //插入一条数据
            Classroom model = new Classroom();
            service.SaveClassroom(model);
			service.ServiceContext.Commit();
            Assert.IsTrue(model.ClassroomId > 0);

            Classroom model1 = new Classroom();
            model1.ClassroomId = model.ClassroomId;
            service.SaveClassroom(model1);
            service.ServiceContext.Commit();
            Assert.IsTrue(model.ClassroomId > 0);

            service.Delete<Classroom>(model.ClassroomId);
           
        }

       [TestMethod]
       public void DeleteClassroom()
        {
            //插入一条数据
            Classroom model = new Classroom();
            service.SaveClassroom(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(model.ClassroomId > 0);

            //通过ID删除刚才插入的数据
            bool result =  service.DeleteClassroom(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(result);

            //通过ID检查是否删除成功
            bool result1 = service.DeleteClassroom(model);
            service.ServiceContext.Commit();
            Assert.IsFalse(result1);
        }
    }
}
