namespace LibraryApi.Models;

public class Book
{
    public int Id { get; set; }
    // [Required(ErrorMessage = "Book Title is required")]
    // [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    // Foreign Key: ဒီစာအုပ်က ဘယ် Category ထဲမှာ ရှိတာလဲ
    // public int CategoryId { get; set; }

    // // Navigation Property: Category အချက်အလက်ကို တိုက်ရိုက်ယူသုံးဖို့
    // public Category? Category { get; set; }

}

