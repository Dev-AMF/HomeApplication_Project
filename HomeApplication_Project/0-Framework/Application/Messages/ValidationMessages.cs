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
        public const string PasswordNotMatch = "رمز عبور مطابقت ندارد.";
        public const string InvalidMobileFormat = "فرمت شماره موبایل وارد شده صحیح نیست!";
        public const string InvalidPassword = "گذرواژه باید بین 6 الی 15 کاراکتر باشد.";
        public const string PasswordsDontMatch = "گذرواژه ها با یکدیگر مطابقت ندارند!!";
    }
}
