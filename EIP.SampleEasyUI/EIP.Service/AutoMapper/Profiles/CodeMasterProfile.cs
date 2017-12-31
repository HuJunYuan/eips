using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;

using EIP.Model;
using EIP.Entity;

namespace EIP.Service.AutoMapper.Profiles
{
    public class CodeMasterProfile : BaseProfile
    {
        protected override void Configure()
        {

            //数据字典视图映射
            CreateMap<AppCodeMaster, TreeDataModel>()
                .ForMember(des => des.Id, map => { map.MapFrom(sou => sou.Id); })
                .ForMember(des => des.Text, map => { map.MapFrom(sou => string.Format("{0}({1})", sou.Text,sou.Code)); })
                .ForMember(des => des.State, map => map.Ignore())
                .ForMember(des => des.Childrens, map => map.Ignore())
                //.ForMember(des => des.State, map => { map.MapFrom(sou => sou.Childrens.Count(p => p.LogicDeleteFlag == false) == 0 ? "open" : "closed"); })
                .ForMember(des => des.Attributes, map => { map.MapFrom(sou => new { AppId = sou.AppId, PId = sou.ParentId.ToString() }); })
                .ForMember(des => des.Checked, map => map.Ignore())
                .ForMember(des => des.IconCls, map => map.Ignore());


            CreateMap<CodeMasterViewModel, string>()
           .ConvertUsing<CodeMasterConvert>();


            //数据字典列表映射
            CreateMap<CodeMaster, CodeMasterListViewModel>();

            //数据字典更新视图映射
            CreateMap<CodeMaster, CodeMasterSaveViewModel>();

            //数据字典添加映射
            CreateMap<CodeMasterSaveViewModel, CodeMaster>()
                .ForMember(des => des.PrepareField1, map => map.Ignore())
                .ForMember(des => des.PrepareField2, map => map.Ignore())
                .ForMember(des => des.PrepareField3, map => map.Ignore())
                .ForMember(des => des.PrepareField4, map => map.Ignore())
                .ForMember(des => des.ParentId, map => { map.MapFrom(sou => sou.ParentId.HasValue ? sou.ParentId : 0); });

        }
    }

    public class CodeMasterConvert : ITypeConverter<CodeMasterViewModel, string>
    {
        public string Convert(ResolutionContext context)
        {
            //转化的源
            var sou = context.SourceValue as CodeMasterViewModel;
            return CoreLand.Framework.Code.CodeManger.GetCodeText(sou.ParentCode, sou.Value);
        }
    }
}