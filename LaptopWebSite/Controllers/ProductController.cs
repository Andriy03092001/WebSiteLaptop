using CourseWork.Helper;
using LaptopWebSite.Core;
using LaptopWebSite.Models;
using LaptopWebSite.Models.Entities;
using LaptopWebSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace LaptopWebSite.Controllers
{
    public class ProductController : Controller
    {
        public readonly ApplicationDbContext _context;
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            List<ProductItemViemModel> list = new List<ProductItemViemModel>();
            var temp = _context.Products.ToList();
            foreach (var item in temp)
            {
                ProductItemViemModel product = new ProductItemViemModel
                {
                    Count = item.Count,
                    Id = item.Id,
                    IsAvailable = item.IsAvailable,
                    Name = item.Name,
                    Price = item.Price
                };
                list.Add(product);
            }

            temp.Clear();
            ListProductViewModel model = new ListProductViewModel()
            {
                listProduct = list
            };

            return View(model);
        }

        public void ClearImage()
        {
            var listImage = _context.ProductDescriptionImages.Where(t => t.ProductId == null);
            foreach (var item in listImage)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //Server.MapPath(
                    string image = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ProductDescriptionPath"]) + item.Name;
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }
                    _context.ProductDescriptionImages.Remove(item);
                    scope.Complete();
                }
            }

            _context.SaveChanges();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ProductAddViewModel model)
        {

            if (model.Count == 0 || model.Price == 0 || model.Name == null || model.Description == null)
            {
                ModelState.AddModelError("", "Invalid enter data.");
            }
            else
            {
                Product product = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Count = model.Count,
                    IsAvailable = model.IsAvailable,
                };
                _context.Products.Add(product);

                if (model.DescriptionImages.Count() != 0)
                {
                    for (int i = 0; i < model.DescriptionImages.Count(); i++)
                    {
                        var temp = model.DescriptionImages[i];
                        if (temp != null)
                        {
                            _context.ProductDescriptionImages.FirstOrDefault(t => t.Name == temp).ProductId = product.Id;
                        }

                    }
                }



                _context.SaveChanges();
            }
            return RedirectPermanent("/Product/Index");
        }

        [HttpPost]
        public JsonResult UploadImageDescription(HttpPostedFileBase file)
        {
            //string pathServer = ConfigurationManager.AppSettings["UserImagePath"];
            //string path = Server.MapPath(pathServer);

            //var image = Guid.NewGuid().ToString() + ".jpg";
            //string savepath = path + image;
            //Bitmap imageBig = ImageWorker.CreateImage(file, 1100, 1200);
            //imageBig.Save(savepath, ImageFormat.Jpeg);

            string link = string.Empty;
            var filename = Guid.NewGuid().ToString() + ".jpg";
            string image = Server.MapPath(Constants.ProductDescriptionPath) + filename;
            try
            {
                using (Bitmap btn = new Bitmap(file.InputStream))
                {
                    var saveImage = ImageWorker.CreateImage(btn, 450, 450);
                    if (saveImage != null)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            var pdImage = new ProductDescriptionImage
                            {
                                Name = filename
                            };
                            _context.ProductDescriptionImages.Add(pdImage);
                            _context.SaveChanges();
                            saveImage.Save(image, ImageFormat.Jpeg);
                            link = Url.Content(Constants.ProductDescriptionPath) + filename;
                            scope.Complete();
                        }

                    }
                }


            }
            catch
            {
                if (System.IO.File.Exists(image))
                {
                    System.IO.File.Delete(image);
                }
                link = string.Empty;
            }

            return Json(new { link, filename });
        }


        [HttpPost]
        public JsonResult DeleteImageDecription(string src)
        {
            string link = string.Empty;
            string filename = Path.GetFileName(src);
            string image = Server.MapPath(Constants.ProductDescriptionPath) +
                filename;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var pdImage = _context
                        .ProductDescriptionImages
                        .SingleOrDefault(p => p.Name == filename);
                    if (pdImage != null)
                    {
                        _context.ProductDescriptionImages.Remove(pdImage);
                        _context.SaveChanges();
                    }
                    //throw new Exception("Галяк");
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }
                    scope.Complete();
                }
            }
            catch
            {
                filename = string.Empty;
            }

            return Json(new { filename });
        }


        public ActionResult Product(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var temp = context.Products.FirstOrDefault(t => t.Id == id);
            ProductViemModel model = new ProductViemModel()
            {
                Id = temp.Id,
                Count = temp.Count,
                IsAvailable = temp.IsAvailable,
                Name = temp.Name,
                Price = temp.Price,
                Description = temp.Description
            };
            return View(model);
        }


        public ActionResult Delete()
        {
            List<ProductDeleteViewModel> list = new List<ProductDeleteViewModel>();
            var temp = _context.Products.ToList();
            foreach (var item in temp)
            {
                ProductDeleteViewModel product = new ProductDeleteViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                };
                list.Add(product);
            }
            temp.Clear();
            ListProductDeleteViewModel model = new ListProductDeleteViewModel()
            {
                listProduct = list
            };
            return View(model);
        }

        //якшо не робить то зробить обэкт 
        public void DeleteProduct(DeleteViewModel model)
        {
            string path = Server.MapPath(Constants.ProductDescriptionPath);
            //Delete in DB
            var product = _context.Products.FirstOrDefault(t => t.Id == model.Id);
            _context.Products.Remove(product);

            var listImageProductDescription = _context.ProductDescriptionImages.Where(t => t.ProductId == model.Id);
            foreach (var item in listImageProductDescription)
            {
                _context.ProductDescriptionImages.Remove(item);
                //Delete image in Server
                System.IO.File.Delete(path + item.Name);
            }
            _context.SaveChanges();

            return;
        }



    }
}