namespace KUSYS_Demo_Web_Uygulamasi.Services.IServices
{
    public interface ITokenHandler
    {
        DTOs.TokenDTO CreateAccessToken(int second);
    }
}
