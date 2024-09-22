﻿namespace PMS.Helpers
{
    public class Result
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
        internal Result(bool succeed, IEnumerable<string> errors, string success)
        {
            Succeed = succeed;
            Errors = errors.ToArray();
            Message = success;
        }

        public static Result Success(string success = "Success")
        {
            return new Result(true, new string[] { }, success);
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors, "Failed");
        }
    }
}
