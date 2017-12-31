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
    public class CollegeServiceTest : ServiceUnitTest
    {
        ICollegeService service;
        public override void AfterInit()
        {
            // 应用消息配置导入
            CoreLand.Framework.Message.MessageManager.Load("Message.config");

            // AutoMapper映射配置导入
            EIP.Service.AutoMapper.Configuration.Configure();

            // 服务实例获取
            service = this.GetService<ICollegeService>();
            AppContext.User = AppContext.ServiceFactory.GetMembershipAdapter().FindBy("eip");
        }


        [TestMethod]
        public void QueryCollege()
        {
            


            //查询数据
            QueryModel model = new QueryModel();
            int totalCount = 0;
            model.Key = "hujunyuan";
            model.PageIndex = 1;
            model.PageSize = 10;
            var colleges = service.QueryCollege(model, out totalCount);
            Assert.IsTrue(totalCount>=0);

            //添加数据
            College college = new College();
            college.CollegeId = 100;
            college.CollegeName = "hujunyuan";
            service.SaveCollege(college);
            service.ServiceContext.Commit();
            Assert.IsTrue(college.CId>0);

            QueryModel model1 = new QueryModel();
            int totalCount1 = 0;
            model1.Key = "";
            model1.PageIndex = 1;
            model1.PageSize = 10;
            model1.SortField = "CollegeName";
            var colleges1 = service.QueryCollege(model1, out totalCount1);
            Assert.IsTrue(totalCount+1==totalCount1);
            //删除插入的数据
            service.Delete<College>(college.CId);
        }

        [TestMethod]
        public void SaveCollege()
        {
            College model = new College();
            model.CollegeName = "testname";
            model.CollegeId = 1001;
            service.SaveCollege(model);
		    service.ServiceContext.Commit();
            Assert.IsTrue(model.CId>0);
            int cid = model.CId;

            College model1 = new College();
            model1.CId = model.CId;
            model1.CollegeName = "cuit";
            model1.CollegeId = 1002;
            service.SaveCollege(model1);
            service.ServiceContext.Commit();
            Assert.IsTrue(model1.CId==cid);
            
            service.Delete<College>(model1.CId);



        }
        [TestMethod]
        public void DeleteCollege()
        {
            //插入一条数据
            College model = new College();
            model.CollegeName = "hujunyuan";
            service.SaveCollege(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(model.CId > 0);

            //通过ID删除这条数据
            bool result = service.DeleteCollege(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(result);
            
            //通过ID检查是否删除成功
            bool result1 = service.DeleteCollege(model);
            service.ServiceContext.Commit();
            Assert.IsFalse(result1);
        }
    }
}
