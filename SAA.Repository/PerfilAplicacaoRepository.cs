﻿using SAA.Infra;
using SAA.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Repository
{
    public class PerfilAplicacaoRepository : RepositoryBase<PerfilAplicacao>
    {
        public PerfilAplicacaoRepository(DbContext context) : base(context) { }

    }
}