namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Domain.Dto
{
    public class RecipientResult
    {
        public string EMail { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
