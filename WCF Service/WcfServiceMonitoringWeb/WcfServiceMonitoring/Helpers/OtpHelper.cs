using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WcfService
{
    public static class OtpHelper
    {
        private const string OTP_HEADER = "X-OTP";

        public static bool HasValidTotp(String otp, string key)
        {
               // We need to check the passcode against the past, current, and future passcodes
               
                if (!string.IsNullOrWhiteSpace(otp))
                {
                    if (TimeSensitivePassCode.GetListOfOTPs(key).Any(t => t.Equals(otp)))
                    {
                        return true;
                    }
                }

            
            return false;
        }
    }
}