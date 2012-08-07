using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Oktogo.UserManagement.Web.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAddressAttribute : RegularExpressionAttribute
    {
        private const string EmailPattern = @"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,6}$";

        static EmailAddressAttribute()
        {
            // necessary to enable client side validation
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailAddressAttribute), typeof(RegularExpressionAttributeAdapter));
        }

        public EmailAddressAttribute()
            : base(EmailPattern)
        {
        }
    }
}