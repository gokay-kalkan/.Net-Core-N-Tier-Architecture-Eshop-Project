using BusinessLayer.Concrete;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entity;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Controllers
{
    public class OrderController : Controller
    {
        OrderManager om = new OrderManager(new EfOrderDal());
        CartManager cm = new CartManager(new EfCartDal());
        ProductManager pm = new ProductManager(new EfProductDal());
        DataContext db = new DataContext();
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ToOrder(int id)
        {
            var model = cm.GetById(id);
            //ViewBag.username = HttpContext.Session.GetString("FullName");

            //ViewBag.userid = HttpContext.Session.GetString("Id");

            //ViewBag.productid = model.ProductId;
            ViewBag.cartid = id;
            TempData["Id"] = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToOrderPost(int id, Order data)
        {
            var fullname = HttpContext.Session.GetString("FullName");
            var user = HttpContext.Session.GetString("Id");
            TempData["Id"] = id;
            var model = cm.GetById(id);
            var product = db.Products.Where(x => x.ProductId == model.ProductId).FirstOrDefault();
            product.Stock = product.Stock - model.Quantity;
            Random random = new Random();

            var order = new Order
            {

                OrderNo = random.Next(),
                City = data.City,
                District = data.District,
                Address = data.Address,
                UserAdminId = model.UserAdminId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                ProductImage = model.ProductImage,
                ProductPrice = model.Price,
                Date = model.Date,
                ExpirationYear = data.ExpirationYear,
                ExpirationMonth = data.ExpirationMonth,
                CardNumber = data.CardNumber,
                CardName = data.CardName,
                Cvc = data.Cvc,
                OrderState = OrderState.Hazırlanıyor
            };

            var payment = PaymentProcesses(order);
            cm.Delete(model);


            om.Add(order);

            pm.Update(product);

            ViewBag.productid = model.ProductId;
            ViewBag.cartid = id;
            return View("OrderMessage");
        }

        private Payment PaymentProcesses(Order order)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-KiSGn7lbz30bvYavBZeUrv3soz2I0tqY";
            options.SecretKey = "sandbox-dotaJsyQSxAA91lXxaUGLYjNLdPPQp8o";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(1111, 9999).ToString();
            request.Price = order.ProductPrice.ToString();
            request.PaidPrice = order.ProductPrice.ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard2 = new PaymentCard();
            paymentCard2.CardHolderName = "John Doe";
            paymentCard2.CardNumber = "5528790000000008";
            paymentCard2.ExpireMonth = "12";
            paymentCard2.ExpireYear = "2030";
            paymentCard2.Cvc = "123";
            paymentCard2.RegisterCard = 0;
            request.PaymentCard = paymentCard2;


            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = order.CardName;
            paymentCard.CardNumber = order.CardNumber;
            paymentCard.ExpireMonth = order.ExpirationMonth;
            paymentCard.ExpireYear = order.ExpirationYear;
            paymentCard.Cvc = order.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = order.UserAdminId;
            buyer.Name = order.UserAdminId;
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = order.Address;
            buyer.Ip = "85.34.78.112";
            buyer.City = order.City;
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            var fullname = HttpContext.Session.GetString("FullName");
            var user = HttpContext.Session.GetString("Id");

            foreach (var item in cm.GetList(x => x.UserAdminId == Convert.ToString(user)))
            {

                basketItems.Add(new BasketItem()
                {
                    Id = item.ProductId.ToString(),
                    Name = item.Product.Name,
                    Category1 = item.Product.Category.CategoryId.ToString(),
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = item.Price.ToString()
                });
            }

            request.BasketItems = basketItems;

            return Payment.Create(request, options);
        }
    }
}
