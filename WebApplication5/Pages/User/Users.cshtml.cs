using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication5.Pages.User
{
    public class UsersModel : PageModel
    {
        private readonly DbUser _context;

        public UsersModel(DbUser context)
        {
            _context = context;
        }

        public List<UserInfo> Users { get; set; } = new();
        public async Task OnGetAsync()
        {
            var search = _context.UserList.AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                search = search.Where(n => n.Name.Contains(SearchString));
            }

            Users = await search.ToListAsync();

        }

        [BindProperty]
        public UserInfo UserInfo { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(UserName))
                return Page();

            var user = new UserInfo { Name = UserName };
            _context.UserList.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { SearchString });
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            Console.WriteLine($"Deleting user with id: {id}");
            var user = await _context.UserList.FindAsync(id);
            if (user != null)
            {
                _context.UserList.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            return RedirectToPage(new { SearchString });

        }
        public IList<UserInfo> NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        
    }
}
