using CourseWork.Helper;
using LaptopWebSite.Core;
using LaptopWebSite.Models;
using LaptopWebSite.Models.Entities;
using LaptopWebSite.ViewModels;
using Newtonsoft.Json;
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
            var listProductImage = _context.ProductImages.Where(t => t.ProductId == null);
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

            foreach (var item in listProductImage)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                  
                    string image = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ProductDescriptionPath"]) + item.FileName;
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }
                    _context.ProductImages.Remove(item);
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

                if (model.ProductImages.Count() != 0)
                {
                    for (int i = 0; i < model.ProductImages.Count(); i++)
                    {
                        var temp = model.ProductImages[i];
                        if (temp != null)
                        {
                            _context.ProductImages.FirstOrDefault(t => t.FileName == temp).ProductId = product.Id;
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
           
            var temp = _context.Products.FirstOrDefault(t => t.Id == id);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ContentResult UploadBase64(string base64image)
        {
            string filename = Guid.NewGuid().ToString() + ".jpg";
            string imageBig = Server.MapPath(Constants.ProductImagesPath) + filename;
            string json = null;
            try
            {
                // The Complete method commits the transaction. If an exception has been thrown,
                // Complete is not  called and the transaction is rolled back.
                Bitmap imgCropped = base64image.FromBase64StringToBitmap();
                var saveImage = ImageWorker.CreateImage(imgCropped, 300, 300);
                if (saveImage == null)
                    throw new Exception("Error save image");
                saveImage.Save(imageBig, ImageFormat.Jpeg);

                //var saveImageIcon = ImageWorker.CreateImage(imgCropped, 32, 32);
                //if (saveImageIcon == null)
                //    throw new Exception("Error save image");
                //saveImageIcon.Save(imageSmall, ImageFormat.Jpeg);

                var productImage = new ProductImage { FileName = filename };
                _context.ProductImages.Add(productImage);
                _context.SaveChanges();

                json = JsonConvert.SerializeObject(new
                {
                    imagePath = Url.Content(Constants.ProductImagesPath) + filename,
                    image = filename
                });

            }
            catch (Exception)
            {
                json = JsonConvert.SerializeObject(new
                {
                    image = filename
                });
                //if (System.IO.File.Exists(imageSmall))
                //{
                //    System.IO.File.Delete(imageSmall);
                //}
                if (System.IO.File.Exists(imageBig))
                {
                    System.IO.File.Delete(imageBig);
                }
            }
            return Content(json, "application/json");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ContentResult DeleteImageAjax(string id)
        {
            string json = JsonConvert.SerializeObject(new
            {
                success = false
            });
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ProductImage productImage = _context.ProductImages.FirstOrDefault(t=>t.FileName == id);
                    if (productImage != null)
                    {
                        string filename = productImage.FileName;
                        _context.ProductImages.Remove(productImage);
                        _context.SaveChanges();
                        string imageBig = Server.MapPath(Constants.ProductImagesPath) + filename;
                        
                        if (System.IO.File.Exists(imageBig))
                        {
                            System.IO.File.Delete(imageBig);
                        }
                        json = JsonConvert.SerializeObject(new
                        {
                            success = true
                        });
                    }
                   
                    scope.Complete();
                }
            }
            catch
            {
            }
            return Content(json, "application/json");
        }
    }
}