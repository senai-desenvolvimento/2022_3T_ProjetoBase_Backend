using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patrimonio.Contexts;
using Patrimonio.Domains;
using Patrimonio.Utils;

namespace Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentosController : ControllerBase
    {
        private readonly PatrimonioContext _context;

        public EquipamentosController(PatrimonioContext context)
        {
            _context = context;
        }

        // GET: api/Equipamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipamento>>> GetEquipamentos()
        {
            return await _context.Equipamentos.ToListAsync();
        }

        // GET: api/Equipamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipamento>> GetEquipamento(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);

            if (equipamento == null)
            {
                return NotFound();
            }

            return equipamento;
        }

        // PUT: api/Equipamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipamento(int id, Equipamento equipamento)
        {
            if (id != equipamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(equipamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Equipamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Equipamento>> PostEquipamento([FromForm] Equipamento equipamento, IFormFile arquivo)
        {

            #region Upload da Imagem com extensões permitidas apenas
                string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas);

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado");
                }

                if (uploadResultado == "Extensão não permitida")
                {
                    return BadRequest("Extensão de arquivo não permitida");
                }

                equipamento.Imagem = uploadResultado; 
            #endregion

            // Pegando o horário do sistema
            equipamento.DataCadastro = DateTime.Now;

            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            return Created("Equipamento", equipamento);
        }

        // DELETE: api/Equipamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipamento(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);
            if (equipamento == null)
            {
                return NotFound();
            }

            _context.Equipamentos.Remove(equipamento);
            await _context.SaveChangesAsync();

            // Removendo Arquivo do servidor
            Upload.RemoverArquivo(equipamento.Imagem);

            return NoContent();
        }

        private bool EquipamentoExists(int id)
        {
            return _context.Equipamentos.Any(e => e.Id == id);
        }
    }
}
