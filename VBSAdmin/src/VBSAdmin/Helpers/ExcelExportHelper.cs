using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using VBSAdmin.Data.VBSAdminModels;

namespace VBSAdmin.Helpers
{
    public class ExcelExportHelper
    {
        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        public static byte[] ExportAttendanceExcel(List<Classroom> classrooms)
        {
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                foreach (Classroom classroom in classrooms)
                {
                    string className = classroom.Session.Period + " " + classroom.Grade + " " + classroom.Name;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(className);
                    worksheet.Cells["A1"].Value = "Child Name";
                    worksheet.Cells["B1"].Value = "Monday";
                    worksheet.Cells["C1"].Value = "Tuesday";
                    worksheet.Cells["D1"].Value = "Wednesday";
                    worksheet.Cells["E1"].Value = "Thursday";
                    worksheet.Cells["F1"].Value = "Friday";
                    worksheet.Cells["A1:F1"].Style.Font.Bold = true;
                    worksheet.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    List<Child> sortedChildren = classroom.Children.OrderBy(c => c.LastName).ToList();
                    int rowCount = 2;
                    foreach (Child child in sortedChildren)
                    {
                        worksheet.Cells["A" + rowCount].Value = child.LastName + ", " + child.FirstName;
                        rowCount++;
                    }

                    worksheet.Column(1).AutoFit();
                    for (int i = 2; i <= 7; i++)
                    {
                        worksheet.Column(i).Width = 12;
                    }
                }

                result = package.GetAsByteArray();
            }

            return result;
        }

    }
}
