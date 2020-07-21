using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepositorio
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext _context)
        {
            this._context = _context;
            //para nao travar o sistema na hroa do post put ou delite por ter que buscar e endiade
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
          public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChngesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetALLEventoAsincBy(bool incldePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(incldePalestrante){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }
            
            query = query.OrderByDescending(c => c.DataEvento);
            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetALLEventoAsincById(int Id, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }
            
            query = query.AsNoTracking()
                    .OrderByDescending(c => c.DataEvento)
                    .Where(c => c.Id == Id);
            return await query.FirstAsync();
        }

        public async Task<Evento[]> GetALLEventoAsincByTema(string tema, bool includePalestrante = false)
        {
             IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }
            
            query = query.AsNoTracking()
                    .OrderByDescending(c => c.DataEvento)
                    .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));
                    
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetALLPalestranteAsincById(int PalestranteId, bool includeEvento)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedesSociais);

            if(includeEvento){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }
            

            query = query.AsNoTracking()
            .Where(p => p.Id == PalestranteId)
            .OrderBy(p => p.Nome);
            return await query.FirstOrDefaultAsync();
  
        }

        public async Task<Palestrante[]> GetALLPalestrantesAsincByName(string Name, bool includeEvento)
        {
              IQueryable<Palestrante> query = _context.Palestrantes
              .Include(c => c.RedesSociais);

            if(includeEvento){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }
            
            query = query.AsNoTracking()
            .Where(p => p.Nome.ToLower().Contains(Name.ToLower()))
            .OrderBy(p => p.Nome);
                    
            return await query.ToArrayAsync(); 
        }
    }
}