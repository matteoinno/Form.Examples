using Form.Examples.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Form.Examples.Controllers
{
    public class ExamplesController : Controller
    {
        #region EXAMPLE ONE
        // Example One - No Validation Summary
        public ActionResult ExampleOne()
        {
            return View();
        }

        // Example One - Validation Summary        
        public ActionResult ExampleOneB()
        {
            return View();
        }
        
        // Example One - highlight input fields Rendered
        public ActionResult ExampleOneC()
        {
            return View();
        }
        
        // Example One - highlight input fields Javascript
        public ActionResult ExampleOneD()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ExampleOne(ExampleOneViewModel model)
        {
            //The entire ModelState is checked by data annotations. Check the class ExampleOneViewModel for the data annotations.
            if (ModelState.IsValid)
            {
                model.Insert();
                return View("Completed");
            }
            else
            {
                //The Request.UrlReferrer object contains the info about the previous page visited.
                //The Segments contains the segments of the previous page visited, and the last one contains the view that submitted the form.
                //Retrieve the last segment and use it to return to the correct view.
                string view = Request.UrlReferrer.Segments.Last();
                return View(view, model);
            }
        }
        #endregion

        #region EXAMPLE TWO

        public ActionResult ExampleTwo()
        {
            return View();
        }

        public ActionResult ExampleTwoForm()
        {
            return View("_ExampleTwoForm");
        }

        [HttpPost]
        public ActionResult ExampleTwo(ExampleTwoViewModel model)
        {
            try
            {
                //The entire ModelState is checked by data annotations. Check the class ExampleTwoViewModel for the data annotations.
                if (ModelState.IsValid)
                {
                    return Json("OK");
                }
                else
                {
                    return View("_ExampleTwoForm", model);
                }
            }
            catch
            {
                return Json("KO");
            }
        }

        #endregion

        #region EXAMPLE THREE

        public ActionResult ExampleThree()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExampleThree(ExampleThreeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImportFile();
                return View("Completed");
            }
            else
                return View(model);
        }
        #endregion

        //View for completed operation.
        public ActionResult Completed()
        {
            return View();
        }
    }
}