using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using System.Configuration;
using SistemaCred9.Modelo;

namespace SistemaCred9.Web.UI.Security
{
    public class PermissaoProvider : RoleProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Cred9DbContext"].ConnectionString;

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //try
            //{
            //    UsuarioBusiness usuarioBus = new UsuarioBusiness(_connectionString);

            //    UsuarioModel usuario = usuarioBus.SelecionarPorLoginComRoles(username);

            //    if (usuario == null)
            //        return new string[] { };

            //    return usuario.Roles.Select(role => role.Descricao).ToArray();
            //}
            //catch 
            //{
            return Role.Roles;
            //}            
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}