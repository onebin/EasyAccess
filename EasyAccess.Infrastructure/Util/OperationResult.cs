using EasyAccess.Infrastructure.Constant;
using EasyAccess.Infrastructure.Util.DataConverter;

namespace EasyAccess.Infrastructure.Util
{
    public class OperationResult
    {
        public OperationResult() { }

        public OperationResult(ResultStatus status)
        {
            this.Status = status;
        }

        public OperationResult(ResultStatus status, string message)
            : this(status)
        {
            this.Message = message;
        }

        public OperationResult(ResultStatus status, string message, object data)
            :this(status, message)
        {
            this.Data = data;
        }

        public OperationResult SetData<T>(T data) where T : class
        {
            this.Data = new DataConverter<T>().ToDictionary(data);
            return this;
        }

        public OperationResult SetExtras(object extras)
        {
            this.Extras = extras;
            return this;
        }

        public ResultStatus Status { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public object Extras { get; set; }
    }
}
