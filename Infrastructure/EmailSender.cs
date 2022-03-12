using Core.Models.Requests;

namespace Infrastructure;

public class EmailSender
{
    public static void Send(EmailRequest request)
    {
        //simulate building email body & retrieving property info from database
        Console.WriteLine($"Sending email to {request.EmailTo} from {request.Email}");
        Thread.Sleep(2000);
        Console.WriteLine($"Body: propertyId - {request.PropertyId}\n{request.Text}");
    }
}
