using Patrimonio.Contexts;
using Patrimonio.Domains;
using Patrimonio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patrimonio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly PatrimonioContext ctx;

        public UsuarioRepository(PatrimonioContext appContext)
        {
            ctx = appContext;
        }

        public Usuario Login(string email, string senha)
        {   
            // Procuramos o usuário pelo email primeiro
            var usuario = ctx.Usuarios.FirstOrDefault(u => u.Email == email);

            if(usuario != null)
            {
                // Com o usuário encontrado, temos a hash da senha para poder comparar com a nova entrada pelo input de senha
                var comparado = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);
                if(comparado)
                    return usuario;
            }

            return null;
        }
    }
}
