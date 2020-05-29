using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//Для добавления миграции необходимо добавить nutget пакет
//Microsoft.EntityFrameworkCore.Tools
namespace WebAPI.Models
{
    public class WorkerDetailContext :DbContext
    {
        public WorkerDetailContext(DbContextOptions<WorkerDetailContext> options) :base(options)
        {

        }

        public DbSet<WorkerDetail> WorkerDetails { get; set; }
    }
}
