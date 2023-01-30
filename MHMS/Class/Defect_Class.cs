using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHMS.Class
{
    class Defect_Class
    {
        public string QualityIssueNo { get; set; }
        public string Date { get; set; }
        public string Shift { get; set; }
        public string Time { get; set; }
        public string DefectCategory { get; set; }
        public string DefectType { get; set; }
        public string Issue { get; set; }
        public string TotalNumberOfDefect { get; set; }
        public string QuantityAffected { get; set; }
        public string DefectRate { get; set; }
        public string AffectedQtyToBeSort { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public string PartsLotNumber { get; set; }
        public string AffectedCassettePONumber { get; set; }
        public string POQuantity { get; set; }
        public string ProcessDetected { get; set; }
        public string Line { get; set; }
	    public string DetectedBy { get; set; }
        public string LiableArea { get; set; }
        public string LiablePerson { get; set; }
        public string LiableLine { get; set; }
        public string ImmediateAction { get; set; }
        public string ProductionPIC { get; set; }
        public string PQAIncharge { get; set; }
        public string TechnicianPIC { get; set; }
        public string QGPIC { get; set; }
        public string OtherDetails { get; set; }
        public string Investigation { get; set; }
        public string DetailsHowTheyDetectTheNG { get; set; }
        public string UploadDate { get; set; }
    }
}
