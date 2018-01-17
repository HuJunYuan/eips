using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;

using EIP.Model;
using EIP.Entity;
using EIP.Model.ViewModels;
namespace EIP.Service.AutoMapper.Profiles
{
    public class StudentProfile : BaseProfile
    {
        protected override void Configure()
        {
            //数据字典添加映射
            CreateMap<CodeMasterSaveViewModel, CodeMaster>()
                .ForMember(des => des.PrepareField1, map => map.Ignore())
                .ForMember(des => des.PrepareField2, map => map.Ignore())
                .ForMember(des => des.PrepareField3, map => map.Ignore())
                .ForMember(des => des.PrepareField4, map => map.Ignore())
                .ForMember(des => des.ParentId, map => { map.MapFrom(sou => sou.ParentId.HasValue ? sou.ParentId : 0); });






            CreateMap<StudentClassManagmentModel, Remo>()
                .ForMember(des => des.Other, map => map.Ignore())
                 .ForMember(des => des.College, map => map.Ignore())
                .ForMember(des => des.LogicDeleteFlag, map => map.Ignore())
                .ForMember(des => des.CreateUserId, map => map.Ignore())
                .ForMember(des => des.CreateUserName, map => map.Ignore())
                .ForMember(des => des.CreateTime, map => map.Ignore())
                .ForMember(des => des.LastUpdateUserId, map => map.Ignore())
                .ForMember(des => des.LastUpdateUserName, map => map.Ignore())
                .ForMember(des => des.LastUpdateTime, map => map.Ignore())
                .ForMember(des => des.entorToRemos, map => map.Ignore())
                .ForMember(des => des.Grades, map => map.Ignore());




            CreateMap<StudentManagmentModel, Grade>()
                .ForMember(des => des.Other, map => map.Ignore())
                .ForMember(des => des.LogicDeleteFlag, map => map.Ignore())
                .ForMember(des => des.CreateUserId, map => map.Ignore())
                .ForMember(des => des.CreateUserName, map => map.Ignore())
                .ForMember(des => des.CreateTime, map => map.Ignore())
                .ForMember(des => des.LastUpdateUserId, map => map.Ignore())
                .ForMember(des => des.LastUpdateUserName, map => map.Ignore())
                .ForMember(des => des.LastUpdateTime, map => map.Ignore())
                .ForMember(des => des.Remo, map => map.Ignore());

            CreateMap<Grade, Grade>();

            CreateMap<Remo, Remo>();

           CreateMap<entorToRemo, entorToRemo>();

            CreateMap<Classroom, Classroom>();
            CreateMap<College, College>();
            CreateMap<entor, entor>();

            CreateMap<entor, entorViewModel>()
                .ForMember(des => des.SexName, map => map.Ignore());

            CreateMap<Grade, GradeViewModel>()
                .ForMember(des => des.SexName, map => map.Ignore())
                .ForMember(des => des.Remo_id, map => map.Ignore())
                .ForMember(des => des.AreaName, map => map.Ignore());

            CreateMap<Local, Local>();
            //CreateMap<Student, StudentViewModel>()
            //     .ForMember(viewStu => viewStu.Sex, stu => stu.MapFrom(sou => CodeManger.GetCodeText(CommonConstant.CODETYPE_SEX, sou.Sex)))










        }
    }
}