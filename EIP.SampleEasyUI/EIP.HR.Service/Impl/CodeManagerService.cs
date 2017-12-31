using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using CoreLand.Framework.Data;
using CoreLand.Framework.EF.Data;
using CoreLand.Framework.Service;
using EIP.Sample.Entity;
using EIP.Sample.Model;
using CoreLand.Framework;
using CoreLand.Framework.Code;

namespace EIP.Sample.Service
{
    /// <summary>
    /// 数据字典服务
    /// </summary>
    public class CodeManagerService : BaseService, ICodeManagerService
    {
        ///// <summary>
        ///// 根据编码缓存
        ///// </summary>
        //public static List<CodeMaster> codeDic = new List<CodeMaster>();

        ///// <summary>
        ///// 根据父Id缓存
        ///// </summary>
        //public static Dictionary<string, List<CodeMaster>> ParentCodeDic = new Dictionary<string, List<CodeMaster>>();

        //private static object lockObj = new object();

        private EFRepository<CodeMaster> codeManagerRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="factory">仓储创建工厂</param>
        public CodeManagerService(IUnitOfWork uow, IRepositoryFactory factory)
            : base(uow, factory)
        {
            codeManagerRepository = this.CreateRepository<EFRepository<CodeMaster>>();
        }

        /// <summary>
        /// 根据查询字典数据中的相应的数据
        /// </summary>
        /// <returns>字典数据类型</returns>
        public IDictionary<string,CodeType> GetAllCode()
        {
            var result = new Dictionary<string,CodeType>();
            var codes = codeManagerRepository.LoadEntities(p => p.LogicDeleteFlag == false)
                .OrderBy(p=>p.ParentId).OrderBy(p=>p.ShowIndex).ToList();
            // 创建CodeType集合
            foreach (var code in codes.Where(p=>p.ParentId == 0).ToList())
            {
                var codeType = new CodeType(code.Code, code.Text, code.ShortText);

                // CodeType集合内添加字典数据
                codeType.AddRange(codes.Where(p => p.ParentId == code.Id).Select(p => new CodeInfo(p.Code, p.Text, p.ShortText)).ToList());

                result.Add(code.Code, codeType);
                
            }

            return result;
        }

        ///// <summary>
        ///// 根据编码获取数据字典信息
        ///// </summary>
        ///// <param name="codeStr">编码</param>
        ///// <returns>数据字典信息</returns>
        //public List<CodeMaster> GetCodeMasterByParentCode(string parentCode)
        //{
        //    List<CodeMaster> codeManagerList = new List<CodeMaster>();

        //    if (ParentCodeDic == null || ParentCodeDic.Count == 0)
        //    {
        //        InitCodeMaster();
        //    }

        //    if (ParentCodeDic != null && ParentCodeDic.ContainsKey(parentCode))
        //    {
        //        codeManagerList = ParentCodeDic.GetValue(parentCode);
        //    }

        //    return codeManagerList;
        //}

        ///// <summary>
        ///// 公用
        ///// 根据parentCode获取数据字典中值为 INT 类型的信息
        ///// 如:信息状态、查询类型
        ///// </summary>
        ///// <param name="parentId">父级Id</param>
        ///// <returns>数据字典信息</returns>
        //public IEnumerable<ICodeEntity> GetDropDownListDataByParentCode(string parentCode)
        //{
        //    var list = GetCodeMasterByParentCode(parentCode).OrderBy(p => p.ShowIndex).ToList();

        //    var result = Mapper.Map<List<CodeMaster>, List<CodeDataModel>>(list);

        //    return result;
        //}

        ///// <summary>
        ///// 公用
        ///// 根据parentCode获取数据字典数据
        ///// </summary>
        ///// <param name="parentCode">父级Code</param>
        ///// <returns>返回树形的数据字典信息</returns>
        //public List<TreeListDataModel> GetTreeListDataByParentCode(string parentCode)
        //{
        //    var list = GetCodeMasterByParentCode(parentCode).OrderBy(p => p.ShowIndex);

        //    var result = new List<TreeListDataModel>();

        //    foreach (var item in list)
        //    {
        //        result.Add(new TreeListDataModel()
        //        {
        //            id = int.Parse(item.Value),
        //            pid = item.ParentId,
        //            text = item.Text
        //        });
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// 根据父级编码和值查询对应的CodeMaster
        ///// 场景：在列表中根据状态的值显示文本信息
        ///// </summary>
        ///// <param name="parentCode">父级编码</param>
        ///// <param name="value">值</param>
        ///// <returns></returns>
        //public CodeMaster QueryCodeMasterByParentCodeAndValue(string parentCode, string value)
        //{
        //    //如果 codeDic == null 则需要重新查询数据库数据
        //    if (codeDic == null || codeDic.Count <= 0)
        //    {
        //        InitCodeMaster();
        //    }

        //    //查询数据并返回
        //    var codeManager = codeDic.Find(p => p.Value == value && p.Code.Contains(parentCode));

        //    return codeManager;
        //}

        ///// <summary>
        ///// 根据Code查询 CodeMaster 对象
        ///// </summary>
        ///// <param name="code">待查询的code</param>
        ///// <returns></returns>
        //public CodeMaster QueryCodeMasterByCode(string code)
        //{
        //    if (codeDic == null || codeDic.Count == 0)
        //    {
        //        InitCodeMaster();
        //    }

        //    return codeDic.Where(p => p.Code == code).FirstOrDefault();
        //}

        /// <summary>
        /// 初始化数据字典信息
        /// </summary>
        public void InitCodeManager()
        {
            // 数据字典载入
            CodeManger.Initialize(this);
 
        }

        #region 后台数据字典操作

        /// <summary>
        /// 获取所有数据字典
        /// </summary>
        /// <returns></returns>
        public List<CodeMasterListViewModel> QueryCodeMaster(QueryModel model)
        {
            //查询数据
            var key = (string.IsNullOrEmpty(model.Key) ? "" : model.Key.Trim());

            var codeManager = this.codeManagerRepository.LoadEntities(p => p.Text.Contains(key) || p.Code.Contains(key))
                                                              .ToList();

            //对象转换
            var result = Mapper.Map<List<CodeMaster>, List<CodeMasterListViewModel>>(codeManager);

            return result;
        }

        /// <summary>
        /// 根据标识数据字典
        /// </summary>
        /// <param name="id">分类标识</param>
        /// <returns>数据字典信息</returns>
        public CodeMasterSaveViewModel GetCodeMaster(int id)
        {
            var codeManager = this.codeManagerRepository.Find(id);

            if (codeManager == null)
            {
                throw new CLApplicationException("W10010");
            }

            var result = new CodeMasterSaveViewModel()
            {
                Id = codeManager.Id,
                ParentId = codeManager.ParentId,
                Code = codeManager.Code,
                Text = codeManager.Text,
                Pinyin = codeManager.Pinyin,
                ShortText = codeManager.ShortText,
                ShowIndex = codeManager.ShowIndex
            };

            return result;
        }

        /// <summary>
        /// 处理数据字典
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">数据字典信息</param>
        /// <returns>处理是否成功</returns>
        public bool SaveCodeMaster(CodeMasterSaveViewModel model)
        {
            //检查是否存在重复数据
            this.HadCodeMaster(model.ParentId, model.Code, model.Id);

            //scoreCategory.Id > 0 更新数据 ， 否则新增数据
            if (model.Id > 0)
            {
                //更新数据时，需要先查询原始数据
                var rawCodeMaster = this.codeManagerRepository.Find(model.Id);

                //将需要更新的数据进行赋值
                var codeManager = Mapper.Map<CodeMasterSaveViewModel, CodeMaster>(model, rawCodeMaster);

                //更新数据
                this.codeManagerRepository.Update(codeManager);
            }
            else
            {
                //将需要录入的数据进行对象转换
                var codeManager = Mapper.Map<CodeMasterSaveViewModel, CodeMaster>(model);

                //录入数据
                this.codeManagerRepository.Insert(codeManager);
            }

            return true;
        }

        /// <summary>
        /// 根据上级ID和名称查询积分分类中是否存在相同数据
        /// </summary>
        /// <param name="parentId">上级ID</param>
        /// <param name="modelName">分类名称</param>
        /// <param name="modelId">分类编号</param>
        /// <returns>存在返回 ScoreCategory 对象，否则返回 nulll </returns>
        public bool HadCodeMaster(int? parentId, string modelCode, int modelId)
        {
            var rawScoreCategory = this.codeManagerRepository.LoadEntities(p => p.ParentId == parentId &&
                                                                                  p.Code == modelCode
                                                                             )
                                                                             .FirstOrDefault();

            //如果数据库中不存在对应的记录，则不存在相同数据
            if (rawScoreCategory == null)
            {
                return true;
            }

            // modelId > 0 时 作为已有数据验证，modelId = 0 时 作为新增数据验证
            if (modelId > 0)
            {
                //修改数据
                var scoreCategory = this.codeManagerRepository.Find(modelId);

                if (scoreCategory == null)
                {
                    throw new CLApplicationException("W10020");
                }

                if (rawScoreCategory.Id != scoreCategory.Id)
                {
                    throw new CLApplicationException("W10030");
                }
            }
            else
            {
                //新增数据
                if (rawScoreCategory != null)
                {
                    throw new CLApplicationException("W10030");
                }
            }

            return true;
        }


        /// <summary>
        /// 获取所有数据字典
        /// </summary>
        /// <returns>所有数据字典信息</returns>
        public List<TreeListDataModel> QueryTreeListCodeMaster()
        {
            var codeManager = this.codeManagerRepository.LoadEntities(p => true).ToList();

            var result = Mapper.Map<List<CodeMaster>, List<TreeListDataModel>>(codeManager);

            return result;
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="id">数据字典id</param>
        /// <returns>删除是否成功</returns>
        public bool DeleteCodeMaster(int id)
        {
            //根据编号查询待删除数据
            var codeManager = this.codeManagerRepository.Find(id);

            //如果数据信息为 null 则数据不存在
            if (codeManager == null)
            {
                throw new CLApplicationException("W10020");
            }

            //执行删除数据操作
            this.codeManagerRepository.LogicDelete(id);

            return true;
        }

        #endregion

    }
}
