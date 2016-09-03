namespace WebErpExt5.Models
{
    public class ResponseObject
    {
        public ResponseObject(bool wasSuccessful)
        {
            success = wasSuccessful;
            error = new SubmitError();
        }

        public ResponseObject(bool wasSuccessful, string errorCode, string msg)
        {
            success = wasSuccessful;
            error = new SubmitError { code = errorCode, message = msg };
        }

        public bool success { get; set; }
        public SubmitError error { get; set; }
    }

    public class SubmitError
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
