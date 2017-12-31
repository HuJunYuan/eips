using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EIP.Model
{
    /// <summary>
    /// 积分信息
    /// </summary>
    public class DptScoreInfo
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public int OID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public int ParentID { get; set; }
    }
}
