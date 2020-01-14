using SistemaCred9.EntityFramework.Mapping;
using SistemaCred9.Modelo;
using SistemaCred9.Modelo.Panorama;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SistemaCred9.EntityFramework.Context
{
    public class Cred9DbContext : DbContext
    {
        static Cred9DbContext()
        {
            Database.SetInitializer<Cred9DbContext>(null);
        }

        public Cred9DbContext() : base("Name=Cred9DbContext")
        {

        }

        public IDbSet<Usuario> Usuarios { get; set; }

        public IDbSet<Role> Permissoes { get; set; }

        public IDbSet<Venda> Venda { get; set; }

        public IDbSet<VendaStatusHistorico> VendaHistorico { get; set; }

        public IDbSet<Contrato> Contratos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new VendaMap());
            modelBuilder.Configurations.Add(new VendaStatusHistoricoMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new ContratoMap());
        }
    }
}
