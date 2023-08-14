using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoUniversity.Models;
using ContosoUniversity.Models.States;

namespace ContosoUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            StudentVM = new StudentVM() { EnrollmentDate = DateTime.Now, FirstMidName = "Joe", LastName = "Smith" };
            return Page();
        }

        [BindProperty]
        public StudentVM StudentVM { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");過剰ポスティングに脆弱*/
            var emptyStudent = new Student();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Add(new Student());
            entry.CurrentValues.SetValues(StudentVM);//プロパティ名が一致していればいい
            await _context.SaveChangesAsync();
            /*
            if (await TryUpdateModelAsync<Student>(
                    emptyStudent,
                    "Student", // Prefix for form value. studentが含まれるフォームフィールドを探索
                    s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate)
                )
                    //リストされたプロパティのみ更新 ハッカーによって不正なプロパティの変更を防ぐため
            {//Studentを作成し、フォームフィールドからプロパティを更新
                //必要なプロパティしか持っていないViewModelを作成するのも有効
                _context.Students.Add(emptyStudent);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }*/

            return RedirectToPage("./Index");
        }
        
    }
}
