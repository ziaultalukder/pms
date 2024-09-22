using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace PMS.Helpers
{
    public class Helper
    {
        public static async Task<Result> SaveSingleImage(string imageFile, string imagePathConstant, IHostEnvironment hostEnvironment)
        {
            var imagePath = ImageDirectory.CheckDirectory(hostEnvironment, imagePathConstant);
            if (imageFile != null || imageFile != "")
            {
                try
                {
                    int extentionStartIndex = imageFile.IndexOf('/');
                    int extensionEndIndex = imageFile.IndexOf(';');
                    int filetypeStartIndex = imageFile.IndexOf(':');
                    //string fileType = imageFile.Substring(filetypeStartIndex + 1, extentionStartIndex-(filetypeStartIndex + 1));
                    string fileExtension = imageFile.Substring(extentionStartIndex + 1, extensionEndIndex - (extentionStartIndex + 1));
                    bool isSaved = false;

                    if (imageFile.Contains(","))
                    {
                        imageFile = imageFile.Substring(imageFile.IndexOf(",") + 1);
                    }

                    byte[] imageInBytes = Convert.FromBase64String(imageFile);

                    var imageName = string.Format(@"{0}." + fileExtension, Guid.NewGuid().ToString().Replace("-", ""));
                    isSaved = await ImageDirectory.SaveImageInDirectory(imageInBytes, imagePath, imageName);
                    if (!isSaved)
                    {
                        ImageDirectory.RemoveExistingFile(hostEnvironment, imageName, imagePathConstant);
                        return Result.Failure(new List<string> { "Image Could Not Saved!!!!" });
                    }
                    else
                    {
                        return Result.Success(imagePathConstant + imageName);
                    }
                }
                catch (Exception ex)
                {
                    return Result.Success(imagePathConstant + "default.png");
                }
            }
            else
            {
                return Result.Success(imagePathConstant + "default.png");
            }

        }

        public static string GeneratePassword(int passLength)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz@#$&ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, passLength)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public static KeyValuePair<string, string> HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var saltString = Convert.ToBase64String(salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return new KeyValuePair<string, string>(saltString, hashed);
        }


        /// <summary>
        /// create hash of old password with password salt
        /// </summary>
        /// <param name="providedPassword"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        public static string HashPassword(string providedPassword, string passwordSalt)
        {
            // generate a 128-bit salt using a secure PRNG

            var saltByte = Convert.FromBase64String(passwordSalt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: providedPassword,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public static string Password(string providedPassword)
        {
            // generate a 128-bit salt using a secure PRNG

            var saltByte = Convert.FromBase64String(providedPassword);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: providedPassword,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public static string GetSqlCondition(string conditionClause, string conditionOperator)
        {
            if (string.IsNullOrWhiteSpace(conditionClause))
            {
                return " WHERE ";
            }
            else if (conditionOperator.ToUpper() == "AND")
            {
                return " AND ";
            }
            else if (conditionOperator.ToUpper() == "OR")
            {
                return " OR ";
            }
            else
            {
                return "";
            }


        }

        public static bool SendEmail(string toEmail, string subject, string body, byte[] invoiceData)
        {
            try
            {
                string senderEmail = "evillage.info.bd@gmail.com";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                var credential = new NetworkCredential()
                {
                    UserName = "evillage.info.bd@gmail.com",
                    Password = "evillage2022"
                };
                client.Credentials = credential;

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, body);
                mailMessage.IsBodyHtml = true;

                if (invoiceData != null)
                {
                    Guid guid = new Guid();
                    byte[] applicationPdfData = invoiceData;
                    Attachment attPdf = new Attachment(new MemoryStream(applicationPdfData), guid + ".pdf");
                    mailMessage.Attachments.Add(attPdf);
                }

                mailMessage.BodyEncoding = Encoding.UTF8;

                client.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static bool SendSms(string toNumber, string smsBody)
        {
            var url = "https://login.esms.com.bd/api/v3/sms/send";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = "Bearer 7|DkV4aXa6b2eiz68155UBAoVf5S0Pc79SqNyKDGC9";
            httpRequest.ContentType = "application/json";
            var myData = new
            {
                recipient = toNumber,
                sender_id = "8809612442292",
                type = "plain",
                message = smsBody
            };

            //Tranform it to Json object
            string data = JsonConvert.SerializeObject(myData);
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            return false;
        }

    }
}
