using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PVA.Web.Data
{
    [MetadataType(typeof(Metadata))]
    public partial class UserLoginDetail
    {
        internal class Metadata
        {
            [ScaffoldColumn(false)]
            public string ID { get; set; }

            [Display(Name = "User Type")]
            public string UserType { get; set; }

            [Display(Name = "Login ID")]
            public string LoginID { get; set; }

            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "First Name")]
            public string Name { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string State { get; set; }

            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please provide valid mobile number")]
            [Display(Name = "Mobile Number")]
            public string MobileNo { get; set; }

            public string LandLineNo { get; set; }

            [EmailAddress(ErrorMessage = "Please provide valid email address")]
            //[DataType(DataType.EmailAddress, ErrorMessage ="Please provide valid email address")]
            [Display(Name = "Email Address")]
            public string EmailAddress { get; set; }

            public string Gender { get; set; }

            [Display(Name = "Is Active")]
            public Nullable<bool> IsActive { get; set; }

            [Display(Name = "Company Code")]
            public string CompanyCode { get; set; }


            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedDate { get; set; }
            public string ModifiedBy { get; set; }
            public Nullable<System.DateTime> ModifiedDate { get; set; }
            public string EnteredOnMachineID { get; set; }
            public string ExeVersionNo { get; set; }
            public Nullable<bool> IsApproved { get; set; }
            public string ApprovedBy { get; set; }
            public Nullable<System.DateTime> ApprovedOn { get; set; }
            public string ModuleName { get; set; }
            public string Salutation { get; set; }
            public string FaxNo { get; set; }
            public Nullable<bool> IsEmailAddressVerified { get; set; }
            public Nullable<bool> IsVerificationMailSend { get; set; }
            public string CircleId { get; set; }
            public string Zone { get; set; }
            public Nullable<System.DateTime> LastQueryDateTime { get; set; }
            public Nullable<int> PendingRowsCount { get; set; }
            public Nullable<int> PendingImageCount { get; set; }
            public string ApkVersionNo { get; set; }
            public Nullable<System.DateTime> DataSyncDateTime { get; set; }
            public string DeviceID { get; set; }
            public string Status { get; set; }
            public string APSyncModifiedBy { get; set; }
            public Nullable<System.DateTime> RequestedOn { get; set; }
            public string RequestedBy { get; set; }
            public Nullable<System.DateTime> SysCreatedDateTime { get; set; }
            public Nullable<System.DateTime> SysModifiedDateTime { get; set; }
            public Nullable<bool> ScanOnly { get; set; }
            public Nullable<bool> ScanOnly2 { get; set; }
            public Nullable<bool> MFScanOnly { get; set; }
            public string PVAgencyName { get; set; }
            public Nullable<bool> isDemoUser { get; set; }
        }
    }
}