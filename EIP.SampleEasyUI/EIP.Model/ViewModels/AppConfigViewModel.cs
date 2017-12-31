using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using EIP.Entity;

namespace EIP.Model
{
    /// <summary>
    /// 应用配置定义模型
    /// </summary>
    public class AppConfigQueryModel : QueryModel
    {

        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; set; }

    }
}
