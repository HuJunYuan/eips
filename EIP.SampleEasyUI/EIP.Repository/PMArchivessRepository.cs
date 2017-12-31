/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-11-16 15:53:12   创建人：KY_THH
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data;

using CoreLand.Framework;
using CoreLand.Framework.Data;
using CoreLand.Framework.Code;
using EIP.HR.Model;
using EIP.HR.Entity;
using EIP.HR.Model.ViewModels;

namespace EIP.HR.Repository
{
    /// <summary>
    /// 档案仓储
    /// </summary>
    public class PMArchivessRepository : DefaultRepository<>
    {

        public PMArchivessRepository(IUnitOfWork unitOfWork, IRepositoryFactory factory)
            : base(unitOfWork, factory)
        {

        }

        public void Test() 
        {
          
        }


    }
}
