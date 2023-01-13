using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFixStationBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportDishIngredientViewModel> DishIngredients { get; set; }
        public List<ReportWareHouseIngredientViewModel> WareHouseIngredients { get; set; }
    }
}