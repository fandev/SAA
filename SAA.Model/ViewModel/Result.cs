using SAA.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Model.ViewModel
{
    public class Result
    {
        public Result()
        {
            Acoes = new List<Acao>();
        }

        [Description("Representa um código de negócio para auxiliar na decisão do cliente")]
        public string Codigo { get; set; }

        [Description("Retorna uma mensagem de negócio legível por humanos")]
        public string Mensagem { get; set; }

        public string TransacaoNome { get; set; }
        public string TransacaoHash { get; set; }
        public virtual List<Acao> Acoes { get; set; }

        public StatusResult StatusResult { get; set; }
        
    }

    public enum StatusResult
    {
        Exception = -1,
        Autorizado = 1,
        NaoAutorizado = 0,
        TransacaoNaoCadastrada = 2,
        AcaoNaoCadastrada = 3,
        AcoesNaoCadastradas = 4
    }
}
