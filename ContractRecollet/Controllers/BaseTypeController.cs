using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using Domain.Management;
using Domain.Entidades;
using System.Net;


namespace ContractRecollet.Controllers
{
    public class BaseTypeController : Controller
    {

        private Entities db = new Entities();

        // GET: BaseType
        public ActionResult Index()
        {
             return View(gBaseType.getListItems());
        }


       
        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="NameBaseType")] BaseType baseType)
        {

            if (ModelState.IsValid)
            {
                if (!gBaseType.Save(baseType)) return HttpNotFound();

                return RedirectToAction("Index");
               
            }
           

            return View("Create",baseType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createTemplate(BaseFieldsTemplate template)
        {

            if (String.IsNullOrEmpty(template.DefaultValue)) template.DefaultValue = "0";


            if (ModelState.IsValid)
            {

                 if (!Template.save(template)) return HttpNotFound();

            }


            // return View();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult createTemplate(Template template)
        //{

        //    if (ModelState.IsValid)
        //    {

        //       // if (!Template.save(baseField)) return HttpNotFound();

        //    }


        //   // return View();
        //    return RedirectToAction("Index");
        //}


        public ActionResult Details(long id)
        {

            BaseType baseType = gBaseType.getElemntById(id);

            if (baseType == null)
            {
                return HttpNotFound();
            }


            return View(baseType);
        }


        public ActionResult addFields(long id)
        {

            BaseType baseType = gBaseType.getElemntById(id);
            Template template = new Template(baseType, new BaseFieldsTemplate());
            ViewBag.idBaseField = new SelectList(db.BaseFieldTypes, "IdBaseFieldTypes", "NameField");

            if (baseType == null)
            {
                return HttpNotFound();
            }


            return View(template);
        }
    }
}