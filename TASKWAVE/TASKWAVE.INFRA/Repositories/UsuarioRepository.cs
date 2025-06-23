using Microsoft.EntityFrameworkCore;
using TASKWAVE.INFRA.Data;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;


namespace TASKWAVE.INFRA.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TaskWaveContext _context;

        public UsuarioRepository(TaskWaveContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Usuario entity)
        {
            await _context.Usuarios.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateUserToEquip(Usuario entity, int teamId)
        {
            await _context.Usuarios.AddAsync(entity);
            await _context.SaveChangesAsync();

            var equipes = await _context.Equipes
            .Include(equipe => equipe.Usuarios)
            .FirstOrDefaultAsync(equipe => equipe.IdEquipe == teamId);

            var usuario = await _context.Usuarios.FindAsync(entity.IdUsuario);

            if (!equipes.Usuarios.Contains(usuario))
            {
                equipes.Usuarios.Add(usuario);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Usuario entity)
        {
            _context.Usuarios.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.EmailUsuario == email.Trim());
        }

        public async Task<Usuario> GetByEmailWithAccessesAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.Acessos)
                .FirstOrDefaultAsync(u => u.EmailUsuario == email);
        }
    }
}

