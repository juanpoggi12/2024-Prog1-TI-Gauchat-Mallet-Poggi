using System.Runtime.InteropServices.ObjectiveC;

namespace TIEntities
{
    public class Result
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int Status {  get; set; }
        public Object Data {  get; set; }

        public Result()
        {
            Success = false;
        }
    }
}