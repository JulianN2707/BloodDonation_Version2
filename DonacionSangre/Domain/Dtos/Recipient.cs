using System.Collections;

namespace DonacionSangre.Domain.Dtos
{
    public class Recipient
    {
        public required string EMail { get; set; }
        public required Hashtable TemplateVars { get; set; }
    }
}
