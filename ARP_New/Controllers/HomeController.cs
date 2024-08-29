using ARP_New.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ARP_New.Models.clsPlantDetailsdata;

namespace ARP_New.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string Zone,string ZoneName)
        {
            if (Session["Firstname"] != null && Session["Firstname"].ToString() != "")
            {
                ViewBag.search = Zone == null ? "" : Zone;
                ViewBag.ZoneName = ZoneName == null ? "" : ZoneName;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        public ActionResult GetData(clsPlantDetailsdata cls)
        {
            try
            {
                PlantDetailsdataOutput output = new PlantDetailsdataOutput();
                var data = cls.GetPlantList(cls);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in User Register");
            }
        }
        public ActionResult GetSideMenu(clsMenuList cls)
        {
            try
            {

                var data = cls.GeMenuList(cls);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw new Exception("Error in User Register");
            }
        }

        public ActionResult GetMonthData(clsPlantDetailsdata cls)
        {
            try
            {

                var data = cls.GetPlantListMonth(cls);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in User Register");
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public FileResult ExporttoExcel(clsPlantDetailsdata cls)
        {
            clsPlantDetailsdata sdb = new clsPlantDetailsdata();
            var data = sdb.GetPlantListMonth(cls);

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Monthly-ReportFile");

            // Setting up the headers
            worksheet.Range("A1:C1").Merge().Value = "AHMEDABAD MUNICIPAL CORPORATION";
            worksheet.Range("A1:C1").Style.Font.Bold = true;
            worksheet.Range("A1:C1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range("A1:C1").Style.Font.FontColor = XLColor.VenetianRed;
            worksheet.Cell(2, 1).Value = "Report Name :";
            //worksheet.Cell(2, 2).Value = "Monthly Report";
            worksheet.Range("B2:C2").Merge().Value = "Monthly Report";
            worksheet.Cell(3, 1).Value = "Location Name :";
            worksheet.Range("B3:C3").Merge().Value = cls.strName;
            //worksheet.Cell(3, 2).Value = cls.strName;
            worksheet.Cell(4, 1).Value = "Report Date :";
            worksheet.Range("B4:C4").Merge().Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
            //worksheet.Cell(4, 2).Value = cls.strDateandTime;
            worksheet.Cell(5, 1).Value = "Zone Name :";
            worksheet.Range("B5:C5").Merge().Value = cls.Zonename;
            // Column headers
            worksheet.Cell(6, 1).Value = "Date Time";
            worksheet.Cell(6, 2).Value = "Total (MLD)";
            worksheet.Cell(6, 3).Value = "Till Today QTY. (MLD)";

            // Adding the data rows
            var row = 7;
            foreach (var item in data)
            {
                worksheet.Cell(row, 1).Value = item.strDateandTime; // Replace with actual date time from item if available
                worksheet.Cell(row, 2).Value = item.decTotal; // Replace with actual value from item
                worksheet.Cell(row, 3).Value = item.decTillTodayQty; // Replace with actual value from item
                worksheet.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;



                row++;
            }

            // Applying styles
            worksheet.Range("A1:D1").Style.Font.Bold = true;
            worksheet.Range("A2:D4").Style.Font.Bold = true;
            worksheet.Range("A6:D6").Style.Font.Bold = true;
            worksheet.Range("A5:B5").Style.Font.Bold = true;

            worksheet.Columns().AdjustToContents();
            using (var stream = new System.IO.MemoryStream())
            {
                workbook.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Monthly-ReportFile.xlsx");
            }
        }


        public FileResult ExporttoExcelDaily(clsPlantDetailsdata cls)
        {
            clsPlantDetailsdata sdb = new clsPlantDetailsdata();
            var data = sdb.GetPlantList(cls);

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Daily-ReportFile");

            // Setting up the headers
            worksheet.Range("A1:E1").Merge().Value = "AHMEDABAD MUNICIPAL CORPORATION";
            worksheet.Range("A1:E1").Style.Font.Bold = true;
            worksheet.Range("A1:E1").Style.Font.FontColor = XLColor.VenetianRed;

            worksheet.Range("A1:C1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(2, 1).Value = "Report Name :";
            //worksheet.Cell(2, 2).Value = "Monthly Report";
            worksheet.Range("B2:E2").Merge().Value = "Daily Report";
            worksheet.Cell(3, 1).Value = "Location Name :";
            worksheet.Range("B3:E3").Merge().Value = cls.strName;
            //worksheet.Cell(3, 2).Value = cls.strName;
            worksheet.Cell(4, 1).Value = "Report Date :";
            worksheet.Range("B4:E4").Merge().Value = cls.strDateandTime;
            //worksheet.Cell(4, 2).Value = cls.strDateandTime;
            worksheet.Cell(5, 1).Value = "Zone Name :";
            worksheet.Range("B5:E5").Merge().Value = cls.Zonename;

            // Column headers                               
            worksheet.Cell(6, 1).Value = "Date Time";
            worksheet.Cell(6, 2).Value = "Level (Mtr.)";
            worksheet.Cell(6, 3).Value = "Flow (m3/Hr)";
            worksheet.Cell(6, 4).Value = "Total (MLD)";
            worksheet.Cell(6, 5).Value = "Till Today Total(ML)";

            // Adding the data rows
            var row = 7;
            foreach (var item in data)
            {
                worksheet.Cell(row, 1).Value = item.strDateandTime; // Replace with actual date time from item if available
                worksheet.Cell(row, 2).Value = item.decLevel; // Replace with actual value from item
                worksheet.Cell(row, 3).Value = item.decFlowRate; // Replace with actual value from item
                worksheet.Cell(row, 4).Value = item.decCurrentDayQty;
                worksheet.Cell(row, 5).Value = item.decTillTodayQty;
                worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                row++;
            }

            // Applying styles
            worksheet.Range("A1:D1").Style.Font.Bold = true;
            worksheet.Range("A2:D4").Style.Font.Bold = true;
            worksheet.Range("A6:E6").Style.Font.Bold = true;
            worksheet.Range("A5:B5").Style.Font.Bold = true;

            worksheet.Columns().AdjustToContents();
            using (var stream = new System.IO.MemoryStream())
            {
                workbook.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Monthly-ReportFile.xlsx");
            }
        }

    }
}