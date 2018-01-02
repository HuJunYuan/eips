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
    public class RemoServiceTest : ServiceUnitTest
    {
        IRemoService service;
        public override void AfterInit()
        {
            // 应用消息配置导入
            CoreLand.Framework.Message.MessageManager.Load("Message.config");

            // AutoMapper映射配置导入
            EIP.Service.AutoMapper.Configuration.Configure();

            // 服务实例获取
            service = this.GetService<IRemoService>();

            AppContext.User = AppContext.ServiceFactory.GetMembershipAdapter().FindBy("eip");
        }


        [TestMethod]
        public void QueryRemo()
        {
           
            
            //查询数据
            QueryModel model = new QueryModel();
            int totalCount = 0;
            model.Key = "1001011";
            model.PageIndex = 1;
            model.PageSize = 10;

            var remos = service.QueryRemo(model, out totalCount);
            Assert.IsTrue(totalCount >= 0);

            //插入数据
            Remo remo = new Remo();
            remo.Remo_id = "1001011" ;
            service.SaveRemo(remo);
            service.ServiceContext.Commit();
            Assert.IsTrue(remo.RId > 0);

            QueryModel model2 = new QueryModel();
            int totalCount2 = 0;
            model2.Key = "1001011";
            model2.PageIndex = 1;
            model2.PageSize = 10;
            model2.SortField = "Remo_id";
            var remos2 = service.QueryRemo(model2, out totalCount2);
            Assert.IsTrue(totalCount2 == totalCount+1);

            service.Delete<Remo>(remo.RId);

            QueryModel model1 = new QueryModel();
            int totalCount1 = 0;
            model1.Key = "";
            model1.PageIndex = 1;
            model1.PageSize = 10;
            model1.SortField = "Remo_id";
            var remos1 = service.QueryRemo(model1, out totalCount1);
            Assert.IsTrue(totalCount1 == totalCount+1);
        }

        [TestMethod]
        public void SaveRemo()
        {
            Remo model = new Remo();
            service.SaveRemo(model);
            model.Remo_id = "1001";
			service.ServiceContext.Commit();
            Assert.IsTrue(model.RId > 0);

            Remo model1 = new Remo();
            model1.RId = model.RId;
            model1.Remo_id = "1002";
            service.SaveRemo(model1);
            service.ServiceContext.Commit();
            Assert.IsTrue(model1.Remo_id=="1002");

            service.Delete<Remo>(model1.RId);
        }

        [TestMethod]
        public void DeleteRemo()
        {
            //插入一条数据
            Remo model = new Remo();
            service.SaveRemo(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(model.RId > 0);

            //通过ID删除刚才插入的数据
            bool result = service.DeleteRemo(model);
            service.ServiceContext.Commit();
            Assert.IsTrue(result);

            //通过ID检查是否删除成功
            bool result1 = service.DeleteRemo(model);
            service.ServiceContext.Commit();
            Assert.IsFalse(result1);
        }

        [TestMethod]
        public void SaveAndAlterMentorToRemo()
        {
            //创建一个新的班级
            Remo oldRremoModel = new Remo();
            oldRremoModel.Remo_id = "1002";
            service.SaveRemo(oldRremoModel);
            service.ServiceContext.Commit();
            Assert.IsTrue(oldRremoModel.RId > 0);

            //更新班级信息
            Remo newRremoModel = new Remo();
            newRremoModel.RId = oldRremoModel.RId;
            newRremoModel.Remo_id = "1003";
            service.SaveRemo(newRremoModel);
            service.ServiceContext.Commit();
            Assert.IsTrue(newRremoModel.Remo_id == "1003");

            //创建一个旧的教师信息
            entor oldMentor = new entor();
            oldMentor.MentorId = 2014;
            oldMentor.MentorName = "hujunyuan";
            oldMentor.MentorLevel = 3;

            //获取教师表服务
            var mentorService = this.GetService<IentorService>();

            //把旧的教师信息写入数据库
            mentorService.Saveentor(oldMentor);
            mentorService.ServiceContext.Commit();

            //创建一个新的教师信息
            entor newMentor = new entor();
            newMentor.MentorId = 2015;
            newMentor.MentorName = "hjynew";
            newMentor.MentorLevel = 2;

            //把新的教师信息写入数据库
            mentorService.Saveentor(newMentor);
            mentorService.ServiceContext.Commit();

            //更新新的教师信息
            newMentor.MentorId = 2015;
            newMentor.MentorName = "hjynew";
            newMentor.MentorLevel = 1;

            // 提交更新
            mentorService.Saveentor(newMentor);
            mentorService.ServiceContext.Commit();

            //创建一个旧的班级任课教师表信息
            entorToRemo mtrModel = new entorToRemo();
            mtrModel.MId = oldMentor.MId;
            mtrModel.RId = newRremoModel.RId;

            //提交旧的班级任课教师表信息
            service.SaveMentorToRemo(mtrModel);
            service.ServiceContext.Commit();

            //更新旧的班级任课教师信息
            mtrModel.MId = oldMentor.MId;
            mtrModel.RId = newRremoModel.RId;

            //提交更新
            service.SaveMentorToRemo(mtrModel);
            service.ServiceContext.Commit();

            //拷贝旧的班级任课教师信息MIRId
            var oldmtrModel = mtrModel;

            //逻辑删除旧的任课教师表信息并创建新的任课教师表信息
            service.AlterMentorToRemoByOrder(oldMentor,newMentor,newRremoModel,out mtrModel);
            service.ServiceContext.Commit();
            int mtrMTRid = mtrModel.MTRId;

            //将数据库中不存在的教师对象设置成新教师
            entor noentor = new entor();
            noentor.MId = 0;
            service.AlterMentorToRemoByOrder(newMentor, noentor, newRremoModel, out mtrModel);
            service.ServiceContext.Commit();

            //删除旧的班级任课教师表信息
            service.Delete<entorToRemo>(oldmtrModel.MTRId);
            service.ServiceContext.Commit();
            service.LogicDeleteMentorToRemo(oldmtrModel);

            //删除班级任课教师表信息
            service.Delete<entorToRemo>(mtrMTRid);
            service.ServiceContext.Commit();

            //删除旧的教师信息
            service.Delete<entor>(oldMentor.MId);

            //删除新的教师信息
            service.Delete<entor>(newMentor.MId);

            //删除班级信息
            service.Delete<Remo>(newRremoModel.RId);
        }

        [TestMethod]
        public void AlterMentorToRemoWithoughtOrder()
        {
            //创建一个新的班级
            Remo oldRremoModel = new Remo();
            oldRremoModel.Remo_id = "1002";
            service.SaveRemo(oldRremoModel);
            service.ServiceContext.Commit();
            Assert.IsTrue(oldRremoModel.RId > 0);

            //更新班级信息
            Remo newRremoModel = new Remo();
            newRremoModel.RId = oldRremoModel.RId;
            newRremoModel.Remo_id = "1003";
            service.SaveRemo(newRremoModel);
            service.ServiceContext.Commit();
            Assert.IsTrue(newRremoModel.Remo_id == "1003");

            //创建一个旧的教师信息
            entor oldMentor = new entor();
            oldMentor.MentorId = 2014;
            oldMentor.MentorName = "hujunyuan";
            oldMentor.MentorLevel = 3;

            //获取教师表服务
            var mentorService = this.GetService<IentorService>();

            //把旧的教师信息写入数据库
            mentorService.Saveentor(oldMentor);
            mentorService.ServiceContext.Commit();

            //创建一个新的教师信息
            entor newMentor = new entor();
            newMentor.MentorId = 2015;
            newMentor.MentorName = "hjynew";
            newMentor.MentorLevel = 2;

            //把新的教师信息写入数据库
            mentorService.Saveentor(newMentor);
            mentorService.ServiceContext.Commit();

            //更新新的教师信息
            newMentor.MentorId = 2015;
            newMentor.MentorName = "hjynew";
            newMentor.MentorLevel = 1;

            // 提交更新
            mentorService.Saveentor(newMentor);
            mentorService.ServiceContext.Commit();

            //创建一个旧的班级任课教师表信息
            entorToRemo mtrModel = new entorToRemo();
            mtrModel.MId = oldMentor.MId;
            mtrModel.RId = newRremoModel.RId;

            //提交旧的班级任课教师表信息
            service.SaveMentorToRemo(mtrModel);
            service.ServiceContext.Commit();

            //更新旧的班级任课教师信息
            mtrModel.MId = oldMentor.MId;
            mtrModel.RId = newRremoModel.RId;

            //提交更新
            service.SaveMentorToRemo(mtrModel);
            service.ServiceContext.Commit();

            //拷贝旧的班级任课教师信息
            var oldmtrModel = mtrModel;

            //逻辑删除旧的任课教师表信息并创建新的任课教师表信息
            service.AlterMentorToRemoWithoughtOrder(oldMentor,newMentor);
            service.ServiceContext.Commit();

            //将数据库中不存在的教师对象设置成新教师
            entor noentor = new entor();
            noentor.MId = 0;
            service.AlterMentorToRemoWithoughtOrder(newMentor, noentor);
            service.ServiceContext.Commit();

            //删除旧的班级任课教师表信息
            service.Delete<entorToRemo>(oldmtrModel.MTRId);
            service.ServiceContext.Commit();
            service.LogicDeleteMentorToRemo(oldmtrModel);

            //删除班级任课教师表信息
            service.Delete<entorToRemo>((mtrModel.MTRId+1));
            service.ServiceContext.Commit();

            //删除旧的教师信息
            service.Delete<entor>(oldMentor.MId);

            //删除新的教师信息
            service.Delete<entor>(newMentor.MId);

            //删除班级信息
            service.Delete<Remo>(newRremoModel.RId);
        }
    }
}
