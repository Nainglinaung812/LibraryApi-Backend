using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // ဒီကောင်က EntityState နဲ့ ToListAsync အတွက် အရေးကြီးတယ်
using LibraryApi.Data;
using LibraryApi.Models;// Book model ကိုသုံးဖို့

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")] // URL က api/books ဖြစ်သွားမယ်
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    // Constructor မှာ Database context ကို တောင်းယူမယ်
    public BooksController(AppDbContext context)
    {
        _context = context;
    }
    // စမ်းသပ်ဖို့အတွက် Data အတုလေးတွေ အရင်ထည့်ထားမယ်
    // private static List<Book> _books = new List<Book>
    // {
    //     new Book { Id = 1, Title = "C# Mastery", Author = "Linn" },
    //     new Book { Id = 2, Title = "Web API Pro", Author = "Mg Mg" }
    // };
    //     private static List<LibraryApi.Models.Book> _books = new List<LibraryApi.Models.Book>
    // {
    //     new LibraryApi.Models.Book { Id = 1, Title = "C# Mastery", Author = "Linn" },
    //     new LibraryApi.Models.Book { Id = 2, Title = "Web API Pro", Author = "Mg Mg" }
    // };

    // [HttpGet("{id}")] // URL က api/books/1 လိုမျိုး ဖြစ်သွားမယ်
    // public ActionResult<Book> GetBook(int id)
    // {
    //     var book = _books.FirstOrDefault(b => b.Id == id);

    //     if (book == null)
    //     {
    //         return NotFound(); // စာအုပ်ရှာမတွေ့ရင် 404 ပြမယ်
    //     }

    //     return Ok(book);
    // }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        // .ToListAsync<Book>() ဆိုပြီး Type ကို အတိအကျ ပြောလိုက်ပါ
        return await _context.Books.ToListAsync<Book>();
    }
    // [HttpPost] // Data အသစ်ဆောက်မယ် (POST method)
    // public ActionResult<Book> CreateBook(Book newBook)
    // {
    //     _books.Add(newBook);
    //     // ပြန်ပို့ပေးတဲ့အခါ စာအုပ်အသစ်လေး တကယ်ဝင်သွားကြောင်း ပြပေးမယ်
    //     return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
    // }
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync(); // Database ထဲ တကယ်သွားသိမ်းတာ

        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }
    // [HttpPut("{id}")] // api/books/1
    // public IActionResult UpdateBook(int id, Book updatedBook)
    // {
    //     var book = _books.FirstOrDefault(b => b.Id == id);
    //     if (book == null)
    //     {
    //         return NotFound(); // ပြင်မယ့်စာအုပ် ရှာမတွေ့ရင် 404 ပြမယ်
    //     }

    //     // အချက်အလက်တွေကို အသစ်လဲလိုက်မယ်
    //     book.Title = updatedBook.Title;
    //     book.Author = updatedBook.Author;

    //     return NoContent(); // 204 No Content - အောင်မြင်တယ်၊ ဒါပေမဲ့ ပြစရာ Data မရှိဘူးလို့ ပြောတာ
    // }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id) return BadRequest();

        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    // [HttpDelete("{id}")] // api/books/1
    // public IActionResult DeleteBook(int id)
    // {
    //     var book = _books.FirstOrDefault(b => b.Id == id);
    //     if (book == null)
    //     {
    //         return NotFound();
    //     }

    //     _books.Remove(book); // List ထဲကနေ ဖယ်ထုတ်လိုက်တာ

    //     return NoContent(); // အောင်မြင်စွာ ဖျက်ပြီးကြောင်း ပြမယ်
    // }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}