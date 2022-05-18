using APIngay15thang7.Model;
using Microsoft.EntityFrameworkCore;

namespace APIngay15thang7.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options ) : base( options )
        {

        }
        
        public DbSet<TodoList> TodoLists { get; set; }
    }
}
