using System;

// Single Responsibility Principle (SRP)
public class Notification
{
    public string Message { get; set; }
}

// Open/Closed Principle (OCP)
public interface INotificationSender
{
    void Send(Notification notification);
}

public class EmailSender : INotificationSender
{
    public void Send(Notification notification)
    {
        Console.WriteLine($"Sending Email: {notification.Message}");
    }
}

public class SMSSender : INotificationSender
{
    public void Send(Notification notification)
    {
        Console.WriteLine($"Sending SMS: {notification.Message}");
    }
}

// Liskov Substitution Principle (LSP)
public class NotificationService
{
    private readonly INotificationSender _notificationSender;

    public NotificationService(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    public void Notify(string message)
    {
        var notification = new Notification { Message = message };
        _notificationSender.Send(notification);
    }
}

// Interface Segregation Principle (ISP)
public interface IEmailService
{
    void SendEmail(Notification notification);
}

public interface ISMSService
{
    void SendSMS(Notification notification);
}

// Dependency Inversion Principle (DIP)
public class EmailService : IEmailService
{
    public void SendEmail(Notification notification)
    {
        Console.WriteLine($"Sending Email: {notification.Message}");
    }
}

public class SMSService : ISMSService
{
    public void SendSMS(Notification notification)
    {
        Console.WriteLine($"Sending SMS: {notification.Message}");
    }
}

// Main Program to demonstrate usage
class Program
{
    static void Main(string[] args)
    {
        // Using Email Sender
        var emailService = new EmailService();
        var emailNotification = new NotificationService(emailService);
        emailNotification.Notify("Hello via Email!");

        // Using SMS Sender
        var smsService = new SMSSender();
        var smsNotification = new NotificationService(smsService);
        smsNotification.Notify("Hello via SMS!");
    }
}
