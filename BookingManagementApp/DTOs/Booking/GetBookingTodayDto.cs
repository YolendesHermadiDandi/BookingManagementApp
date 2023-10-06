namespace API.DTOs.Booking
{
    public class GetBookingTodayDto
    {
        public Guid Guid { get; set; }
        public string RoomName {  get; set; }
        public string Status { get; set; }
        public int Floor { get; set; }
        public string BookedBy { get; set; }



    }
}
