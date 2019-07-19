using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVA.Web.Models
{
    public class ExcelImportModel
    {
        public string BA { get; set; }
        public string AssetId { get; set; }
        public string Process { get; set; }
        public string Product { get; set; }
        public string Pack { get; set; }
        public string EquipmentDetails { get; set; }
        public string AssetDescription { get; set; }
        public string InstallationStatus { get; set; }
    }
}