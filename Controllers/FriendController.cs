using DapperBasicCrud.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperBasicCrud.Controllers
{
    public class FriendController : Controller
    {
        private FriendsRepository repository = new FriendsRepository();
        private RequestModel request;
        private object excelSheet;

        // GET: Friends
        public ActionResult Index(RequestModel request, int? i)
        {
            
          
            if (request.OrderBy == null)
            {
                request = new RequestModel
                {
                   Search = request.Search,
                    OrderBy = "name",
                    IsDescending = false
                };
            }
             int PageNumber = 1;
           int PageSize = 5;
            ViewBag.Request = request;
            return View(repository.GetAll(request).ToList().ToPagedList(i ?? PageNumber, PageSize));
        }

        private List<Friend> GetFriends()
        {
            var friends = new List<Friend>();
            friends.Add(new Friend() { City = "Pune" });
            friends.Add(new Friend() { City = "Panji" });
            friends.Add(new Friend() { City = "Jammu" });
            friends.Add(new Friend() { City = "Leh" });

            return friends;
        }

        // GET: Friends/Details/5
        public ActionResult Details(int id)
        {
            return View(repository.Get(id));
        }

        // GET: Friends/Create
        [HttpGet]
        public ActionResult Create()
        {
            var list = new List<string>(){ "Leh","Jammu","Gulmarg" };
            ViewBag.list = list;
            return View();
        }
                       

        // POST: Friends/Create
        [HttpPost]
        public ActionResult Create(Friend friend, bool editAfterSaving = false)
        {
            if (ModelState.IsValid)
            {
                var lastInsertedId = repository.Create(friend);
                if (lastInsertedId > 0)
                {
                    TempData["Message"] = "Record added successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to add record";
                }
                if (editAfterSaving)
                {
                    return RedirectToAction("Edit", new { Id = lastInsertedId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        
        // GET: Friends/Edit/5
        public ActionResult Edit(int id)
        {
            var list = new List<string>() { "Leh", "Jammu", "Gulmarg" };
            ViewBag.list = list;
            return View();
            return View(repository.Get(id));
        }

        // POST: Friends/Edit/5
        [HttpPost]
        public ActionResult Edit(Friend friend)
        {
            if (ModelState.IsValid)
            {
                var recordAffected = repository.Update(friend);
                if (recordAffected > 0)
                {
                    TempData["Message"] = "Record updated successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to update record";
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Friends/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repository.Get(id));
        }

        // POST: Friends/Delete/5
        [HttpPost]
        public ActionResult Delete(Friend friend)
        {
            var recordAffected = repository.Delete(friend.Id);
            if (recordAffected > 0)
            {
                TempData["Message"] = "Record deleted successfully";
            }
            else
            {
                TempData["Error"] = "Failed to delete record";
            }
            return RedirectToAction("Index");

        }

        public ActionResult ExportToExcel()
        {

            FriendsRepository repository = new FriendsRepository();
            var model = repository.GetAll(request);

            HSSFWorkbook templateWorkbook = new HSSFWorkbook();
            HSSFSheet sheet =(HSSFSheet) templateWorkbook.CreateSheet("Index");
            List<Friend> _friend = model.ToList();
            HSSFRow dataRow =(HSSFRow) sheet.CreateRow(0);
            HSSFCellStyle style =(HSSFCellStyle) templateWorkbook.CreateCellStyle();

            HSSFFont font =(HSSFFont) templateWorkbook.CreateFont();
            font.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
            HSSFCell cell;
            style.SetFont(font);

     
            cell = (HSSFCell)dataRow.CreateCell(0);
            cell.CellStyle = style;
            cell.SetCellValue("No.");

            cell =(HSSFCell) dataRow.CreateCell(1);
            cell.CellStyle = style;
            cell.SetCellValue("Name");
            cell =(HSSFCell) dataRow.CreateCell(2);
            cell.CellStyle = style;
            cell.SetCellValue("City");
            cell =(HSSFCell) dataRow.CreateCell(3);
            cell.CellStyle = style;
            cell.SetCellValue("PhoneNumber");

            for (int i = 0; i < _friend.Count; i++)
            {
                dataRow = (HSSFRow) sheet.CreateRow(i + 1);
                dataRow.CreateCell(0).SetCellValue(i + 1);
                dataRow.CreateCell(1).SetCellValue(_friend[i].Name);
                dataRow.CreateCell(2).SetCellValue(_friend[i].City);
                dataRow.CreateCell(3).SetCellValue(_friend[i].PhoneNumber);
            }
            MemoryStream ms = new MemoryStream();
            templateWorkbook.Write(ms);
            return File(ms.ToArray(), "application/vnd.ms-excel", "Friends.xls");
            ////ExcelPackage pck = new ExcelPackage();
            ////ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            //ws.Cells["A6"].Value = "Id";
            //ws.Cells["B6"].Value = "Name";
            //ws.Cells["C6"].Value = "City";
            //ws.Cells["D6"].Value = "PhoneNumber";

            //int rowStart = 7;
            //foreach (var item in model)
            //{

            //    ws.Cells[string.Format("A{0}", rowStart)].Value = item.Id;
            //    ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
            //    ws.Cells[string.Format("C{0}", rowStart)].Value = item.City;
            //    ws.Cells[string.Format("D{0}", rowStart)].Value = item.PhoneNumber;

            //    rowStart++;
            //}

            //ws.Cells["A:AZ"].AutoFitColumns();
            //Response.Clear();
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            //Response.BinaryWrite(pck.GetAsByteArray());
            //Response.End();
        }
    }
}