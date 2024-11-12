namespace DonacionSangre.Domain.Dtos
{
    public class ReservaDto
    {
        public Guid Id { get; set; }              
        public Guid DonanteId { get; set; }      
        public DateTime FechaReserva { get; set; } 
        public string Estado { get; set; }        
        public string Zona { get; set; }          
    }
}
