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

namespace EIP.ServiceTest
{
    [TestClass]
    public class entorServiceTest : ServiceUnitTest
    {
        IentorService service;
        public override void AfterInit()
        {
            // 应用消息配置导入
            CoreLand.Framework.Message.MessageManager.Load("Message.config");

            // AutoMapper映射配置导入
            EIP.Service.AutoMapper.Configuration.Configure();

            // 服务实例获取
            service = this.GetService<IentorService>();
            AppContext.User = AppContext.ServiceFactory.GetMembershipAdapter().FindBy("eip");
        }


        [TestMethod]
        public void Queryentor()
        {
            //查询数据
            QueryModel model = new QueryModel();
            int totalCount = 0;
            //model.Key = "hujunyuan";
            model.PageIndex = 1;
            model.PageSize = 10;
            var entors = service.Queryentor(model, out totalCount);
            Assert.IsTrue(totalCount >= 0);

            entor mentor = new entor();
            mentor.MentorName = "hujunyuan";
            service.Saveentor(mentor);
            service.ServiceContext.Commit();
            Assert.IsTrue(mentor.MId > 0);

            QueryModel model1 = new QueryModel();
            int totalCount1 = 0;
            model1.Key = "";
            model1.PageIndex = 1;
            model1.PageSize = 10;
            model1.SortField = "MId";
            var entors1 = service.Queryentor(model1, out totalCount1);
            Assert.IsTrue(totalCount1 >= 0);

            QueryModel model2 = new QueryModel();
            int totalCount2 = 0;
            //model2.Key = "hujunyuan";
            model2.PageIndex = 1;
            model2.PageSize = 10;
            model2.SortField = "MId";
            var entors2 = service.Queryentor(model2, out totalCount2);
            Assert.IsTrue(totalCount2 == totalCount+1);

            service.Delete<entor>(mentor.MId);
            
            
        }

        [TestMethod]
        public void Saveentor()
        {
            entor model = new entor();
            model.MentorName = "cuit";
            service.Saveentor(model);
			service.ServiceContext.Commit();
            Assert.IsTrue(model.MId > 0);

            //插入一条数据
            entor model1 = new entor();
            model1.MentorName = "hujunyuan";
            service.Saveentor(model1);
            service.ServiceContext.Commit();
            Assert.IsTrue(model1.MentorName=="hujunyuan");

           // service.Delete<entor>(model1.MId);
        }

        [TestMethod]
        public void Deleteentor()
        {
            //插入一条数据
            entor model = new entor();
            service.Saveentor(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(model.MId > 0);
            
            //通过ID删除刚才插入的数据
            bool result = service.Deleteentor(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(result);

            //通过ID检查是否删除成功
            bool result1 = service.Deleteentor(model);
            service.ServiceContext.Commit();
            Assert.IsFalse(result1);
        }

    }
}
