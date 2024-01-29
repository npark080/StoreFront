using Microsoft.AspNetCore.Mvc;

using StoreFront.DATA.EF.Models;
using Microsoft.AspNetCore.Identity;
using StoreFront.UI.MVC.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace StoreFront.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly StardewContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(StardewContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var localCart = GetCart();
            if (!localCart.Any())
            {
                ViewBag.Message = "There are no items in your cart";
            }
            return View(localCart);
        }

        public IActionResult AddToCart(int id)
        {
            Dictionary<int, CartItemViewModel> localCart = GetCart();

            Product? p = _context.Products.Find(id);
            if (p == null)
            {
                ViewBag.Message = "Invalid Product ID.";
                return RedirectToAction(nameof(Index));
            }

            if (localCart.ContainsKey(p.ProductId))
            {
                localCart[p.ProductId].Qty++;
            }
            else
            {

                localCart.Add(p.ProductId, new CartItemViewModel(1, p));
            }

            SetCart(localCart);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            Dictionary<int, CartItemViewModel> localCart = GetCart();
            if (localCart.ContainsKey(id))
            {
                localCart.Remove(id);
            }
            SetCart(localCart);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateCart(int id, int qty)
        {
            if (qty <= 0)
            {
                RemoveFromCart(id);
            }
            var localCart = GetCart();
            if (localCart.ContainsKey(id))
            {
                localCart[id].Qty = qty;
            }
            SetCart(localCart);

            return RedirectToAction(nameof(Index));
        }

        //GET
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            if (!GetCart().Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ud = await _context.Users.FindAsync(userId);

            CheckoutViewModel model = new()
            {
                ShipToName = (ud?.FirstName + " " + ud?.LastName).Trim(),
                ShipRegion = ud?.Region,
                Address = ud?.Address,
                ShipCity = ud?.City,
                ShipState = ud?.State,
                ShipZip = ud?.Zip
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Order o = new()
            {
                OrderDate = DateTime.Now,
                UserId = userId,
                ShipToName = model.ShipToName,
                ShipRegion = model.ShipRegion,
                ShipCity = model.ShipCity,
                ShipState = model.ShipState,
                ShipZip = model.ShipZip
            };
            var localCart = GetCart();
            _context.Orders.Add(o);
            foreach (var item in localCart.Values)
            {
                OrderDetail od = new()
                {
                    OrderId = o.OrderId,
                    ProductId = item.Product.ProductId,
                    Price = item.Product.Price,
                    Quantity = (short)item.Qty
                };
                o.OrderDetails.Add(od);
            }
            await _context.SaveChangesAsync();

            SetCart(new());

            return RedirectToAction(nameof(Index), "Orders");
        }
        #region Json Methods

        private Dictionary<int, CartItemViewModel> GetCart()
        {
            var jsonCart = HttpContext.Session.GetString("cart");
            ViewBag.Session = jsonCart;
            if (string.IsNullOrEmpty(jsonCart))
            {
                return new();
            }

            return JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(jsonCart);
        }

        private void SetCart(Dictionary<int, CartItemViewModel> localCart)
        {
            if (!localCart.Any())
            {
                HttpContext.Session.Remove("cart");
            }
            else
            {
                var jsonCart = JsonConvert.SerializeObject(localCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }
        }
        #endregion
    }
}