using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace EIP.Model
{
    /// <summary>
    /// 公用
    /// 列表查询的结果模型
    /// </summary>
    public class QueryResultModel
    {
        /// <summary>
        /// 查询的数据
        /// </summary>
        [JsonProperty("rows")]
        public object Data { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
