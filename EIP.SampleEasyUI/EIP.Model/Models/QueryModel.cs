using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace EIP.Model
{
    /// <summary>
    /// 公用
    /// 列表查询的基础模型
    /// </summary>
    public class QueryModel
    {
        /// <summary>
        /// 关键词
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        [JsonProperty("page")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页记录
        /// </summary>
        [JsonProperty("rows")]
        public int PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [JsonProperty("sort")]
        public string SortField { get; set; }

        /// <summary>
        /// 排序方式
        /// desc or asc
        /// </summary>
        [JsonProperty("order")]
        public string SortOrder { get; set; }
    }
}
