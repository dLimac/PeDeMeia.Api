using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Service.ReceitaService
{
    public class CadastroReceitaService
    {
        public bool CadastroReceita(ReceitaInputModel model)
        {
            ReceitaEntity receita = new ReceitaEntity(0, model.Descricao, model.Categoria, model.Valor, model.PessoaId, model.DataReceita, model.ContaBancariaId);
            bool resultado = new ReceitaRepository().Cadastrar(receita);
            return resultado;
        }
    }
}
