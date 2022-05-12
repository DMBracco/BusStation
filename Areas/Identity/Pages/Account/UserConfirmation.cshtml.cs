using BusStation.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BusStation.Areas.Identity.Pages.Account
{
    [Authorize]
    public class UserConfirmation : PageModel
    {
        UserManager<IdentityUser> _userManager;

        public UserConfirmation(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IList<IdentityUser> Users { get; set; } = new List<IdentityUser>();

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
        }
    }
}
