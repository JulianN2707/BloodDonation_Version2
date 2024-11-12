using System.Collections;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Domain.Dto
{
    public class RecipientRequest
    {
        public string EMail { get; set; }
        public string? Subject { get; set; }
        public Hashtable TemplateVars { get; set; }
    }
}
