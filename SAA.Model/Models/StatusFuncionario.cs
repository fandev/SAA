using System;
using System.Collections.Generic;
using SAA.Infra;

namespace SAA.Model.Models
{
    public partial class StatusFuncionario : ModelBase
    {

	
        public StatusFuncionario()
        {
            this.Funcionarios = new List<Funcionario>();
        }

		public override int Id { get; set; }
		      public string Codigo { get; set; }
      public string Descricao { get; set; }
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
    }
}
