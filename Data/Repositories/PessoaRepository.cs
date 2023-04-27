using Data.Context;
using Data.Repositories.Abstractions;
using Domain;

namespace Data.Repositories
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(AppDbContext appContext) : base(appContext)
        {

        }
    }
}