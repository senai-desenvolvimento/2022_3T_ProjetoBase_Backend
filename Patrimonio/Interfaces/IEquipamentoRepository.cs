using Patrimonio.Domains;
using System.Collections;
using System.Collections.Generic;

namespace Patrimonio.Interfaces
{
    public interface IEquipamentoRepository
    {
        Equipamento Cadastrar(Equipamento equipamento);
        Equipamento Alterar(Equipamento equipamento);
        void Excluir(Equipamento equipamento);
        IEnumerable<Equipamento> Listar();
        Equipamento BuscarPorID(int id);
    }
}
