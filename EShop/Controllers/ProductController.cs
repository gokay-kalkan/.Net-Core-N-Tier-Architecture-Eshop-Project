using BusinessLayer.Concrete;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Controllers
{
    public class ProductController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        ImageManager im = new ImageManager(new EfImageDal());
        CommentManager cm = new CommentManager(new EfCommentDal());
        FavoriteManager fm = new FavoriteManager(new EfFavoriteDal());

        DataContext db = new DataContext();
        public IActionResult Details(int id)
        {
            var productdetail = pm.GetById(id);
            TempData["productid"] = id;
            var image = im.GetList(x => x.ProductId == id).Select(y => y.ImageName).FirstOrDefault();
            ViewBag.image = image;

            var commenttext = db.Comments.Where(x => x.ProductId == id).Select(x => x.CommentText).FirstOrDefault();
            ViewBag.commenttext = commenttext;

            var commentid = db.Comments.Where(x => x.ProductId == id).Select(x => x.CommentId).FirstOrDefault();
            ViewBag.commentid = commentid;
            TempData["CommentId"] = commentid;

            var comment = db.Comments.Where(x => x.ProductId == id).FirstOrDefault();
            ViewBag.commentfind = comment;

            var userid = db.Comments.Where(x => x.ProductId == id).Select(x => x.UserAdminId).FirstOrDefault();
            ViewBag.userid = userid;
            TempData["UserId"] = userid;

            var productrating = db.Comments.Where(x => x.ProductId == id).Select(x => x.RatingProduct).FirstOrDefault();
            ViewBag.productrating = productrating;
            var commentlist = cm.GetList(x => x.ProductId == id && x.Status == true);
            ViewBag.comment = commentlist;
            var productimage = im.GetList(x => x.Status == true && x.ProductId == id).OrderByDescending(x => x.ImageId);
            ViewBag.orderdescimage = productimage;


            var number = cm.GetList(x => x.ProductId == id).Count();
            var average = cm.GetList(x => x.ProductId == id).Select(x => x.RatingProduct).DefaultIfEmpty(0).Average();
            ViewBag.number = number;
            ViewBag.average = Math.Round(average);
            return View(productdetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Comment data)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (data != null)
                    {
                        var user = db.Comments.Where(x => x.UserAdminId == data.UserAdminId && x.ProductId == data.ProductId).FirstOrDefault();
                        var shop = db.Orders.Where(x => x.ProductId == Convert.ToInt32(TempData["productid"])).FirstOrDefault();
                        if (shop == null)
                        {

                            ViewBag.shop = "Bu ürüne yorum yapabilmek için öncelikle ürünü almalısınız";
                            return View("CommentUserInfo");

                        }
                        if (db.Comments.Contains(user))
                        {

                            ViewBag.info = "Bir ürüne sadece bir kere yorum yapabilirsiniz.";
                            return View("CommentUserInfo");
                        }
                        cm.Add(data);
                        return RedirectToAction("Details");
                    }
                }
                else
                {
                    ViewBag.error = ModelState.Values.FirstOrDefault(x => x.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).Errors[0].ErrorMessage;
                    ViewBag.info = "Boş Geçtiğiniz Alanlar Var";
                    return View("CommentInfo2");
                }

            }
            else
            {
                ViewBag.info = "Yorum yapabilmek için öncelikle sisteme giriş yapınız";


                return View("CommentInfo");
            }
            return View();



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProductFavoriteAdd(int id)
        {
            var products = pm.GetById(id);
            if (User.Identity.IsAuthenticated)
            {

                var user = HttpContext.Session.GetString("Id");

                Favorite favorite = new Favorite()
                {
                    ProductId = id,
                    UserAdminId = user
                };

                fm.Add(favorite);

                return RedirectToAction("FavoriteProducts", "User");
            }
            else
            {
                ViewBag.info = "Öncelikle Sisteme giriş yapınız.";
                return View("CommentInfo");
            }

        }

       
        public IActionResult FavoriteDelete(int id)
        {
            var products = fm.GetById(id);
            if (User.Identity.IsAuthenticated)
            {

     

                fm.Delete(products);

                return RedirectToAction("FavoriteProducts", "User");
            }
            else
            {
                ViewBag.info = "Öncelikle Sisteme giriş yapınız.";
                return View("CommentInfo");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CommentUpdate(Comment data)
        {

            var user = HttpContext.Session.GetString("Id");
            var comment = cm.GetById(Convert.ToInt32(TempData["CommentId"]));
            comment.CommentText = data.CommentText;
            comment.RatingProduct = data.RatingProduct;
            if (Convert.ToString(comment.UserAdminId) == Convert.ToString(user))
            {
                cm.Update(comment);
                ViewBag.update = "Yorumunuz Başarıyla Güncellendi";
                return View("ProductCommentInfo");
            }
            return View();
        }

        public IActionResult CommentDelete(int id)
        {

            var user = HttpContext.Session.GetString("Id");
            var comment = cm.GetById(Convert.ToInt32(TempData["CommentId"]));

            if (Convert.ToString(comment.UserAdminId) == Convert.ToString(user))
            {
                cm.CommentDelete(comment.CommentId);
                ViewBag.delete = "Yorumunuz Başarıyla Silinmiştir.";
                return View("ProductCommentInfo");
            }
            return View();
        }
        public IActionResult Search(string search)
        {
            var find = db.Products.Where(x => x.Name.Contains(search) && x.Category.Name.Contains(search) && x.Description.Contains(search)).ToList();


            var imagelist = im.GetList(x => x.Status == true);
            ViewBag.image = imagelist;

            var computerproducts = pm.GetList(x => x.Status == true && x.Category.Name == "Bilgisayar");
            ViewBag.computer = computerproducts;

            var computerimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Bilgisayar");
            ViewBag.computerimage = computerimage;

            var tabletproducts = pm.GetList(x => x.Status == true && x.Category.Name == "Tablet");
            ViewBag.tablet = tabletproducts;

            var tabletimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Tablet");
            ViewBag.tabletimage = tabletimage;

            var phoneproducts = pm.GetList(x => x.Status == true && x.Category.Name == "Telefon");
            ViewBag.phone = phoneproducts;

            var phoneimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Telefon");
            ViewBag.phoneimage = phoneimage;

            var tvproducts = pm.GetList(x => x.Status == true && x.Category.Name == "Televizyon");
            ViewBag.tv = tvproducts;

            var tvimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Televizyon");
            ViewBag.tvimage = tvimage;

            var camera = pm.GetList(x => x.Status == true && x.Category.Name == "Kamera");
            ViewBag.camera = camera;

            var cameraimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Kamera");
            ViewBag.cameraimage = cameraimage;
            return View(find);
        }
    }
}
