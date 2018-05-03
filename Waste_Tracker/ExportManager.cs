using System;

using System.Runtime.InteropServices;

namespace Waste_Tracker
{
    public static class ExportManager
    {
        public static void ExportToExcel(object[,] data)
        {

            dynamic excel = Microsoft.VisualBasic.Interaction.CreateObject("Excel.Application", string.Empty);


            excel.ScreenUpdating = false;
            dynamic workbook = excel.workbooks;
            workbook.Add();

            dynamic worksheet = excel.ActiveSheet;

            const int left = 1;
            const int top = 1;
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            int bottom = top + height - 1;
            int right = left + width - 1;

            if (height == 0 || width == 0)
                return;

            dynamic rg = worksheet.Range[worksheet.Cells[top, left], worksheet.Cells[bottom, right]];


            rg.Value = data;


            // Set borders
            for (int i = 1; i <= 4; i++)
                rg.Borders[i].LineStyle = 1;

            // Set auto columns width
            rg.EntireColumn.AutoFit();

            // Set header view
            dynamic rgHeader = worksheet.Range[worksheet.Cells[top, left], worksheet.Cells[top, right]];
            rgHeader.Font.Bold = true;
            rgHeader.Interior.Color = 189 * (int)Math.Pow(16, 4) + 129 * (int)Math.Pow(16, 2) + 78; // #4E81BD

            // Show excel app
            excel.ScreenUpdating = true;
            excel.Visible = true;


            Marshal.ReleaseComObject(rg);
            Marshal.ReleaseComObject(rgHeader);
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excel);

            rg = null;
            rgHeader = null;
            worksheet = null;
            workbook = null;
            excel = null;
            GC.Collect();
        }
    }
}
