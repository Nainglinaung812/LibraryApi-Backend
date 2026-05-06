// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// dotnet add package Microsoft.EntityFrameworkCore.Tools
// dotnet add package Microsoft.EntityFrameworkCore.Design
// အမှားကို ရှာဖို့အတွက် Terminal မှာ အောက်က command ကို အရင်ရိုက်ကြည့်ပါ-

// Bash
// dotnet build
using Microsoft.EntityFrameworkCore;
using LibraryApi.Models;// ညီလေးရဲ့ Model folder ကို ညွှန်းပါ
// using Microsoft.EntityFrameworkCore;
namespace LibraryApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // စာအုပ်တွေအတွက် Database Table ဆောက်ဖို့ ကြေညာတာ
    public DbSet<Book> Books { get; set; }
}