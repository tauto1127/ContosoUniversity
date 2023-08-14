using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Students;

public class EditModel : PageModel
{
    private readonly ContosoUniversity.Data.SchoolContext _context;

    [BindProperty] 
    public Student Student { get; set; }

    public EditModel(ContosoUniversity.Data.SchoolContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Student = await _context.Students.FindAsync(id); //FirstOrDefaultAsyncから変更

        if (Student == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var studentToUpdate = await _context.Students.FindAsync(id);
        if (studentToUpdate == null)
        {
            return NotFound();
        }
        
        /*
        if (await TryUpdateModelAsync<Student>(
                studentToUpdate,
                "Student",
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate)
            )
        {
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }*/

        studentToUpdate.EnrollmentDate = Student.EnrollmentDate;
        studentToUpdate.LastName = Student.LastName;
        studentToUpdate.FirstMidName = Student.FirstMidName;
        _context.SaveChanges();
        return RedirectToPage("./Index");
    }
}