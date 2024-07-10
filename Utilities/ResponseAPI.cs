namespace LoginApiTest.Utilities
{
    public class ResponseAPI<T>
    {
        public T? Value { get; set; }
        public string msg {  get; set; }
        public bool status { get; set; }

    }
}
