﻿using AutoMapper;
using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.ViewModels.Usuario;
using SistemaCred9.Web.UI.ViewModels.Venda;

namespace SistemaCred9.Web.UI.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
 
        protected override void Configure()
        {
            Mapper.CreateMap<UsuarioViewModel, Usuario>();
            Mapper.CreateMap<TrocarSenhaViewModel, Usuario>();
            Mapper.CreateMap<VendaViewModel, Venda>();
        }

    }
}