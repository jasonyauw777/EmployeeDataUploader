using System;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace WindowsFormsApp1
{
    public partial class UploadFailed : Form
    {
        private string ErrorReportFilePath = "";
        private string ErrorReportFileName = "";
        private SaveFileDialog sfd;

        public UploadFailed(string ErrorReportPath, string ErrorReportName)
        {
            InitializeComponent();
            ErrorReportFilePath = ErrorReportPath;
            ErrorReportFileName = ErrorReportName;
            // Ignore Below
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
        }

        private void UploadFailed_Load(object sender, EventArgs e)
        {
            sfd = new SaveFileDialog();
        }

        private void Download_Error_Report(object sender, EventArgs e)
        {
            Console.WriteLine(ErrorReportFilePath);
            // Check Template Exists
            FileInfo ErrorReport = new FileInfo(ErrorReportFilePath);
            if (ErrorReport.Exists)
            {
                Console.WriteLine("Error Report Exists");
            }
            else
            {
                Console.WriteLine("Error Report Does Not Exist");
                return;
            }

            // Download ErrorReport To Selected Directory
            sfd.FileName = ErrorReportFileName;
            // Make sure file is stored in .xlsx format
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // See Selected Directory in Console
                Console.WriteLine(Path.GetFullPath(sfd.FileName));
                // OPEN generated error report in project folder
                using (ExcelPackage ErrorReportPackage = new ExcelPackage(ErrorReport))
                {
                    // Download Error Report to Selected Location 
                    FileInfo error_report_download = new FileInfo(Path.GetFullPath(sfd.FileName));
                    ErrorReportPackage.SaveAs(error_report_download);
                }

                DownloadSuccessful DSForm = new DownloadSuccessful(Path.GetFullPath(sfd.FileName));
                DSForm.Show();
            }
        }
    }
}