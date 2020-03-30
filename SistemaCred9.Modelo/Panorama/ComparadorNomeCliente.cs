using System.Collections.Generic;

namespace SistemaCred9.Modelo.Panorama
{
    public class ComparadorNomeCliente : IEqualityComparer<Cliente>
    {
        public bool Equals(Cliente x, Cliente y)
        {
            return (x.Id == y.Id);
        }

        public int GetHashCode(Cliente obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
