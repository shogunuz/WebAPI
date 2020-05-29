using Microsoft.EntityFrameworkCore;

//Для добавления миграции необходимо добавить nutget пакет
//Microsoft.EntityFrameworkCore.Tools
namespace WebAPI.Models
{
    public class WorkerHolidayContext : DbContext
    {
        public WorkerHolidayContext(DbContextOptions<WorkerHolidayContext> options) : base(options)
        {

        }

        public DbSet<WorkerHoliday> WorkerHolidays { get; set; }
    }
}
