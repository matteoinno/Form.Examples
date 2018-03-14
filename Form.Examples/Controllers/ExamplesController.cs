using Form.Examples.Models;
using Form.Examples.Utils;
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
            //The entire ModelState is checked by data annotations. Check the class ExampleThreeViewModel for the data annotations.
            if (ModelState.IsValid)
            {
                //Import the file in a folder inside the solution.
                model.ImportFile();
                return View("Completed");
            }
            else
                return View(model);
        }

        public ActionResult ExampleThreeB()
        {
            return View();
        }

        [HttpPost]
        //The FormCollection is used to differ the get and post call.
        public ActionResult ExampleThreeB(FormCollection collection)
        {
            //The Request object contains data about the post. It contains also the list of the file passed to the controller.
            foreach (string ind in Request.Files)
            {
                //Save the current file in a HttpPostedFileBase.
                HttpPostedFileBase hpf = Request.Files[ind] as HttpPostedFileBase;
                //if the content lenght of the file is zero means either the file is empty or no file has been chosen.
                if (hpf.ContentLength == 0)
                    //return to the View
                    return View();
                //Upload the file as in the ExampleThreeViewModel ImportFile() method.
                Tools.UploadFile(hpf);
            }
            return View("Completed");

        }

        #endregion

        #region EXAMPLE FOUR

        public ActionResult ExampleFour()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExampleFour(ExampleFourViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View("Completed");
            }
            else
                return View(model);
        }

        #endregion

        #region EXAMPLE FIVE

        public ActionResult ExampleFive()
        {
            ExampleFiveViewModel model = new ExampleFiveViewModel();
            model.Init();
            return View(model);
        }

        [HttpPost]
        public ActionResult ExampleFive(ExampleFiveViewModel model)
        {
            return PartialView("_ExampleFiveResult", model);
        }

        #endregion


        //View for completed operation.
        public ActionResult Completed()
        {
            return View();
        }
    }
}