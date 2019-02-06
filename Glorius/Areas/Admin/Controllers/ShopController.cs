using Glorius.Models.Data;
using Glorius.Models.ViewModels.Shop;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Glorius.Areas.Admin.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        public ActionResult Sections()
        {
            List<SectionVM> SectionVMList;

            using (Db db = new Db())
            {
                SectionVMList = db.Sections
                    .ToArray()
                    .OrderBy(x => x.Sorting)
                    .Select(x => new SectionVM(x))
                    .ToList();

                ViewBag.Sections = new SelectList(db.Sections.ToList(), "Id", "Name");

            }

            return View(SectionVMList);
        }
        [HttpPost]
        public string AddNewSection(string secName)
        {
            string id;
            using (Db db = new Db())
            {
                if (db.Sections.Any(x => x.Name == secName))
                {
                    return "titletaken";
                }
                SectionDTO dto = new SectionDTO();

                dto.Name = secName;
                dto.Slug = secName.Replace(" ", "-").ToLower();
                dto.Sorting = 100;

                db.Sections.Add(dto);
                db.SaveChanges();

                id = dto.Id.ToString();

                return id;
            }
        }
        public ActionResult DeleteSection(int id)
        {
            using (Db db = new Db())
            {
                SectionDTO dto = db.Sections.Find(id);

                db.Sections.Remove(dto);
                db.SaveChanges();

                return RedirectToAction("Sections");
            }
        }
        [HttpPost]
        public string RenameSections(string newSecName, int id)
        {
            using (Db db = new Db())
            {
                if (db.Sections.Any(x => x.Name == newSecName))
                    return "Имя существует";
                var dto = db.Categories.Find(id);
                dto.Name = newSecName;
                dto.Slug = newSecName.Replace(" ", "-").ToLower();
                db.SaveChanges();
            }

            return "okay";
        }






        public ActionResult Categories()
        {
            List<CategoryVM> CategoryVMList;

            using (Db db = new Db())
            {
                CategoryVMList = db.Categories
                    .ToArray()
                    .OrderBy(x => x.Sorting)
                    .Select(x => new CategoryVM(x))
                    .ToList();

                ViewBag.Sections = new SelectList(db.Sections.ToList(), "Id", "Name");

            }

            return View(CategoryVMList);
        }
        [HttpPost]
        public string AddNewCategory(string catName, int secId)
        {
            string id;
            using (Db db = new Db())
            {
                string secName = db.Sections.Find(secId).Name;

                if (db.Categories.Any(x => x.Name == catName))
                {
                    return "titletaken";
                }
                CategoryDTO dto = new CategoryDTO();

                dto.Name = catName;
                dto.Slug = catName.Replace(" ", "-").ToLower();
                dto.Sorting = 100;
                dto.SectionId = secId;
                dto.SectionName = secName;

                db.Categories.Add(dto);
                db.SaveChanges();

                id = dto.Id.ToString();

                return id;
            }
        }
        public ActionResult DeleteCategory(int id)
        {
            using (Db db = new Db())
            {
                CategoryDTO dto = db.Categories.Find(id);

                db.Categories.Remove(dto);
                db.SaveChanges();

                return RedirectToAction("Categories");
            }
        }
        [HttpPost]
        public string RenameCategory(string newCatName, int id, int secId)
        { 
            using (Db db = new Db())
            {
                string secName = db.Sections.Find(secId).Name;

                if (db.Categories.Any(x => x.Name == newCatName) && db.Categories.Any(x => x.SectionId == secId))
                    return "Имя существует";

                var dto = db.Categories.Find(id);
                dto.Name = newCatName;
                dto.Slug = newCatName.Replace(" ", "-").ToLower();
                dto.SectionId = secId;
                dto.SectionName = secName;
                db.SaveChanges();
            }

            return "okay";
        }

        public ActionResult Materials()
        {
            List<MaterialVM> MaterialVMList;

            using (Db db = new Db())
            {
                MaterialVMList = db.Materials
                    .ToArray()
                    .OrderBy(x => x.Sorting)
                    .Select(x => new MaterialVM(x))
                    .ToList();
            }

            return View(MaterialVMList);
        }
        [HttpPost]
        public string AddNewMaterial(string matName)
        {
            string id;
            using (Db db = new Db())
            {
                if (db.Materials.Any(x => x.Name == matName))
                {
                    return "Имя существует";
                }
                MaterialDTO dto = new MaterialDTO();

                dto.Name = matName;
                dto.Slug = matName.Replace(" ", "-").ToLower();
                dto.Sorting = 100;

                db.Materials.Add(dto);
                db.SaveChanges();

                id = dto.Id.ToString();

                return id;
            }
        }

        public ActionResult DeleteMaterial(int id)
        {
            using (Db db = new Db())
            {
                MaterialDTO dto = db.Materials.Find(id);

                db.Materials.Remove(dto);
                db.SaveChanges();

                return RedirectToAction("Materials");
            }
        }

        public string RenameMaterial(string newMatName, int id)
        {
            using (Db db = new Db())
            {
                if (db.Materials.Any(c => c.Name == newMatName))
                    return "Имя существует";

                MaterialDTO dto = db.Materials.Find(id);

                dto.Name = newMatName;
                dto.Slug = newMatName.Replace(" ", "-").ToLower();

                db.SaveChanges();
            }

            return "ok";
        }
        [HttpGet]
        public ActionResult AddProduct(int? secId)
        {
            ProductVM model = new ProductVM();
            List<CategoryVM> categoriesList;

            using (Db db = new Db())
            {
                categoriesList = db.Categories.ToArray()
                   .Where(x => (secId == null || secId == 0 || x.SectionId == secId))
                   .Select(x => new CategoryVM(x))
                   .ToList();

                model.Categories = new SelectList(categoriesList, "Id", "Name");
                model.Materials = new SelectList(db.Materials.ToList(), "Id", "Name");
                model.Sections = new SelectList(db.Sections.ToList(), "Id", "Name");

                ViewBag.SelectedSec = secId.ToString();
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddProduct(ProductVM model, HttpPostedFileBase file = null)
        {
            if (!ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                    model.Materials = new SelectList(db.Materials.ToList(), "Id", "Name");

                    return View(model);
                }
            }

            if (file != null && file.ContentLength > 0)
            {
                string ext = file.ContentType.ToLower();

                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/x-png" &&
                    ext != "image/png")
                {
                    using (Db db = new Db())
                    {
                        model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                        model.Materials = new SelectList(db.Materials.ToList(), "Id", "Name");

                        ModelState.AddModelError("", "Формат файла неподдерживается. Доступные форматы: jpg,jpeg,pjpeg,gif,x-png,png");
                        return View(model);
                    }
                }
            }

            using (Db db = new Db())
            {
                if (db.Products.Any(x => x.Name == model.Name))
                {
                    model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                    model.Materials = new SelectList(db.Materials.ToList(), "Id", "Name");

                    ModelState.AddModelError("", "Имя уже существует!");
                    return View(model);
                }
            }

            int id;

            using (Db db = new Db())
            {
                ProductDTO dto = new ProductDTO();

                dto.Name = model.Name;
                dto.Slug = model.Name.Replace(" ", "-").ToLower();
                dto.Description = model.Description;
                dto.Price = model.Price;
                dto.Discount = model.Discount;
                dto.ProductCode = "1";
                dto.CategoryId = model.CategoryId;
                dto.MaterialId = model.MaterialId;
                dto.SectionId = model.SectionId;
                dto.SectionName = model.SectionName;

                if (file != null)
                {
                    dto.ImgType = file.ContentType;
                    dto.Img = new byte[file.ContentLength];
                    file.InputStream.Read(dto.Img, 0, file.ContentLength);
                }

                CategoryDTO catDTo = db.Categories.FirstOrDefault(x => x.Id == model.CategoryId);
                dto.CategoryName = catDTo.Name;

                MaterialDTO matDTO = db.Materials.FirstOrDefault(x => x.Id == model.MaterialId);
                dto.MaterialName = matDTO.Name;

                SectionDTO secDTO = db.Sections.FirstOrDefault(x => x.Id == model.SectionId);
                dto.SectionName = secDTO.Name;

                db.Products.Add(dto);
                db.SaveChanges();

                id = db.Products.Max(i => i.Id);
                string prodCode = model.SectionId.ToString("00") + model.CategoryId.ToString("00") + id.ToString("0000");
                ProductDTO dto_ = db.Products.Find(id);

                dto_.ProductCode = prodCode;
                db.SaveChanges();
            }

            TempData["SM"] = "Продукт добавлен!";

            return RedirectToAction("AddProduct");
        }

        public ActionResult Products(int? page, int? catId, int? matId)
        {
            List<ProductVM> productsList;

            var pageNumber = page ?? 1;

            using (Db db = new Db())
            {
                productsList = db.Products.ToArray()
                    .Where(x => (catId == null || catId == 0 || x.CategoryId == catId) && (matId == null || matId == 0 || x.MaterialId == matId))
                    .Select(x => new ProductVM(x))
                    .ToList();

                ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                ViewBag.Materials = new SelectList(db.Materials.ToList(), "Id", "Name");

                ViewBag.SelectedCat = catId.ToString();
                ViewBag.SelectedMat = matId.ToString();
            }
            var onePageOfProducts = productsList.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfProducts = onePageOfProducts;

            return View(productsList);
        }
        [HttpGet]
        public ActionResult EditProduct(int id, int? secId)
        {
            List<CategoryVM> categoriesList;

            ProductVM model;

            using (Db db = new Db())
            {
                ProductDTO dto = db.Products.Find(id);

                if (dto == null)
                {
                    return Content("Продукт не найден!");
                }
                categoriesList = db.Categories.ToArray()
                   .Where(x => (secId == null || secId == 0 || x.SectionId == secId))
                   .Select(x => new CategoryVM(x))
                   .ToList();

                model = new ProductVM(dto);

                model.Categories = new SelectList(categoriesList, "Id", "Name");
                model.Materials = new SelectList(db.Materials.ToList(), "Id", "Name");
                model.Sections = new SelectList(db.Sections.ToList(), "Id", "Name");

                ViewBag.SelectedSec = dto.SectionId.ToString();
            }
            //GALLRY///////////////////////////////////////
            List<GalleryVM> gallery;
            List<int> ids = new List<int>();
            using (Db db = new Db())
            {
                gallery = db.ProductGallery
                    .ToArray()
                    .Where(x => x.ProductId == id)
                    .Select(x => new GalleryVM(x))
                    .ToList();
            }
            foreach (var item in gallery)
            {
                ids.Add(item.Id);
            }
            ViewBag.Ids = ids;

            return View(model);
        }
        [HttpPost]
        public ActionResult EditProduct(ProductVM model, HttpPostedFileBase file = null)
        {
            int id = model.Id;

            using (Db db = new Db())
            {
                model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                model.Materials = new SelectList(db.Materials.ToList(), "Id", "Name");
            }

            if (file != null && file.ContentLength > 0)
            {
                string ext = file.ContentType.ToLower();

                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/x-png" &&
                    ext != "image/png")
                {
                    using (Db db = new Db())
                    {
                        ModelState.AddModelError("", "Формат файла неподдерживается. Доступные форматы: jpg,jpeg,pjpeg,gif,x-png,png");
                        return View(model);
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Products.Where(x => x.Id != id).Any(x => x.Name == model.Name))
                {
                    ModelState.AddModelError("", "Имя уже существует");
                }
            }
            

            using (Db db = new Db())
            {
                
                ProductDTO dto = db.Products.Find(id);

                dto.Name = model.Name;
                dto.Slug = model.Name.Replace(" ", "-").ToLower();
                dto.Description = model.Description;
                dto.Price = model.Price;
                dto.Discount = model.Discount;
                dto.CategoryId = model.CategoryId;
                dto.MaterialId = model.MaterialId;
                dto.SectionId = model.SectionId;
                dto.SectionName = model.SectionName;
                if (file != null)
                {
                    dto.ImgType = file.ContentType;
                    dto.Img = new byte[file.ContentLength];
                    file.InputStream.Read(dto.Img, 0, file.ContentLength);
                }                             
                CategoryDTO catDTo = db.Categories.FirstOrDefault(x => x.Id == model.CategoryId);
                dto.CategoryName = catDTo.Name;

                MaterialDTO matDTO = db.Materials.FirstOrDefault(x => x.Id == model.MaterialId);
                dto.MaterialName = matDTO.Name;

                SectionDTO secDTO = db.Sections.FirstOrDefault(x => x.Id == model.SectionId);
                dto.SectionName = secDTO.Name;

                db.SaveChanges();

                id = db.Products.Max(i => i.Id);
                string prodCode = model.SectionId.ToString("00") + model.CategoryId.ToString("00") + id.ToString("0000");
                ProductDTO dto_ = db.Products.Find(id);

                dto_.ProductCode = prodCode;
                db.SaveChanges();
            }
            TempData["SM"] = "Продукт изменен!";

            return RedirectToAction("Products");
        }

        public ActionResult DeleteProduct(int id)
        {
            using (Db db = new Db())
            {
                ProductDTO dto = db.Products.Find(id);

                db.Products.Remove(dto);
                db.SaveChanges();
            }

            TempData["SM"] = "Продукт удален!";

            return RedirectToAction("Products");
        }

        [HttpPost]
        public void SaveGallaryImages(int id) //не работает при создании товара т.к. у товора до создания нету ИД
        {
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];

                if (file != null && file.ContentLength > 0)
                {
                    using (Db db = new Db())
                    {
                        GalleryDTO dto = new GalleryDTO();
                        dto.ProductId = id;
                        dto.ImgType = file.ContentType;
                        dto.Img = new byte[file.ContentLength];
                        file.InputStream.Read(dto.Img, 0, file.ContentLength);

                        db.ProductGallery.Add(dto);
                        db.SaveChanges();
                    }
                }
            }
        }



        //for preview image
        public FileContentResult GetPreview(int id)
        {
            using (Db db = new Db())
            {
                ProductDTO img = db.Products.Find(id);

                if (img != null)
                {
                    return File(img.Img, img.ImgType);
                }
                else
                {
                    return null;
                }
            }
        }

        //for gallery image
        public FileContentResult GetGallery(int id)
        {
            using (Db db = new Db())
            {
                GalleryDTO img = db.ProductGallery.Find(id);

                if (img != null)
                {
                    return File(img.Img, img.ImgType);
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpPost]
        public void DeleteImg(int id)
        {
            using (Db db = new Db())
            {
                GalleryDTO dto = db.ProductGallery.Find(id);
                db.ProductGallery.Remove(dto);
                db.SaveChanges();
            }
        }
    }
}