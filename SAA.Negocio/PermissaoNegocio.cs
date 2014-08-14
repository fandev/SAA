using SAA.Infra;
using SAA.Model.Models;
using SAA.Model.ViewModel;
using SAA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Negocio
{
    public class PermissaoNegocio
    {
        public PermissaoNegocio()
        {
        }

        public Result VerificaPermissaoAcao(Acao acao, FuncionarioPerfilPermissao userPermissao = null)
        {
            Result result = new Result();

            if (acao == null)
            {
                result.Codigo = "3";
                result.Mensagem = "Acao Não cadastrada";
                return result;
            }

            result.TransacaoNome = acao.Transacao.Nome;
            result.TransacaoHash = acao.Transacao.Hash;
            acao.Transacao = null;

            if (userPermissao == null)
            {
                result.Codigo = "0";
                result.Mensagem = "Não autorizado";
                return result;
            }
            else
            {
                result.Codigo = "1";
                result.Mensagem = "Autorizado";
                acao.Autorizado = true;
                acao.FuncionarioPerfilPermissaos = null;
                result.Acoes.Add(acao);
            }

            return result;
        }

        public Result VerificaPermissaoTransacao(Transacao transacao, List<FuncionarioPerfilPermissao> userPermissoes)
        {
            Result result = new Result();
            if (transacao == null)
            {
                result.Codigo = "2";
                result.Mensagem = "Transação não cadastrada";
                return result;
            }
            
            result.TransacaoNome = transacao.Nome;
            result.TransacaoHash = transacao.Hash;
            
            if (transacao.Acaos.Count() == 0)
            {
                result.Codigo = "4";
                result.Mensagem = "Ações não cadastradas";                
                return result;
            }

            if (userPermissoes.Count() == 0)
            {
                result.Codigo = "0";
                result.Mensagem = "Não autorizado";
                return result;
            }

            foreach (var acao in transacao.Acaos)
            {
                acao.Autorizado = userPermissoes.Any(x => x.IdAcao == acao.Id);
                acao.Transacao = null;
                acao.FuncionarioPerfilPermissaos = null;
            }

            result.Codigo = "1";
            result.Mensagem = "Autorizado";
            result.Acoes = transacao.Acaos.ToList();
            return result;
        }
    }
}
