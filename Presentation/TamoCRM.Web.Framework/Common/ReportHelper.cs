using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Reports;

namespace TamoCRM.Web.Framework.Common
{
    public class ReportHelper
    {
        public static void ToExcel(List<TmpJobReportImportExportInfo> list, ReportModel model)
        {
            var pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Báo cáo");

            // Header
            AddCellHeader(ws, "Báo cáo xuất nhập tồn kho L1 MOL mới V1.0", 1, 1, 1, 6);

            // HeaderInfo
            var cell = ws.Cells[2, 1];
            AddCellHeadeInfo(ws, "Từ ngày", 2, 1);
            AddCellHeadeInfo(ws, "Đến ngày", 2, 3);
            AddCellHeadeInfo(ws, model.DateTimeFrom.Value.ToString("dd/MM/yyyy"), 2, 2);
            AddCellHeadeInfo(ws, model.DateTimeTo.Value.ToString("dd/MM/yyyy"), 2, 4);

            // HeaderTitle
            AddCellHeaderTitle(ws, "Kênh", 4, 1, 5, 1);

            int a = 2;
            var days = (model.DateTimeTo - model.DateTimeFrom).Value.Days;
            for (var j = 0; j <= days; j++)
            {
                AddCellHeaderTitle(ws, model.DateTimeFrom.Value.AddDays(j).ToString("dd/MM/yyyy"), 4, a, 4, a + 3);
                AddCellHeaderTitle(ws, "Tồn đầu ngày", 5, a, 5, a);
                AddCellHeaderTitle(ws, "Phát sinh tăng", 5, a + 1, 5, a + 1);
                AddCellHeaderTitle(ws, "Phát sinh giảm", 5, a + 2, 5, a + 2);
                AddCellHeaderTitle(ws, "Tồn cuối ngày", 5, a + 3, 5, a + 3);
                a = a + 4;
            }

            var rowMarket = 7;
            var itemChannels = list.Select(c => new { c.TenKenh, c.TonDauNgay, c.PhatSinhTang, c.PhatSinhGiam, c.TonCuoiNgay, c.NgayBaoCao })
                                   .Distinct()
                                   .ToList();

            for (int i = 0; i < itemChannels.Count; i++)
            {
                // Add Channel
                AddCellText(ws, itemChannels[i].TenKenh, rowMarket + i, 1);
                int b = 2;
                for (var h = 0; h <= days; h++)
                {
                    var date = model.DateTimeFrom.Value.AddDays(h);

                    var item = itemChannels.Where(c => c.TenKenh == itemChannels[i].TenKenh)
                                           .Where(c => c.NgayBaoCao == date)
                                           .ToList();

                    if (!item.IsNullOrEmpty())
                    {
                        var TondauNgay = item[0] == null ? 0 : item[0].TonDauNgay;
                        AddCellText(ws, TondauNgay.ToString(), i + rowMarket, b);

                        var PhatSinhTang = item[0] == null ? 0 : item[0].PhatSinhTang;
                        AddCellText(ws, PhatSinhTang.ToString(), i + rowMarket, b + 1);

                        var PhatSinhGiam = item[0] == null ? 0 : item[0].PhatSinhGiam;
                        AddCellText(ws, PhatSinhGiam.ToString(), i + rowMarket, b + 2);

                        var TonCuoiNgay = item[0] == null ? 0 : item[0].TonCuoiNgay;
                        AddCellText(ws, TonCuoiNgay.ToString(), i + rowMarket, b + 3);
                    }

                    ////Add ton dau ngay
                    //var TonDauNgay = itemChannels[i].TonDauNgay;
                    //AddCellText(ws, TonDauNgay.ToString(), i + rowMarket, b);

                    ////Add phat sinh tang
                    //var PhatSinhTang = itemChannels[i].PhatSinhTang;
                    //AddCellText(ws, PhatSinhTang.ToString(), i + rowMarket, b + 1);

                    ////Add phat sinh giam
                    //var PhatSinhGiam = itemChannels[i].PhatSinhGiam;
                    //AddCellText(ws, PhatSinhGiam.ToString(), i + rowMarket, b + 2);

                    ////Add ton cuoi ngay
                    //var TonCuoiNgay = itemChannels[i].TonCuoiNgay;
                    //AddCellText(ws, TonCuoiNgay.ToString(), i + rowMarket, b + 3);

                    b = b + 4;
                }
            }
            ToExcel(pck, model);
        }

        private static void AddCellHeadeInfo(ExcelWorksheet ws, string value, int fromRow, int fromCol, int toRow = 0, int toCol = 0)
        {
            var cell = ws.Cells[fromRow, fromCol, toRow == 0 ? fromRow : toRow, toCol == 0 ? fromCol : toCol];
            cell.Value = value;
            cell.Style.WrapText = true;
            cell.Style.Font.Bold = true;
            cell.Merge = (toRow != 0 || toCol != 0);
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            cell.AutoFitColumns(12);
        }

        private static void AddCellHeaderTitle(ExcelWorksheet ws, string value, int fromRow, int fromCol, int toRow = 0, int toCol = 0)
        {
            var cell = ws.Cells[fromRow, fromCol, toRow == 0 ? fromRow : toRow, toCol == 0 ? fromCol : toCol];
            cell.Value = value;
            cell.Style.Font.Bold = true;
            cell.Merge = (toRow != 0 || toCol != 0);
            cell.Style.Font.Color.SetColor(Color.White);
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Fill.BackgroundColor.SetColor(Color.Blue);
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.AutoFitColumns(12);
        }

        private static void AddCellText(ExcelWorksheet ws, string value, int fromRow, int fromCol, int toRow = 0, int toCol = 0)
        {
            var cell = ws.Cells[fromRow, fromCol, toRow == 0 ? fromRow : toRow, toCol == 0 ? fromCol : toCol];
            cell.Value = value;
            cell.Merge = (toRow != 0 || toCol != 0);
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Fill.BackgroundColor.SetColor(Color.White);
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.AutoFitColumns(12);
        }

        private static void AddCellHeader(ExcelWorksheet ws, string value, int fromRow, int fromCol, int toRow = 0, int toCol = 0)
        {
            var cell = ws.Cells[fromRow, fromCol, toRow == 0 ? fromRow : toRow, toCol == 0 ? fromCol : toCol];
            cell.Value = value;
            cell.Style.Font.Size = 15;
            cell.Style.Font.Bold = true;
            ws.Row(fromRow).Height = 37.5;
            cell.Merge = (toRow != 0 || toCol != 0);
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            cell.AutoFitColumns(12);
        }

        private static void ToExcel(ExcelPackage pck, ReportModel model)
        {
            //Write it back to the client
            var file = "BaoCaoXuatNhapTon_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xlsx";
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + file);
            HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
            HttpContext.Current.Response.End();
        }
    }
}