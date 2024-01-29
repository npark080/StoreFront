// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using StoreFront.DATA.EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;

namespace StoreFront.UI.MVC.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly StardewContext _context;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            StardewContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            #region Custom Fields
            //Custom Fields
#nullable enable
            [StringLength(50, ErrorMessage = "Maximum 50 characters")]
            [Display(Name = "First Name")]
            public string? FirstName { get; set; } = null!;

            [StringLength(50, ErrorMessage = "Maximum 50 characters")]
            [Display(Name = "Last Name")]
            public string? LastName { get; set; } = null!;

            [StringLength(50, ErrorMessage = "Maximum 50 characters")]
            public string? Region { get; set; }

            [StringLength(50, ErrorMessage = "Maximum 50 characters")]
            public string? Address { get; set; }

            [StringLength(50, ErrorMessage = "Maximum 50 characters")]
            public string? City { get; set; }

            [StringLength(2, MinimumLength = 2)]
            public string? State { get; set; }

            [StringLength(5, MinimumLength = 5)]
            public string? Zip { get; set; }

            [StringLength(24)]
            public string? Phone { get; set; }
#nullable disable
            #endregion
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            var u = await _context.Users.FindAsync(user.Id);
            if (u == null)
            {
                u = new() { UserId = user.Id };
                _context.Add(u);
                await _context.SaveChangesAsync();
            }
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = u?.FirstName,
                LastName = u?.LastName,
                Region = u?.Region,
                Address = u?.Address,
                City = u?.City,
                State = u?.State,
                Zip = u?.Zip,
                Phone = u?.Phone
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            #region Custom User Details Update

            {
                User u = await _context.Users.FindAsync(user.Id);
                u.FirstName = Input.FirstName;
                u.LastName = Input.LastName;
                u.Region = Input.Region;
                u.Address = Input.Address;
                u.City = Input.City;
                u.State = Input.State;
                u.Zip = Input.Zip;
                u.Phone = Input.Phone;

                _context.Update(u);
                await _context.SaveChangesAsync();
            }
            #endregion
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
