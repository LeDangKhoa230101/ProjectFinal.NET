using System.Text;

namespace ProjectFinal.NET.Helper
{
    public class Util
    {
        public static string GenerateRamdomkey(int length=  5) 
        {
          
                 var pattern = "qawszedcrfvtgbyhnujmiklopQAWSZEDCRFVTGBYHNUJMIKLOP1234567890!@#$%^&*()_+";
            // Kiểm tra độ dài yêu cầu
            length = Math.Max(length, 1); // Đảm bảo độ dài tối thiểu là 1
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++) { sb.Append(pattern[rd.Next(0, pattern.Length)]); }
            return sb.ToString();
        }
    }
}
