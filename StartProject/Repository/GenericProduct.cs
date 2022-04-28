using StartProject.Entities;
using StartProject.Interfaces;

namespace StartProject.Repository
{
    public class GenericProduct : GenericRepository<Product>, IProduct
    {
        public Task<List<Product>> ListagemCustomizada()
        {
            throw new NotImplementedException();
        }
    }
}
