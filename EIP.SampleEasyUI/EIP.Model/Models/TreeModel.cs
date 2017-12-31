/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017/6/5 23:01:14   创建人：youke
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace EIP.Model
{
    public class TreeDataModel
    {
        /// <summary>
        /// 节点的 id，它对于加载远程数据很重要。
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        [JsonProperty("children")]
        public IList<TreeDataModel> Childrens { get; set; }

        /// <summary>
        /// 要显示的节点文本。
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// 节点状态，'open' 或 'closed'，默认是 'open'。当设置为 'closed' 时，该节点有子节点，并且将从远程站点加载它们。
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// 指示节点是否被选中。
        /// </summary>
        [JsonProperty("checked")]
        public bool Checked { get; set; }

        /// <summary>
        /// 给一个节点添加的自定义属性。
        /// 例："attributes":{
        ///	"url":"/demo/book/abc",
        ///	"price":100
        /// },
        /// </summary>
        [JsonProperty("attributes")]
        public object Attributes { get; set; }

        /// <summary>
        /// 图标样式。
        /// </summary>
        [JsonProperty("iconCls")]
        public string IconCls { get; set; }

    }

    public class TreePlainDataModel
    {
        /// <summary>
        /// 节点的 id，它对于加载远程数据很重要。
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 父级节点id
        /// </summary>
        [JsonProperty("pid")]
        public string PId { get; set; }

        /// <summary>
        /// 要显示的节点文本。
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// 节点状态，'open' 或 'closed'，默认是 'open'。当设置为 'closed' 时，该节点有子节点，并且将从远程站点加载它们。
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// 指示节点是否被选中。
        /// </summary>
        [JsonProperty("checked")]
        public bool Checked { get; set; }

        /// <summary>
        /// 给一个节点添加的自定义属性。
        /// 例："attributes":{
		///	"url":"/demo/book/abc",
		///	"price":100
		/// },
        /// </summary>
        [JsonProperty("attributes")]
        public object Attributes { get; set; }

        /// <summary>
        /// 图标样式。
        /// </summary>
        [JsonProperty("iconCls")]
        public string IconCls { get; set; }

    }
}
