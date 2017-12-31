using System;

using CoreLand.Framework.Data;

namespace EIP.Model
{
    /// <summary>
    /// 公用 常用的下拉列表模型
    /// 如 combox
    /// </summary>
    public class ListDataModel
    {
        public int id { get; set; }

        public string text { get; set; }
    }

    /// <summary>
    /// 公用 常用的树形下拉列表模型
    /// 如 treeselect
    /// </summary>
    public class TreeListDataModel : ListDataModel
    {
        public int? pid { get; set; }
    }

    /// <summary>
    /// Code数据模型
    /// </summary>
    public class CodeDataModel : ICodeEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string ItemShortText { get; set; }
        public string ItemText { get; set; }
        public string ItemValue { get; set; }
        public string ItemPinyin { get; set; }
    }
    /// <summary>
    ///  下拉列表模型
    /// </summary>
    public class GuidListDataModel  
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public Guid Code { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }

    }
}