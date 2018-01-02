using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIP.Entity;
namespace EIP.Model.ViewModels
{
    public class CountManAndWoman
    {

        /// <summary>
        /// 班级表标识ID
        /// </summary>
        public int Rid { get; set; }

        /// <summary>
        /// 班级名字
        /// </summary>
        public string Remo_id { get; set; }

        ///<summary>
        ///男生人数
        /// </summary>
        /// 
        public int manCount { get; set; }

        ///<summary>
        ///女生人数
        /// </summary>
        /// 
        public int womanCount { get; set; }

        /// <summary>
        /// 班级总人数
        /// </summary>
        /// 
        public int totalCount { get; set; }
    }
}
