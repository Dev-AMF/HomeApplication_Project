using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public static class ValidationMessages
    {
        public const string IsRequired = "این مقدار نمیتواند خالی باشد.";
        public const string MustBeUnsigned = "این مقدار نمیتواند منفی باشد.";
        public const string FileSizeExceeded = "حجم فایل بیش از حد مجاز است!";
        public const string FormatIsNotValid = "فرمت فایل مجاز نیست!";
    }
}
