namespace Salon_Management_NET.Model.ResponseDto
{
    public class LoginResult
    {

        public string token {  get; set; }
        public DateTime Expiry { get; set; }
    }
}
