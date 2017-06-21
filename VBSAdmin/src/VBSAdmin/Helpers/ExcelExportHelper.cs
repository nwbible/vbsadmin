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
                    string className = classroom.Session.Period + " " + classroom.Grade.GetDisplayName() + " " + classroom.Name;
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

        public static byte[] ExportVBSExcel(List<Classroom> classrooms, List<Child> children)
        {
            byte[] result = null;

            using (ExcelPackage package = new ExcelPackage())
            {
                //Export children to worksheet
                ExcelWorksheet childrenWorksheet = package.Workbook.Worksheets.Add("Children");
                childrenWorksheet.Cells["A1"].Value = "Session";
                childrenWorksheet.Cells["B1"].Value = "Child Name";
                childrenWorksheet.Cells["C1"].Value = "Date of Birth";
                childrenWorksheet.Cells["D1"].Value = "Gender";
                childrenWorksheet.Cells["E1"].Value = "Address1";
                childrenWorksheet.Cells["F1"].Value = "Address2";
                childrenWorksheet.Cells["G1"].Value = "City";
                childrenWorksheet.Cells["H1"].Value = "State";
                childrenWorksheet.Cells["I1"].Value = "Zip Code";
                childrenWorksheet.Cells["J1"].Value = "Grade Completed";
                childrenWorksheet.Cells["K1"].Value = "Attend NWB";
                childrenWorksheet.Cells["L1"].Value = "Home Church";
                childrenWorksheet.Cells["M1"].Value = "Invited By";
                childrenWorksheet.Cells["N1"].Value = "Assigned Class";
                childrenWorksheet.Cells["O1"].Value = "Place Child With Request";
                childrenWorksheet.Cells["P1"].Value = "Accepted Christ";
                childrenWorksheet.Cells["Q1"].Value = "Guardian Name";
                childrenWorksheet.Cells["R1"].Value = "Guardian Relationship";
                childrenWorksheet.Cells["S1"].Value = "Guardian Email";
                childrenWorksheet.Cells["T1"].Value = "Guardian Phone";
                childrenWorksheet.Cells["U1"].Value = "Emergency Contact Name";
                childrenWorksheet.Cells["V1"].Value = "Emergency Contact Relationship";
                childrenWorksheet.Cells["W1"].Value = "Emergency Contact Phone";
                childrenWorksheet.Cells["X1"].Value = "Allergies";
                childrenWorksheet.Cells["Y1"].Value = "Medical Conditions";
                childrenWorksheet.Cells["Z1"].Value = "Medications";
                childrenWorksheet.Cells["A1:Z1"].Style.Font.Bold = true;

                int rowCount = 2;
                foreach (Child child in children)
                {
                    childrenWorksheet.Cells["A" + rowCount].Value = child.Session.Period;
                    childrenWorksheet.Cells["B" + rowCount].Value = child.FirstName + " " + child.LastName;
                    childrenWorksheet.Cells["C" + rowCount].Value = child.DateOfBirth.ToString("d");
                    childrenWorksheet.Cells["D" + rowCount].Value = child.Gender;
                    childrenWorksheet.Cells["E" + rowCount].Value = child.Address1;
                    childrenWorksheet.Cells["F" + rowCount].Value = child.Address2;
                    childrenWorksheet.Cells["G" + rowCount].Value = child.City;
                    childrenWorksheet.Cells["H" + rowCount].Value = child.State;
                    childrenWorksheet.Cells["I" + rowCount].Value = child.Zip;
                    childrenWorksheet.Cells["J" + rowCount].Value = child.GradeCompleted.GetDisplayName();
                    childrenWorksheet.Cells["K" + rowCount].Value = child.AttendHostChurch;
                    childrenWorksheet.Cells["L" + rowCount].Value = child.HomeChurch;
                    childrenWorksheet.Cells["M" + rowCount].Value = child.InvitedBy;
                    childrenWorksheet.Cells["N" + rowCount].Value = (child.Classroom != null) ? child.Classroom.Session.Period + " " + child.Classroom.Grade.GetDisplayName() + " " + child.Classroom.Name : "Not Assigned";
                    childrenWorksheet.Cells["O" + rowCount].Value = child.PlaceChildWithRequest;
                    childrenWorksheet.Cells["P" + rowCount].Value = child.DecisionMade;
                    childrenWorksheet.Cells["Q" + rowCount].Value = child.GuardianFirstName + " " + child.GuardianLastName;
                    childrenWorksheet.Cells["R" + rowCount].Value = child.GuardianChildRelationship;
                    childrenWorksheet.Cells["S" + rowCount].Value = child.GuardianEmail;
                    childrenWorksheet.Cells["T" + rowCount].Value = child.GuardianPhone;
                    childrenWorksheet.Cells["U" + rowCount].Value = child.EmergencyContactFirstName + " " + child.EmergencyContactLastName;
                    childrenWorksheet.Cells["V" + rowCount].Value = child.EmergencyContactChildRelationship;
                    childrenWorksheet.Cells["W" + rowCount].Value = child.EmergencyContactPhone;
                    childrenWorksheet.Cells["X" + rowCount].Value = child.AllergiesDescription;
                    childrenWorksheet.Cells["Y" + rowCount].Value = child.MedicalConditionDescription;
                    childrenWorksheet.Cells["Z" + rowCount].Value = child.MedicationDescription;
                    rowCount++;
                }

                //Export classes to worksheet
                ExcelWorksheet classWorksheet = package.Workbook.Worksheets.Add("Classes");
                classWorksheet.Cells["A1"].Value = "Class";
                classWorksheet.Cells["A1"].Style.Font.Bold = true;

                rowCount = 2;
                foreach (Classroom classroom in classrooms)
                {
                    classWorksheet.Cells["A" + rowCount].Value = classroom.Session.Period + " " + classroom.Grade.GetDisplayName() + " " + classroom.Name;
                    rowCount++;
                }
                classWorksheet.Column(1).AutoFit();




                result = package.GetAsByteArray();
            }

            return result;
        }
    }
}
