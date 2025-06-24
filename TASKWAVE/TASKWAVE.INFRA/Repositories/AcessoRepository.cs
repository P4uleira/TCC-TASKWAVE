using Microsoft.EntityFrameworkCore;
using TASKWAVE.INFRA.Data;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;


namespace TASKWAVE.INFRA.Repositories
{
    public class AcessoRepository : IAcessoRepository
    {
        private readonly TaskWaveContext _context;

        public AcessoRepository(TaskWaveContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Acesso access)
        {
            await _context.Acessos.AddAsync(access);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAccessToUser(int idAccess, int idUser)
        {
            var access = await _context.Acessos
             .Include(access => access.Usuarios) 
             .FirstOrDefaultAsync(access => access.IdAcesso == idAccess);

            var user = await _context.Usuarios.FindAsync(idUser);

            if (access == null || user == null)
            {
                throw new Exception("Acesso ou Usuário não encontrados.");
            }

            
            if (!access.Usuarios.Any(p => p.IdUsuario == idUser))
            {
                access.Usuarios.Add(user); 
                await _context.SaveChangesAsync(); 
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var acesso = await _context.Acessos.FindAsync(id);
            if (acesso != null)
            {
                _context.Acessos.Remove(acesso);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Acesso access)
        {
            _context.Acessos.Update(access);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Acesso>> GetAllAsync()
        {
            return await _context.Acessos.ToListAsync();
        }

        public async Task<Acesso> GetByIdAsync(int idAccess)
        {
            return await _context.Acessos.FindAsync(idAccess);
        }

        public async Task<IEnumerable<(int accessId, string accessName, int userId, string userName)>> GetAccessUserLinksAsync(int? accessId, int? userId)
        {
            var query = _context.Acessos
                .Include(e => e.Usuarios)
                .AsQueryable();

            if (accessId.HasValue)
                query = query.Where(a => a.IdAcesso == accessId.Value);

            var result = await query
                .SelectMany(e => e.Usuarios
                    .Where(u => !userId.HasValue || u.IdUsuario == userId.Value)
                    .Select(u => new
                    {
                        accessId = e.IdAcesso,
                        accessName = e.NomeAcesso,
                        userId = u.IdUsuario,
                        userName = u.NomeUsuario
                    }))
                .ToListAsync();

            return result.Select(x => (x.accessId, x.accessName, x.userId, x.userName));
        }

        public async Task DeleteAccessInUser(int accessId, int userId)
        {
            var access = await _context.Acessos
                .Include(a => a.Usuarios)
                .FirstOrDefaultAsync(a => a.IdAcesso == accessId);

            if (access == null)
                throw new Exception("Acesso não encontrada.");

            var user = access.Usuarios.FirstOrDefault(u => u.IdUsuario == userId);

            if (user == null)
                throw new Exception("Usuário não está vinculado a esta equipe.");

            access.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

