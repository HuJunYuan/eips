using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.Model
{
    /// <summary>
    /// 根据父级编码和值，查询对应的CodeMaster 的视图模型
    /// </summary>
    public class CodeMasterViewModel
    {
        /// <summary>
        /// 父级编码
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// 数据字典列表视图
    /// </summary>
    public class CodeMasterListViewModel
    {
        /// <summary>
        /// 主键
        /// 不能为空
        /// </summary>	
        public int Id { get; set; }

        /// <summary>
        /// 父节点ID
        /// 可为空
        /// 最大长度为100     
        /// </summary>	
        public int? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// 可为空
        /// 最大长度为100     
        /// </summary>	
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// 可为空
        /// 最大长度为200     
        /// </summary>	
        public string Text { get; set; }

        /// <summary>
        /// 略称
        /// 可为空
        /// 最大长度为200     
        /// </summary>	
        public string ShortText { get; set; }

        /// <summary>
        /// 汉字拼音
        /// 可为空
        /// 最大长度为200     
        /// </summary>	
        public string Pinyin { get; set; }

        /// <summary>
        /// 显示顺序
        /// 可为空
        /// </summary>
        public int ShowIndex { get; set; }

    }

    /// <summary>
    /// 数据字典新建视图
    /// </summary>
    public class CodeMasterSaveViewModel
    {
        /// <summary>
        /// 主键
        /// 不能为空
        /// </summary>	
        public int Id { get; set; }

        /// <summary>
        /// 父节点ID
        /// 可为空
        /// 最大长度为100     
        /// </summary>	
        public int? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// 可为空
        /// 最大长度为100     
        /// </summary>	
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// 可为空
        /// 最大长度为200     
        /// </summary>	
        public string Text { get; set; }

        /// <summary>
        /// 略称
        /// 可为空
        /// 最大长度为200     
        /// </summary>	
        public string ShortText { get; set; }

        /// <summary>
        /// 汉字拼音
        /// 可为空
        /// 最大长度为200     
        /// </summary>	
        public string Pinyin { get; set; }

        /// <summary>
        /// 显示顺序
        /// 可为空
        /// </summary>
        public int ShowIndex { get; set; }

    }
}
