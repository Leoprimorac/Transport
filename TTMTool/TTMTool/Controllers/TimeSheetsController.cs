using Aspose.Pdf;
using Aspose.Pdf.Text;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TTMTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetsController : ControllerBase
    {

        //When User edits some time sheet or add new time sheet for current day

        [HttpPost("/api/editTimeSheetByUser")]
        [Produces("application/json")]
        public ActionResult EditTimeSheetByUser(TimeSheetModel model)
        {
            try
            {
                if (model.endTime != "-1" && model.startTime != "-1")
                {
                    DateTime StartTime = Convert.ToDateTime(model.startTime);
                    DateTime EndTime = Convert.ToDateTime(model.endTime);
                    var WorkTime = EndTime.Subtract(StartTime);
                    var conv = WorkTime.ToString(@"h\:mm");
                    model.workTime = conv;
                }
                using (var con = db.Gdb())
                {

                    SqlCommand cmd = new SqlCommand("dbo.spEditTimeSheetByUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", model.id);
                    cmd.Parameters.AddWithValue("@type", model.type);
                    cmd.Parameters.AddWithValue("@start_time", model.startTime);
                    cmd.Parameters.AddWithValue("@end_time", model.endTime);
                    cmd.Parameters.AddWithValue("@breaks", model.breaks);
                    cmd.Parameters.AddWithValue("@work_time", model.workTime);
                    cmd.Parameters.AddWithValue("@m3", model.m3);
                    cmd.Parameters.AddWithValue("@km_stand", model.kmStand);
                    cmd.Parameters.AddWithValue("@privat", model.privat);
                    cmd.Parameters.AddWithValue("@fuel", model.fuel);
                    cmd.Parameters.AddWithValue("@adblue", model.adblue);
                    cmd.Parameters.AddWithValue("@notes", model.notes);
                    cmd.Parameters.AddWithValue("@employeeId", model.UserId);
                    cmd.Parameters.AddWithValue("@entryDate", model.entryDate);
                    cmd.Parameters.AddWithValue("@timeSheetKey", model.timeSheetKey);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Ok();
        }

        [HttpPost("/api/createTimeSheet")]
        [Produces("application/json")]
        //After new User creation this method create time sheet, after employee aproval it creates rest of time sheets
        public static void CreateTimeSheet(EmployeeModel user)
        {


            var arr = 0;
            if (user.level == "User")
            {


                using (var con = db.Gdb())
                {
                    SqlCommand cmdd = new SqlCommand($"SELECT * FROM TimeSheets WHERE employeeId = '{user.id}'", con);
                    con.Open();
                    SqlDataReader rd = cmdd.ExecuteReader();

                    TimeSheetModel TS = new TimeSheetModel();
                    List<TimeSheetModel> TSList = new List<TimeSheetModel>();

                    string month = null;
                    string year = null;
                    string TSKey = null;

                    while (rd.Read())
                    {
                        TS = new TimeSheetModel();
                        TS.id = Convert.ToInt32(rd["id"]);
                        TS.timeSheetKey = Convert.ToString(rd["timeSheetKey"]);
                        TS.type = Convert.ToString(rd["type"]);
                        TS.startTime = Convert.ToString(rd["start_time"]);
                        TS.endTime = Convert.ToString(rd["end_time"]);
                        TS.breaks = Convert.ToString(rd["breaks"]);
                        TS.workTime = Convert.ToString(rd["work_time"]);
                        TS.notes = Convert.ToString(rd["notes"]);
                        TS.entryDate = Convert.ToDateTime(rd["entryDate"]);
                        TS.timeSheetStatus = Convert.ToString(rd["timeSheetStatus"]);
                        TS.m3 = Convert.ToInt32(rd["m3"]);
                        TS.kmStand = Convert.ToInt32(rd["km_stand"]);
                        TS.privat = Convert.ToInt32(rd["privat"]);
                        TS.fuel = Convert.ToInt32(rd["fuel"]);
                        TS.adblue = Convert.ToInt32(rd["adblue"]);

                        TS.UserId = Convert.ToInt32(rd["employeeId"]);

                        TSList.Add(TS);
                    }
                    con.Close();



                    if (TSList.Count == 0)
                    {
                        month = DateTime.Now.Month.ToString();
                        year = DateTime.Now.Year.ToString();
                        TSKey = user.userName.ToString() + "-" + month + "-" + year;
                    }
                    else
                    {

                        var lastDateEntry = TSList[TSList.Count - 1].entryDate;
                        var nextMonth = lastDateEntry.AddMonths(1);

                        month = Convert.ToString(nextMonth.Month);
                        year = Convert.ToString(nextMonth.Year);
                        TSKey = user.userName.ToString() + "-" + month + "-" + year;
                    }
                    if (month == "2")
                    {
                        for (int i = 0; i < 28; i++)
                        {
                            string date = (i + 1) + "." + month + "." + year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                            SqlCommand cmd = new SqlCommand("dbo.spCreateTimeSheetsByEmpId", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", user.id);
                            cmd.Parameters.AddWithValue("@timeSheetKey", TSKey);
                            cmd.Parameters.AddWithValue("@entryDate", Convert.ToDateTime(date));
                            cmd.Parameters.AddWithValue("@timeSheetStatus", "working progress");
                            con.Open();
                            arr = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else if ((month == "1") || (month == "3") || (month == "5") || (month == "7")
                       || (month == "8") || (month == "10") || (month == "12"))
                    {
                        for (int i = 0; i < 31; i++)
                        {
                            string date = (i + 1) + "." + month + "." + year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                            SqlCommand cmd = new SqlCommand("dbo.spCreateTimeSheetsByEmpId", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", user.id);
                            cmd.Parameters.AddWithValue("@timeSheetKey", TSKey);
                            cmd.Parameters.AddWithValue("@entryDate", Convert.ToDateTime(date));
                            cmd.Parameters.AddWithValue("@timeSheetStatus", "working progress");
                            con.Open();
                            arr = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            string date = (i + 1) + "." + month + "." + year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                            SqlCommand cmd = new SqlCommand("dbo.spCreateTimeSheetsByEmpId", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", user.id);
                            cmd.Parameters.AddWithValue("@timeSheetKey", TSKey);
                            cmd.Parameters.AddWithValue("@entryDate", Convert.ToDateTime(date));
                            cmd.Parameters.AddWithValue("@timeSheetStatus", "working progress");
                            con.Open();
                            arr = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    con.Close();



                }
            }
        }

        //Return time sheet for specific user for one month

        [HttpPost("/api/getTimeSheetByIdDate")]
        [Produces("application/json")]
        public List<TimeSheetModel> GetTimeSheetsByEmployeeIdAndDate(GetTimeSheetModel data)
        {
            string StartDate = "01." + data.month + "." + data.year;
            string EndDate = "";
            if (data.month == "2")
            {
                EndDate = "28." + data.month + "." + data.year;
            }
            else if ((data.month == "1") || (data.month == "3") || (data.month == "5") || (data.month == "7")
               || (data.month == "8") || (data.month == "10") || (data.month == "12"))
            {
                EndDate = "31." + data.month + "." + data.year;
            }
            else
            {
                EndDate = "30." + data.month + "." + data.year;
            }


            TimeSheetModel TS = null;
            List<TimeSheetModel> timeSheets = null;
            try
            {
                using (var con = db.Gdb())
                {
                    timeSheets = new List<TimeSheetModel>();
                    SqlCommand cmd = new SqlCommand("dbo.spGetTimeSheetByIdDate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", data.id);
                    cmd.Parameters.AddWithValue("@monthStart", Convert.ToDateTime(StartDate));
                    cmd.Parameters.AddWithValue("@monthEnd", Convert.ToDateTime(EndDate));
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        TS = new TimeSheetModel();
                        TS.id = Convert.ToInt32(rd["id"]);
                        TS.timeSheetKey = Convert.ToString(rd["timeSheetKey"]);
                        TS.type = Convert.ToString(rd["type"]);
                        TS.startTime = Convert.ToString(rd["start_time"]);
                        TS.endTime = Convert.ToString(rd["end_time"]);
                        TS.breaks = Convert.ToString(rd["breaks"]);
                        TS.workTime = Convert.ToString(rd["work_time"]);
                        TS.notes = Convert.ToString(rd["notes"]);
                        TS.entryDate = Convert.ToDateTime(rd["entryDate"]);
                        TS.timeSheetStatus = Convert.ToString(rd["timeSheetStatus"]);
                        TS.m3 = Convert.ToInt32(rd["m3"]);
                        TS.kmStand = Convert.ToInt32(rd["km_stand"]);
                        TS.privat = Convert.ToInt32(rd["privat"]);
                        TS.fuel = Convert.ToInt32(rd["fuel"]);
                        TS.adblue = Convert.ToInt32(rd["adblue"]);

                        TS.UserId = Convert.ToInt32(rd["employeeId"]);

                        timeSheets.Add(TS);
                    }
                    con.Close();

                }
            }
            catch (Exception)
            {

                throw;
            }


            return timeSheets;
        }

        //From User Interface it edits and adds record in time sheet table
        [HttpPost("/api/createAuditFile")]
        public IActionResult EditUsersTimeSheet(List<EditTimeSheetModel> modelList)
        {

            using (var con = db.Gdb())
            {
                string startTime = null;
                string endTime = null;
                string breaks = null;
                AuditFileModel auditFile = null;
                List<AuditFileModel> auditFileList = new List<AuditFileModel>();

                foreach (EditTimeSheetModel item in modelList)
                {
                    SqlCommand cmd = new SqlCommand("dbo.spGetTimeSheetRecordById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", item.id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        startTime = Convert.ToString(rd["start_time"]);
                        endTime = Convert.ToString(rd["end_time"]);
                        breaks = Convert.ToString(rd["breaks"]);

                        if (startTime != item.startTime && item.startTime != null)
                        {
                            auditFile = new AuditFileModel();
                            auditFile.timeSheetKey = item.timeSheetKey;
                            auditFile.day = item.day;
                            auditFile.field = "Start Time";
                            auditFile.oldValue = startTime;
                            auditFile.newValue = item.startTime;
                            auditFile.dateOfChange = DateTime.Now;
                            auditFile.changedBy = item.changedBy;
                            auditFileList.Add(auditFile);

                        }
                        if (endTime != item.endTime && item.endTime != null)
                        {
                            auditFile = new AuditFileModel();
                            auditFile.timeSheetKey = item.timeSheetKey;
                            auditFile.day = item.day;
                            auditFile.field = "End Time";
                            auditFile.oldValue = endTime;
                            auditFile.newValue = item.endTime;
                            auditFile.dateOfChange = DateTime.Now;
                            auditFile.changedBy = item.changedBy;
                            auditFileList.Add(auditFile);
                        }
                        if (breaks != item.breaks && item.breaks != null)
                        {
                            auditFile = new AuditFileModel();
                            auditFile.timeSheetKey = item.timeSheetKey;
                            auditFile.day = item.day;
                            auditFile.field = "Break";
                            auditFile.oldValue = breaks;
                            auditFile.newValue = item.breaks;
                            auditFile.dateOfChange = DateTime.Now;
                            auditFile.changedBy = item.changedBy;
                            auditFileList.Add(auditFile);
                        }
                    }
                    con.Close();

                    DateTime StartTime = item.startTime != "-1" ? Convert.ToDateTime(item.startTime) : default;
                    DateTime EndTime = item.endTime != "-1" ? Convert.ToDateTime(item.endTime) : default;
                    var WorkTime = EndTime.Subtract(StartTime);
                    var conv = WorkTime.ToString(@"h\:mm");

                    cmd = new SqlCommand("dbo.spEditTimeSheetById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", item.id);
                    cmd.Parameters.AddWithValue("@startTime", item.startTime);
                    cmd.Parameters.AddWithValue("@endTime", item.endTime);
                    cmd.Parameters.AddWithValue("@breaks", item.breaks);
                    cmd.Parameters.AddWithValue("@workTime", conv);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }

                if (auditFileList.Count > 0)
                {
                    foreach (AuditFileModel item in auditFileList)
                    {
                        SqlCommand cmd = new SqlCommand("dbo.spCreateAuditFile", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@timeSheetKey", item.timeSheetKey);
                        cmd.Parameters.AddWithValue("@day", item.day);
                        cmd.Parameters.AddWithValue("@field", item.field);
                        cmd.Parameters.AddWithValue("@oldValue", item.oldValue);
                        cmd.Parameters.AddWithValue("@newValue", item.newValue);
                        cmd.Parameters.AddWithValue("@dateOfChange", item.dateOfChange);
                        cmd.Parameters.AddWithValue("@changedBy", item.changedBy);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }

                }
                return Ok(auditFileList);
            }


        }
        //From User interface returns all time sheets which have working progress status
        [HttpPost("/api/getTimeSheetByStatus")]
        public ActionResult GetTimeSheetByStatus(UpdateTimeSheetStatusModels data)
        {
            TimeSheetModel TS = null;
            List<TimeSheetModel> timeSheets = null;
            try
            {
                using (var con = db.Gdb())
                {
                    timeSheets = new List<TimeSheetModel>();
                    SqlCommand cmd = new SqlCommand("dbo.spGetTimeSheetByStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", data.userId);
                    cmd.Parameters.AddWithValue("@status", data.status);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        TS = new TimeSheetModel();
                        TS.id = Convert.ToInt32(rd["id"]);
                        TS.timeSheetKey = Convert.ToString(rd["timeSheetKey"]);
                        TS.type = Convert.ToString(rd["type"]);
                        TS.startTime = Convert.ToString(rd["start_time"]);
                        TS.endTime = Convert.ToString(rd["end_time"]);
                        TS.breaks = Convert.ToString(rd["breaks"]);
                        TS.workTime = Convert.ToString(rd["work_time"]);
                        TS.notes = Convert.ToString(rd["notes"]);
                        TS.entryDate = Convert.ToDateTime(rd["entryDate"]);
                        TS.timeSheetStatus = Convert.ToString(rd["timeSheetStatus"]);
                        TS.m3 = Convert.ToInt32(rd["m3"]);
                        TS.kmStand = Convert.ToInt32(rd["km_stand"]);
                        TS.privat = Convert.ToInt32(rd["privat"]);
                        TS.fuel = Convert.ToInt32(rd["fuel"]);
                        TS.adblue = Convert.ToInt32(rd["adblue"]);

                        TS.UserId = Convert.ToInt32(rd["employeeId"]);

                        timeSheets.Add(TS);
                    }
                    con.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(timeSheets);
        }
        
        //Updates time sheets status to approved or apprvoed by employee
        [HttpPost("/api/updateTimeSheetStatus")]
        [Produces("application/json")]
        public ActionResult UpdateTimeSheetStatus(UpdateTimeSheetStatusModels model)
        {
            using (var con = db.Gdb())
            {
                SqlCommand cmd = new SqlCommand("dbo.spUpdateStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", model.userId);
                cmd.Parameters.AddWithValue("@monthStart", Convert.ToDateTime(model.monthStart));
                cmd.Parameters.AddWithValue("@monthEnd", Convert.ToDateTime(model.monthEnd));
                cmd.Parameters.AddWithValue("@status", model.status);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            if (model.status == "approved by employee")
            {
                EmployeeModel EM = new EmployeeModel();
                using (var con = db.Gdb())
                {
                    SqlCommand cmdd = new SqlCommand($"SELECT * FROM Employee WHERE id = '{model.userId}'", con);
                    con.Open();
                    SqlDataReader rd = cmdd.ExecuteReader();



                    while (rd.Read())
                    {
                        EM = new EmployeeModel();
                        EM.id = Convert.ToInt32(rd["id"]);
                        EM.userName = Convert.ToString(rd["userName"]);
                        EM.level = Convert.ToString(rd["level_"]);

                    }
                }
                CreateTimeSheet(EM);
            }
            return Ok();
        }

        //return all audit files from database

        [HttpGet("/api/getAllAuditFiles")]
        [Produces("application/json")]
        public ActionResult GetAllAuditFiles()
        {
            AuditFileModel AF = null;
            List<AuditFileModel> AFList = new List<AuditFileModel>();
            try
            {
                using (var con = db.Gdb())
                {

                    string sql = $"SELECT * FROM AuditFile";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();

                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        AF = new AuditFileModel();
                        AF.timeSheetKey = Convert.ToString(rd["timeSheetKey"]);
                        AF.day = Convert.ToString(rd["day"]);
                        AF.field = Convert.ToString(rd["field"]);
                        AF.oldValue = Convert.ToString(rd["oldValue"]);
                        AF.newValue = Convert.ToString(rd["newValue"]);
                        AF.dateOfChange = Convert.ToDateTime(rd["dateOfChange"]);
                        AF.changedBy = Convert.ToString(rd["changedBy"]);

                        AFList.Add(AF);
                    }
                    con.Close();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(AFList);
        }


        //Export excel table for selected users and months
        [HttpPost("/api/exportTimeSheets")]
        [Produces("application/json")]
        public ActionResult ExportTimeSheets(List<GetTimeSheetModel> model)
        {
            List<List<TimeSheetModel>> timeSheets = new List<List<TimeSheetModel>>();

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[12] {new DataColumn("TimeSheetKey"),
                                        new DataColumn("Type"),
                                        new DataColumn("StartTime"),
                                        new DataColumn("EndTime"),
                                        new DataColumn("Breaks"),
                                        new DataColumn("WorkTime"),
                                        new DataColumn("m3"),
                                        new DataColumn("Km-Stand"),
                                        new DataColumn("Privat"),
                                        new DataColumn("Fuel"),
                                        new DataColumn("AdBlue"),
                                        new DataColumn("Notes"),});

            foreach (var item in model)
            {
                foreach (var sheet in GetTimeSheetsByEmployeeIdAndDate(item))
                {
                    if (sheet.startTime != "-1" || sheet.endTime != "-1" || sheet.breaks != "-1")
                    {
                        dt.Rows.Add(sheet.timeSheetKey == "-1" ? "" : sheet.timeSheetKey,
                        sheet.type == "-1" ? "" : sheet.type,
                        sheet.startTime == "-1" ? "" : sheet.startTime,
                        sheet.endTime == "-1" ? "" : sheet.endTime,
                        sheet.breaks == "-1" ? "" : sheet.breaks,
                        sheet.workTime == "-1" ? "" : sheet.workTime,
                        (int)(sheet.m3 == -1 ? 0 : sheet.m3),
                        (int)(sheet.kmStand == -1 ? 0 : sheet.kmStand),
                        (int)(sheet.privat == -1 ? 0 : sheet.privat),
                        (int)(sheet.fuel == -1 ? 0 : sheet.fuel),
                        (int)(sheet.adblue == -1 ? 0 : sheet.adblue),
                        sheet.notes == "-1" ? "" : sheet.notes);
                    }
                }

                timeSheets.Add(GetTimeSheetsByEmployeeIdAndDate(item));
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");

                }
            }
        }

        //Export pdf document for selected users and months

        [HttpPost("/api/exportTimeSheetPDF")]
        [Produces("application/json")]
        public ActionResult ExportPdf(List<GetTimeSheetModel> model)
        {
            List<List<TimeSheetModel>> timeSheets = new List<List<TimeSheetModel>>();

            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();

            Page page = pdfDocument.Pages.Add();
            page.PageInfo.IsLandscape = true;

            TextState tstate = new TextState();
            tstate.FontSize = 10;
            page.PageInfo.DefaultTextState = tstate;




            MarginInfo marginInfo = new MarginInfo();
            marginInfo.Left = 35;
            marginInfo.Right = 28;
            marginInfo.Top = 28;
            marginInfo.Bottom = 28;
            page.PageInfo.Margin = marginInfo;



            TextFragment text = new TextFragment("Time Sheets");

            text.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
            text.TextState.FontSize = 16;
            text.Margin.Bottom = 20;
            page.Paragraphs.Add(text);


            List<Table> tableList = new List<Table>();

            foreach (var item in model)
            {
                Table table = new Table
                {
                    ColumnWidths = "80, 50, 65, 65, 60, 65, 50, 65, 50, 50, 65, 50",
                    DefaultCellPadding = new MarginInfo(10, 5, 10, 5),
                    Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black),
                };

                Aspose.Pdf.Row row1 = table.Rows.Add();

                row1.Cells.Add("Time Sheet Key");
                row1.Cells.Add("Type");
                row1.Cells.Add("Start Time");
                row1.Cells.Add("End Time");
                row1.Cells.Add("Breaks");
                row1.Cells.Add("Work Time");
                row1.Cells.Add("m3");
                row1.Cells.Add("Km - Stand");
                row1.Cells.Add("Privat");
                row1.Cells.Add("Fuel");
                row1.Cells.Add("Ad Blue");
                row1.Cells.Add("Notes");

                foreach (var sheet in GetTimeSheetsByEmployeeIdAndDate(item))
                {
                    if (sheet.startTime != "-1" || sheet.endTime != "-1" || sheet.breaks != "-1")
                    {

                        Aspose.Pdf.Row row = table.Rows.Add();
                        row.Cells.Add(sheet.timeSheetKey == "-1" ? "" : sheet.timeSheetKey);
                        row.Cells.Add(sheet.type == "-1" ? "" : sheet.type);
                        row.Cells.Add(sheet.startTime == "-1" ? "" : sheet.startTime);
                        row.Cells.Add(sheet.endTime == "-1" ? "" : sheet.endTime);
                        row.Cells.Add(sheet.breaks == "-1" ? "" : sheet.breaks);
                        row.Cells.Add(sheet.workTime == "-1" ? "" : sheet.workTime);
                        row.Cells.Add(Convert.ToString(sheet.m3 == -1 ? 0 : sheet.m3));
                        row.Cells.Add(Convert.ToString(sheet.kmStand == -1 ? 0 : sheet.kmStand));
                        row.Cells.Add(Convert.ToString(sheet.privat == -1 ? 0 : sheet.privat));
                        row.Cells.Add(Convert.ToString(sheet.fuel == -1 ? 0 : sheet.fuel));
                        row.Cells.Add(Convert.ToString(sheet.adblue == -1 ? 0 : sheet.adblue));
                        row.Cells.Add(sheet.notes == "-1" ? "" : sheet.notes);
                    }
                }
                tableList.Add(table);
                timeSheets.Add(GetTimeSheetsByEmployeeIdAndDate(item));
            }

            foreach (var table in tableList)
            {
                pdfDocument.Pages[1].Paragraphs.Add(table);

                text = new TextFragment("");

                text.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                text.TextState.FontSize = 16;
                text.Margin.Bottom = 20;
                page.Paragraphs.Add(text);
            }

            using (var streamOut = new MemoryStream())
            {
                pdfDocument.Save(streamOut);
                return new FileContentResult(streamOut.ToArray(), "application/pdf");
            }
        }
    }
}
