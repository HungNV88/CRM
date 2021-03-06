using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using TamoCRM.Domain.Table;
using TamoCRM.Services.Table;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Tables;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class TableController : AdminController
    {
        public ActionResult TruncateTable()
        {
            var model = new TableModel
            {
                PostedTable = new PostedTable(),
                AllTables = TableRepository.GetAll(),
                SelectedTable = new Collection<TableInfo>(),
            };
            model.AllTables = model.AllTables
                .Where(c => !c.Name.Equals("Branches"))
                .Where(c => !c.Name.Equals("Channels"))
                .Where(c => !c.Name.Equals("Collectors"))
                .Where(c => !c.Name.Equals("Container"))
                .Where(c => !c.Name.Equals("EducationLevels"))
                .Where(c => !c.Name.Equals("LandingPage"))
                .Where(c => !c.Name.Equals("Levels"))
                .Where(c => !c.Name.Equals("Package"))
                .Where(c => !c.Name.Equals("SearchKeyword"))
                .Where(c => !c.Name.Equals("SourceTypes"))
                .Where(c => !c.Name.Equals("TeacherType"))
                .Where(c => !c.Name.Equals("TemplateAds"))
                .Where(c => !c.Name.Equals("TimeSlot"))
                .Where(c => !c.Name.Equals("EmailTemplate"))
                .Where(c => !c.Name.Equals("WebServiceConfig"))
                .Where(c => !c.Name.Equals("CampaindTpe"));
            return View(model);
        }

        [HttpPost]
        public ActionResult TruncateTable(TableModel model)
        {
            if (model.PostedTable != null && model.PostedTable.Name.Any())
            {
                var names = string.Join(",", model.PostedTable.Name.Select(c => c.Trim()));
                TableRepository.Truncate(names);
                ViewBag.Message = "Truncate thành công";
            }
            return TruncateTable();
        }
    }
}

