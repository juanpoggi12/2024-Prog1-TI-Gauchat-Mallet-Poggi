namespace TIEntities
{
    public class Result
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public Result() {
        Success = true;
        }
    }