using System;
using System.Collections.Generic;
using System.Text;
using Infra.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace U2U_HRS_Infra
{
  public class ToDoDbContextFactory : DesignTimeDbContextFactory<ToDoDb> 
  {
    public ToDoDbContextFactory() : base(ToDoDb.ConnectionStringName) { }

    protected override ToDoDb CreateDbContext(DbContextOptions<ToDoDb> options) 
      => new ToDoDb(options);
  }
}
