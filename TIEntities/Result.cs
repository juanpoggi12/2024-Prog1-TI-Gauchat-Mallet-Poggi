using System.Runtime.InteropServices.ObjectiveC;

namespace TIEntities
{
    public class Result
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int Data {  get; set; }
        public Object objeto {  get; set; }

        public Result()
        {
            Success = false;
        }
    }
}