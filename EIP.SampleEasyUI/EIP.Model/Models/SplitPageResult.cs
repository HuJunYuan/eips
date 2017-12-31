using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.Model
{
   public class SplitPageResult<T> 
       where T:class
    {
       /// <summary>
       /// 查询结果
       /// </summary>
       public List<T> ResultList { get; set; }

       /// <summary>
       /// 总条数
       /// </summary>
       public int RecordCount { get; set; }

       /// <summary>
       /// 总页数
       /// </summary>
       public int PageCount { get; set; }

    }
}
