using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        public Restaurant Restaurant { get; set; }
        [TempData]
        public string Message { get; set; }
        private readonly IRestaurantData _restaurantData;
        public DetailModel(IRestaurantData restaurantData)
        {
            this._restaurantData = restaurantData;

        }
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = _restaurantData.GetById(restaurantId);
            if (Restaurant ==null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
