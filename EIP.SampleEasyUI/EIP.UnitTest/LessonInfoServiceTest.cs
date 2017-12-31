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
    public class LessonInfoServiceTest : ServiceUnitTest
    {
        ILessonInfoService service;
        public override void AfterInit()
        {
            // 应用消息配置导入
            CoreLand.Framework.Message.MessageManager.Load("Message.config");

            // AutoMapper映射配置导入
            EIP.Service.AutoMapper.Configuration.Configure();

            // 服务实例获取
            service = this.GetService<ILessonInfoService>();
            AppContext.User = AppContext.ServiceFactory.GetMembershipAdapter().FindBy("eip");
        }


        [TestMethod]
        public void QueryLessonInfo()
        {
            //查询数据
            QueryModel model = new QueryModel();
            int totalCount = 0;
            model.Key = "hujunyuan";
            model.PageIndex = 1;
            model.PageSize = 10;
            var lessonInfos = service.QueryLessonInfo(model, out totalCount);
            Assert.IsTrue(totalCount >= 0);

             
            LessonInfo li = new LessonInfo();
            li.category = "hujunyuan";
            service.SaveLessonInfo(li);
            service.ServiceContext.Commit();
            Assert.IsTrue(li.LIId > 0);

            QueryModel model3 = new QueryModel();
            int totalCount3 = 0;
            model3.Key = "hujunyuan";
            model3.PageIndex = 1;
            model3.PageSize = 10;
            var lessonInfos3 = service.QueryLessonInfo(model3, out totalCount3);
            Assert.IsTrue(totalCount + 1 == totalCount3);

            QueryModel model1 = new QueryModel();
            int totalCount1 = 0;
            model1.Key = "";
            model1.PageIndex = 1;
            model1.PageSize = 10;
            model1.SortField = "LIId";
            var lessonInfos1 = service.QueryLessonInfo(model1, out totalCount1);
            Assert.IsTrue(totalCount>=0);

            service.Delete<LessonInfo>(li.LIId);

            
        }

        [TestMethod]
        public void SaveLessonInfo()
        {
            LessonInfo model = new LessonInfo();
            model.category = "testCatogory";
            service.SaveLessonInfo(model);
			service.ServiceContext.Commit();
            Assert.IsTrue(model.LIId>0);

            LessonInfo model1 = new LessonInfo();
            model1.category = "Catogory";
            model1.LIId = model.LIId;
            service.SaveLessonInfo(model1);
            service.ServiceContext.Commit();
            Assert.IsTrue(model1.category== "Catogory");

            service.Delete<LessonInfo>(model.LIId);
        }

        [TestMethod]
        public void DeleteLessonInfo()
        {
            //插入一条数据
            LessonInfo model = new LessonInfo();
            model.category = "hujunyuan";
            service.SaveLessonInfo(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(model.LIId > 0);

            //通过ID删除刚才插入的数据
            bool result = service.DeleteLessonInfo(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(result);

            //通过ID检查是否删除成功
            bool result1 = service.DeleteLessonInfo(model);
            service.ServiceContext.Commit();
            Assert.IsFalse(result1);
        }
    }
}
