namespace EcommerceApiTest.Model
{
    public class Picture
    {
        public int PictureId { get; set; }
        public byte[] Pictures { get; set; }
        public DateTime UplodeDate { get; set; }= DateTime.Now;
    }
}
