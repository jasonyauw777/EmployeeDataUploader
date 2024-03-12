using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Configuration;

namespace WindowsFormsApp1
{
    /***
        * REFER TO EPPlus Documentation
        * EPPlus is a Excel Sheet Library for .Net Framework/.Net Core
        * REFER to Nuget Package Manager for More Information
        * https://epplussoftware.com/docs/6.2/api/index.html
    ***/
    // MAIN Windows Form
    public partial class Form1 : Form
    {
        private SaveFileDialog sfd;
        private OpenFileDialog ofd;
        // use current working directory as initial value
        private string UploadedFilePath = Directory.GetCurrentDirectory();
        private string UploadedFileName = "";
        private string ErrorReportPath = "";
        private string ErrorReportName = "";
        // initialize boolean variable to true
        private bool isErrorFree = true;
        private bool changeErrorFreeVar = false;

        public Form1()
        {
            InitializeComponent();
            // Ignore Below
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sfd = new SaveFileDialog();
            ofd = new OpenFileDialog();
            //deleteExistingErrorReport();
        }

        private void Download_Template(object sender, EventArgs e)
        {
            // Get Template File Directory in Project Folder
            string current_directory = Directory.GetCurrentDirectory();
            //string template_directory = Directory.GetParent(Directory.GetParent(current_directory).ToString()).ToString();
            //string template_path = Path.Combine(template_directory, "ExcelTemplate", "Template_DaaS_Karyawan.xlsx");

            string template_path = Path.Combine(current_directory, "ExcelTemplate", "DaaS_Karyawan.xlsx");
            Console.WriteLine(template_path);

            // Check Template Exists
            FileInfo template = new FileInfo(template_path);
            if (template.Exists)
            {
                Console.WriteLine("Template Exists");
            }
            else
            {
                Console.WriteLine("Template Does Not Exist");
                return;
            }

            // Download Excel Template To Selected Directory
            sfd.FileName = "DaaS_Karyawan.xlsx";
            // Make sure file is stored in .xlsx format
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
           
            if (sfd.ShowDialog() == DialogResult.OK)
            {   
                // See Selected Directory in Console
                Console.WriteLine(Path.GetFullPath(sfd.FileName));

                // OPEN template in project folder
                using (ExcelPackage package = new ExcelPackage(template))
                {
                    // Add Dropdown List Validation
                    ExcelWorksheet sheet1 = package.Workbook.Worksheets[0];
                    //var SE = sheet1.DataValidations.AddListValidation("C:C");
                    //SE.AllowBlank = true;
                    //SE.Formula.Values.Add("ACTIVE");
                    //SE.Formula.Values.Add("NOT ACTIVE");
                    var G = sheet1.DataValidations.AddListValidation("O:O");
                    G.AllowBlank = true;
                    G.Formula.Values.Add("MALE");
                    G.Formula.Values.Add("FEMALE");
                    var MS = sheet1.DataValidations.AddListValidation("P:P");
                    MS.AllowBlank = true;
                    MS.Formula.Values.Add("MARRIED");
                    MS.Formula.Values.Add("SINGLE");
                    MS.Formula.Values.Add("DIVORCED");
                    var RS = sheet1.DataValidations.AddListValidation("Q:Q");
                    RS.AllowBlank = true;
                    RS.Formula.Values.Add("WIFE");
                    RS.Formula.Values.Add("CHILD");
                    RS.Formula.Values.Add("HUSBAND");
                    RS.Formula.Values.Add("EMPLOYEE");
                    var HS = sheet1.DataValidations.AddListValidation("X:X");
                    HS.AllowBlank = true;
                    HS.Formula.Values.Add("RUMAH DINAS");
                    HS.Formula.Values.Add("ORANG TUA");
                    HS.Formula.Values.Add("LAINNYA");
                    // Download Template to Selected Location 
                    FileInfo template_download = new FileInfo(Path.GetFullPath(sfd.FileName));
                    package.SaveAs(template_download);
                    package.Dispose();
                }
                // Create Notification
                Console.WriteLine("File Downloaded Successfully");
                DownloadSuccessful DSForm = new DownloadSuccessful(Path.GetFullPath(sfd.FileName));
                DSForm.Show();
            };
        }

        private void Browse_File(object sender, EventArgs e)
        {
            // Browse File
            Console.WriteLine("Browse File");
            // Make sure file is stored in .xlsx format
            ofd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                deleteExistingErrorReport();
                isErrorFree = true;
                changeErrorFreeVar = false;
                // UPDATE UploadedFilePath Global variable
                UploadedFilePath = ofd.FileName;
                UploadedFileName = Path.GetFileName(UploadedFilePath);
                UploadedFileName = UploadedFileName.Split('.')[0];
                //Console.WriteLine(UploadedFilePath);
                //Console.WriteLine(UploadedFileName);
                // UPDATE Label
                UploadedFileLabel.Text = "File Path : " + UploadedFilePath;
            }
        }
        
        private void Upload_Document(object sender, EventArgs e)
        {
            deleteExistingErrorReport();
            // Upload & Validate Document
            Console.WriteLine("Upload Document");
            // Validation
            // Read Uploaded Excel File
            //Console.WriteLine(UploadedFilePath);
            FileInfo uploadedFile = new FileInfo(UploadedFilePath);
            // Check Uploaded File Exists
            if (uploadedFile.Exists == false)
            {
                Console.WriteLine("Uploaded File Does Not Exists");
                return;
            }
            // File Successfully Uploaded
            // Generate Error Report
            else if (uploadedFile.Exists)
            {
                //Console.WriteLine("Uploaded File Exists");

                // Prepare Error Report
                Console.WriteLine("Report Generated");
                Generate_Error_Report();
                
            }
            // Traverse Excel File
            using (ExcelPackage uploadedPackage = new ExcelPackage(uploadedFile))
            {
                // get Worksheet
                ExcelWorksheet sheet1 = uploadedPackage.Workbook.Worksheets[0];
                // get column number
                //int colCount = sheet1.Dimension.End.Column;
                // get row number
                int rowCount = sheet1.Dimension.End.Row;

                Console.WriteLine("Column Count: " + sheet1.Dimension.End.Column);
                Console.WriteLine("Row Count: " + rowCount);

                // empty spreadsheet, invalid 
                if (rowCount == 1)
                {
                    isErrorFree = false;
                }
                // open error report
                FileInfo existingErrorReport = new FileInfo(ErrorReportPath);
                ExcelPackage errorPackage = new ExcelPackage(existingErrorReport);
                ExcelWorksheet errorWorksheet = errorPackage.Workbook.Worksheets[0];

                //int newRowCount = rowCount;
                // Delete Empty Rows
                List<int> emptyRows = new List<int>();
                for (int or = 2; or <= rowCount; or++)
                {
                    bool isEmpty_row = true;
                    for (int cc = 1; cc <= 26; cc++)
                    {
                        if (sheet1.Cells[or, cc].Value != null)
                        {
                            //Console.WriteLine(sheet1.Cells[or, cc].Value);
                            isEmpty_row = false;
                            break;
                        }
                    }
                    if (isEmpty_row == true)
                    {
                        //Console.WriteLine("empty row : " + or);
                        emptyRows.Add(or);
                    }
                    //Console.WriteLine(isEmpty_row);
                }
                
                // Skip 1st row (Column Names)
                // Read from 2nd row
                for (int r = 2; r <= rowCount; r++)
                {   
                    if (emptyRows.Contains(r))
                    {
                        continue;
                    }
                    string all_error_messages = "";
                    // Read from 1st column
                    for (int c = 1; c <= 26; c++)
                    {   
                        // skip columns that require no validation
                        if (c == 1 || c == 10 || c == 12 || c == 16 || c >= 20)
                        {
                            //Console.WriteLine("**No Validation Required**");
                            continue;
                        }
                        string cell_value;
                        if (sheet1.Cells[r, c].Value == null)
                        {
                            cell_value = null;
                        }
                        else
                        {
                            cell_value = sheet1.Cells[r, c].Value.ToString();
                        }
                        
                        // validate data in cell
                        List<string> error_messages = new List<string>();
                        string col_type = sheet1.Cells[1, c].Value.ToString();
                         
                        error_messages = Validate_Excel_Cell(col_type, cell_value);

                        // get error message/messages for the row
                        if (error_messages.Count != 0)
                        {
                            foreach(string em in error_messages)
                            {
                                //Console.WriteLine("**" + em + "**");
                                if (all_error_messages == "")
                                {
                                    all_error_messages += em;
                                }
                                else
                                {
                                    all_error_messages = all_error_messages + ", " + em;
                                }
                                
                            }
                        } 
                    }

                    // write to "ERROR" column
                    // contain all error messages for that row
                    //Console.WriteLine(all_error_messages);
                    if (all_error_messages.Equals("") != true)
                    {
                        //Add_ErrorMessageTo_Col(all_error_messages, r);
                        errorWorksheet.SetValue(r, 27, all_error_messages);    
                    }
                    Console.WriteLine("ROW: " + r);
                }
                // close error report 
                errorPackage.Save();
                errorPackage.Dispose();
            }
            // Validation Succesful
            // Send to Server
            if (isErrorFree)
            {
                string full_fileName = "DaaS_Karyawan" + ".xlsx";
                // :D Location
                //Untuk Sementara Buat Bukti File.Copy() Jalan
                string D_PATH = ConfigurationManager.AppSettings["D"];
                if (!Directory.Exists(D_PATH))
                {
                    Directory.CreateDirectory(D_PATH);
                }
                File.Copy(UploadedFilePath, D_PATH + full_fileName, true);
                //UploadSuccessful USForm = new UploadSuccessful();
                //USForm.Show();

                // ITPFIF Location
                // BUTUH AKSES
                string ITPFIF_PATH_QC = ConfigurationManager.AppSettings["ITPFIF-QC"];
                string ITPFIF_PATH_STAGING = ConfigurationManager.AppSettings["ITPFIF-STAGING"];
                string ITPFIF_PATH_PROD = ConfigurationManager.AppSettings["ITPFIF-PROD"];

                string ITPFIF_PATH = ITPFIF_PATH_QC;
                Console.WriteLine(ITPFIF_PATH + full_fileName);
                try
                {
                    if (!Directory.Exists(ITPFIF_PATH))
                    {
                        Directory.CreateDirectory(ITPFIF_PATH);
                    }
                    File.Copy(UploadedFilePath, ITPFIF_PATH + full_fileName, true);
                    UploadSuccessful USForm = new UploadSuccessful();
                    USForm.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No Access to sharing Folder");
                    UploadSuccessful USForm = new UploadSuccessful();
                    USForm.Show();
                    return;
                }

            }
            // Validation Error
            else
            {
                // pass FilePath to error report 
                UploadFailed UFForm = new UploadFailed(ErrorReportPath, ErrorReportName);
                UFForm.Show();
            }
        }

        // Function Column Validation
        private List<string> Validate_Excel_Cell(string col_type, string cell_value)
        {
            // If list is NULL, no error for cell value

            List<string> errorMessagesList = new List<string>();

            switch (col_type)
            {   
                // done
                case "NPK":
                    // field is empty
                    if (cell_value == null)
                    {
                        errorMessagesList.Add("Please fill NPK field");
                        break;
                    }
                    // NPK must contain only numbers
                    if (Regex.IsMatch(cell_value, @"^\d+$") != true)
                    {
                        errorMessagesList.Add("NPK must contain only numbers");
                    }
                    break;
                // done
                case "STATUS_EMPLOYEE":
                    // field is empty
                    if (cell_value == null)
                    {
                        errorMessagesList.Add("Please fill STATUS_EMPLOYEE field");
                    }
                    break;
                // done
                case "NAME":
                    // field is empty
                    if (cell_value == null)
                    {
                        errorMessagesList.Add("Please fill NAME field");
                        break;
                    }
                    // name is not in UPPERCASE
                    if (IsAllUpper(cell_value) == false)
                    {
                        errorMessagesList.Add("NAME must be written in capital letters");
                    }
                    break;
                // done
                case "ID_NUMBER":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill ID_NUMBER field");
                        break;
                    }
                    // ID_Number must only contain numbers 
                    if (Regex.IsMatch(cell_value, @"^\d+$") != true)
                    {
                        errorMessagesList.Add("ID_NUMBER must contain only numbers");
                    }
                    break;
                // done
                case "NPWP":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill NPWP field");
                        break;
                    }
                    // NPWP must only contain numbers
                    if (Regex.IsMatch(cell_value, @"^\d+$") != true)
                    {
                        errorMessagesList.Add("NPWP must contain only numbers");
                    }
                    // NPWP > 16 characters
                    if (cell_value.Length > 16)
                    {
                        errorMessagesList.Add("NPWP can not be more than 16 characters");
                    }
                    // NPWP < 15 characters
                    if (cell_value.Length < 15)
                    {
                        errorMessagesList.Add("NPWP can not be less than 15 characters");
                    }
                    break;
                // done
                case "NOMOR_KK":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill NOMOR_KK field");
                        break;
                    }
                    // Nomor_KK must only contain numbers
                    if (Regex.IsMatch(cell_value, @"^\d+$") != true)
                    {
                        errorMessagesList.Add("NOMOR_KK must contain only numbers");
                    }
                    // Nomor_KK > 16 characters
                    if (cell_value.Length > 16)
                    {
                        errorMessagesList.Add("NOMOR_KK can not be more than 16 characters");
                    }
                    // Nomor_KK < 16 characters
                    if (cell_value.Length < 16)
                    {
                        errorMessagesList.Add("NOMOR_KK can not be less than 16 characters");
                    }
                    break;
                // done
                case "DOB":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill DOB field");
                        break;
                    }
                    // DOB must be written in ddmmyyyy format
                    DateTime parsed;
                    string s = cell_value;
                    if (DateTime.TryParseExact(s, "ddMMyy",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out parsed) != true)
                    {
                        errorMessagesList.Add("DOB must be written with the format ddMMyy");
                    }
                    break;
                // done
                case "YOB":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill YOB field");
                        break;
                    }
                    // YOB > 4 characters
                    if (cell_value.Length > 4)
                    {
                        errorMessagesList.Add("YOB can not be more than 4 characters");
                    }
                    // YOB < 4 characters
                    if (cell_value.Length < 4)
                    {
                        errorMessagesList.Add("YOB can not be less than 4 characters");
                    }
                    // YOB must only contain numbers   
                    if (Regex.IsMatch(cell_value, @"^\d+$") != true)
                    {
                        errorMessagesList.Add("YOB must contain only numbers");
                    }
                    break;
                // done
                case "ZIPCODE":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill ZIPCODE field");
                        break;
                    }
                    // ZIPCODE must only contain numbers 
                    if (Regex.IsMatch(cell_value, @"^\d+$") != true)
                    {
                        errorMessagesList.Add("ZIPCODE must contain only numbers");
                    }
                    // ZIPCODE > 5 characters
                    if (cell_value.Length > 5)
                    {
                        errorMessagesList.Add("ZIPCODE can not be more than 5 characters");
                    }
                    // ZIPCODE < 5 characters
                    if (cell_value.Length < 5)
                    {
                        errorMessagesList.Add("ZIPCODE can not be less than 5 characters");
                    }
                    break;
                // done
                case "PHONE":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill Phone field");
                        break;
                    }
                    // PHONE must start with 08
                    if (cell_value[0] != '0' && cell_value[1] != '8')
                    {
                        errorMessagesList.Add("PHONE must start with 08");
                    }
                    // PHONE > 15 characters 
                    if (cell_value.Length > 15)
                    {
                        errorMessagesList.Add("PHONE can not be more than 15 characters");
                    }
                    break;
                // done
                case "EMAIL":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill EMAIL field");
                        break;
                    }
                    string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
                    // EMAIL not in proper format
                    if (Regex.IsMatch(cell_value, regex, RegexOptions.IgnoreCase) != true)
                    {
                        errorMessagesList.Add("EMAIL must be written with the email format (with @ and .)");
                    }
                    break;
                // done
                case "GENDER":
                    // GENDER not filled 
                    if (cell_value == null)
                    {
                        errorMessagesList.Add("Please fill GENDER field.");
                    }
                    break;
                // done
                case "RELATION_STATUS":
                    // RELATION_STATUS not filled 
                    if (cell_value == null)
                    {
                        errorMessagesList.Add("Please fill RELATION_STATUS field.");
                    }
                    break;
                // done
                case "START_DATE":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill START_DATE field");
                        break;
                    }
                    // START_DATE must be ddmmyyyy Format
                    DateTime parsed2;
                    string s2 = cell_value;
                    if (DateTime.TryParseExact(s2, "ddMMyyyy",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out parsed2) != true)
                    {
                        errorMessagesList.Add("START_DATE must be written with the format ddMMyyyy");
                    }
                    break;
                //done
                case "END_DATE":
                    if (cell_value == null)
                    {
                        //errorMessagesList.Add("Please fill END_DATE field");
                        break;
                    }
                    // END_DATE must be ddmmyyyy Format
                    DateTime parsed3;
                    string s3 = cell_value;
                    if (DateTime.TryParseExact(s3, "ddMMyyyy",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out parsed3) != true)
                    {
                        errorMessagesList.Add("END_DATE must be written with the format ddMMyyyy");
                    }
                    break;
            }
            
            // error exists in excel file
            if (errorMessagesList.Count >= 1 && changeErrorFreeVar == false)
            {   
                //updateBooleanVariable
                isErrorFree = false;
                changeErrorFreeVar = true;
            }
            return errorMessagesList;
        }

        // Function Check If All UPPERCASE
        private bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {   
                if (input[i] == ' ')
                {
                    continue;
                }
                if (!Char.IsUpper(input[i]))
                {
                    return false;
                }
            }

            return true;
        }

        // Function Generate Error Report
        private void Generate_Error_Report()
        {
            // Get ErrorReport Folder Directory in Project 
            
            // Check Template Exists
            FileInfo errorReportTemplate = new FileInfo(UploadedFilePath);
            if (errorReportTemplate.Exists)
            {
                Console.WriteLine("Template Exists");
            }
            else
            {
                Console.WriteLine("Template Does Not Exist");
            }

            // See Error Report Directory in Console
            string error_report_name = UploadedFileName + "_Error.xlsx";
            string error_report_path = Path.Combine("ErrorReport", error_report_name);

            // Update ErrorReportName & Path Values
            ErrorReportName = error_report_name;
            ErrorReportPath = error_report_path;
            //Console.WriteLine(ErrorReportPath);
            
            using (ExcelPackage errorReportPackage = new ExcelPackage(errorReportTemplate))
            {
                // Add Error Column
                // get Worksheet
                ExcelWorksheet sheet1 = errorReportPackage.Workbook.Worksheets[0];
                // add error column di paling kanan
                //sheet1.InsertColumn(27, 1);
                sheet1.SetValue(1, 27, "ERROR");
                // Save Error Report to ErrorReport Folder
                FileInfo error_report = new FileInfo(Path.GetFullPath(ErrorReportPath));
                errorReportPackage.SaveAs(error_report);
                errorReportPackage.Dispose();
            }
        }
        // Every time app is started again
        // All existing files in ErrorReport are deleted
        private void deleteExistingErrorReport()
        {
            string current_directory = Directory.GetCurrentDirectory();
            //string ErrorReportParent_directory = Directory.GetParent(Directory.GetParent(current_directory).ToString()).ToString();
            string ErrorReport_directory = Path.Combine(current_directory, "ErrorReport");
            
            //Console.WriteLine(ErrorReport_directory);
            string[] files = Directory.GetFiles(ErrorReport_directory);
            foreach (string file in files)
            {
                File.Delete(file);
            }
            ErrorReportName = "";
            ErrorReportPath = "";
        }
    }
}
