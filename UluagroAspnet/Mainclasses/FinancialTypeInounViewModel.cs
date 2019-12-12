using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.ClassGetAjaxs;
using UluagroAspnet.Models;

namespace UluagroAspnet.Mainclasses
{
    public class FinancialTypeInounViewModel
    {
        public List<CopyPayment> _AllFinancialReport { get; set; }
        public List<PAYMENT_TYPE> _PaymentType { get; set; }
        public List<PAYMENT_INOUT> _PaymentInout { get; set; }
    }
}