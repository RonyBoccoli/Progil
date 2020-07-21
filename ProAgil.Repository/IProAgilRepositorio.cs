using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepositorio
    {

        //base
         void Add<T> (T entity) where T : class;
         void Update<T> (T entity) where T : class;
         void Delete<T> (T entity) where T : class;

         Task<bool> SaveChngesAsync();

        //
        Task<Evento[]> GetALLEventoAsincByTema(string tema, bool includePalestrante);
        Task<Evento[]> GetALLEventoAsincBy(bool incldePalestrante);
        Task<Evento> GetALLEventoAsincById(int Id, bool includePalestrante);
        Task<Palestrante[]> GetALLPalestrantesAsincByName(string Name, bool includeEvento);
        Task<Palestrante> GetALLPalestranteAsincById(int PalestranteId, bool includeEvento);
    }
}