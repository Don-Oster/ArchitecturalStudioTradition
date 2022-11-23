namespace ArchitecturalStudioTradition.Contract.v1
{
    public abstract class ResponseBase
    {
        protected ResponseBase()
        {
            Errors = Enumerable.Empty<string>().ToArray();
        }

        public int AppId { get; }
        public string[] Errors { get; private set; }

        protected void AppendErrors(params string[] errors)
        {
            Errors = (Errors ?? Enumerable.Empty<string>()).Union(errors).ToArray();
        }
    }
}
