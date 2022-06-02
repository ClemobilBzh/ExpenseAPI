using System;

namespace ExpenseAPI.Constants
{
    public static class ErrorMessage
    {
        public const string CURRENCY_REQUIRED = "CurrencyId must be informed";
        public const string CURRENCY_NOT_REGISTERED = "Currency must be registered before its use.";
        public const string CURRENCY_NOT_CONSISTANT = "Expense currency is inconsistent with user currency.";
        public const string DATE_IN_FUTURE = "Date cannot be in the future.";
        public const string DATE_TOO_OLD = "Date cannot be older than {0} months.";
        public const string DOUBLE_EXPENSE = "Expense has been already registered.";
        public const string NATURE_ID_REQUIRED = "Nature must be informed";
        public const string USER_ID_REQUIRED = "UserId must be informed";
        public const string USER_NOT_REGISTERED = "User must be registered before its use.";
    }
}
