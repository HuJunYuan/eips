/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2018-01-12 10:32:10   创建人：Hujunyuan
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
    public class LocalServiceTest : ServiceUnitTest
    {
        ILocalService service;
        public override void AfterInit()
        {
            // 应用消息配置导入
            CoreLand.Framework.Message.MessageManager.Load("Message.config");

            // AutoMapper映射配置导入
            EIP.Service.AutoMapper.Configuration.Configure();

            // 服务实例获取
            service = this.GetService<ILocalService>();
        }


        [TestMethod]
        public void QueryLocal()
        {
            //查询数据
            QueryModel model = new QueryModel();
            int totalCount = 0;
            model.Key = "test";
            model.PageIndex = 1;
            model.PageSize = 10;

            var locals = service.QueryLocal(model, out totalCount);

            //查询数据
            QueryModel model1 = new QueryModel();
            int totalCount1 = 0;
            model1.Key = "";
            model1.SortField = "LocalID";
            model1.PageIndex = 1;
            model1.PageSize = 10;

            var locals1 = service.QueryLocal(model1, out totalCount1);

        }

        [TestMethod]
        public void SaveLocal()
        {
            Local model = new Local{
                
            };
            service.SaveLocal(model);
			service.ServiceContext.Commit();
            model.LocalNum = "510124";
            model.LocalName = "成都";
            service.SaveLocal(model);
            service.ServiceContext.Commit();
            service.Delete<Local>(model.LocalID);
            service.ServiceContext.Commit();
        }
    }
}
