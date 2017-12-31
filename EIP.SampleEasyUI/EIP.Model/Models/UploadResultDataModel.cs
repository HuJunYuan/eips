using System;

namespace EIP.Model
{
    /// <summary>
    /// 内容管理系统，上传数据完成后，返回数据模型
    /// </summary>
    public class UploadResultDataModel
    {
        /// <summary>
        /// 调用当前页面的对象
        /// 由调用页面传入
        /// </summary>
        public object Sender { get; set; }

        /// <summary>
        /// 是否上传成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据ID
        /// </summary>
        public Guid Data { get; set; }

        /// <summary>
        /// 数据URL
        /// </summary>
        public string DataUrl { get; set; }

        /// <summary>
        /// 数据Html
        /// </summary>
        public string DataHtml { get; set; }

        /*
         *  数据ID，一般是调用者需要保存到数据库中的数据
            var data = Guid.Parse("c0273240-ad77-41f6-8d23-e9e57b2d6e25");
            数据URL, 是用来展示上传内容的URL，调用者可自定义展示方式
            var dataUrl = "http://localhost:34303/" + data;
            数据Html，是返回的最基础的内容展示HTML,调用者可根据 dataUrl 自定义
            如上传图片此处可为 "<img src=" + dataUrl + " id =" + data + "/>"
            如上传视频此处可为 "<embed src=" + dataUrl + " id =" + data + "/>";
            var dataHtml = "<embed src=" + dataUrl + " id =" + data + "/>";

            var result = new UploadResultDataModel()
            {
                Success = true,
                Message = string.Empty,
                Data = data,
                DataUrl = dataUrl,
                DataHtml = dataHtml
            };
         * 
        */
    }
}