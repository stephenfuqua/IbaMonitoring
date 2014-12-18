using System;
using System.ComponentModel;
using System.Net.Mail;
using safnet.iba.Business.Components;
using safnet.iba.Properties;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Possible Role Types
    /// </summary>
    public enum Role : short
    {
        /// <summary>
        /// Default
        /// </summary>
        None = 0,
        /// <summary>
        /// Administrator
        /// </summary>
        Administrator = 1,
        /// <summary>
        /// Regular user
        /// </summary>
        RegularUser = 2
    }

    /// <summary>
    /// Possible Status Types
    /// </summary>
    public enum Status : short
    {
        /// <summary>
        /// Default
        /// </summary>
        None = 0,
        /// <summary>
        /// Pending user - not yet approved
        /// </summary>
        Pending = 1,
        /// <summary>
        /// Active user
        /// </summary>
        Active = 2,
        /// <summary>
        /// Inactive user
        /// </summary>
        Inactive = 3
    }

    /// <summary>
    /// Models a real-world person.
    /// </summary>
    public class Person : SafnetBaseEntity
    {
        /// <summary>
        /// Gets or sets a person's first / given name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a person's last / sur name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets a person's full name by combining first and last names.
        /// </summary>
        public string FullName { get { return FirstName + " " + LastName; } }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the OpenID used for authentication.
        /// </summary>
        /// <value>The open id.</value>
        public string OpenId { get; set; }

        // TODO: record active dates instead of just a boolean

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive { get { return PersonStatus == Status.Active; } }

        /// <summary>
        /// Gets a value indicating whether this instance is administrator.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is administrator; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdministrator { get { return PersonRole == Role.Administrator; } }

        /// <summary>
        /// Gets or sets a Address1 of this instance
        /// </summary>
        /// <value>Address1</value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets a Address2 of this instance
        /// </summary>
        /// <value>Address2</value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets a City of this instance
        /// </summary>
        /// <value>City</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a State of this instance
        /// </summary>
        /// <value>City</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets a zipe code of this instance
        /// </summary>
        /// <value>Status</value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets a Country of this instance
        /// </summary>
        /// <value>City</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets a HasPersonBeenTrained of this instance
        /// </summary>
        /// <value><c>true</c>if person is trained, otherwise<c>false</c></value>
        public bool HasBeenTrained { get; set; }

        /// <summary>
        /// Gets or sets a HasPersonClipboard of this instance
        /// </summary>
        /// <value><c>true</c>if person is trained, otherwise<c>false</c></value>
        public bool HasClipboard { get; set; }

        /// <summary>
        /// Gets or sets a RoleName of this instance
        /// </summary>
        /// <value>RoleName</value>
        public Role PersonRole { get; set; }

        /// <summary>
        /// Gets or sets the person status.
        /// </summary>
        /// <value>The person status.</value>
        public Status PersonStatus { get; set; }

        /// <summary>
        /// Factory method to create a new <see cref="Person"/> instance, automatically creating a new unique identifier.
        /// </summary>
        /// <returns>Instance of <see cref="Person"/></returns>
        public static Person CreatePerson()
        {
            Person newPerson = new Person();
            newPerson.SetNewId();
            return newPerson;
        }

        /// <summary>
        /// Sends the registration confirmation e-mail
        /// </summary>
        public void SendRegistrationConfirmation(SendCompletedDelegate sendCompleted)
        {
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                string body = string.Format(
@"Your registration on ibamonitoring.org has been received with the following information: 

First Name: {0}
Last Name: {1}
E-mail Address: {2}
Phone Number: {3}
Address 1: {4}
Address 2: {5}
City: {6}
State: {7}
Zip Code: {8}
OpenID: {9}

While this account remains in a pending state, you will not be able to login to the website. A site administrator must activate your account, at which time you will receive a second e-mail confirming account activation.

Please do not respond to this e-mail address.
", FirstName, LastName, EmailAddress, PhoneNumber, Address1, Address2, City, State, ZipCode, OpenId);

                string fromAddress = Settings.Default.ConfirmationEmailAddress;
                string bccAddress = Settings.Default.AdminEmailAddress;
                string subject = "ibamonitoring.org Registration Confirmation";

                SendEmail(body, fromAddress, bccAddress, subject, sendCompleted);
            }
        }

        /// <summary>
        /// Sends the activation confirmation e-mail
        /// </summary>
        public void SendActivationConfirmation(SendCompletedDelegate sendCompleted)
        {
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                string body = string.Format(
@"Your account on ibamonitoring.org has been activated with OpenID {0}. You can now login and submit observations or update your profile.

Please do not respond to this e-mail address.", OpenId);

                string fromAddress = Settings.Default.ConfirmationEmailAddress;
                string bccAddress = Settings.Default.AdminEmailAddress;
                string subject = "ibamonitoring.org Activation Confirmation";

                SendEmail(body, fromAddress, bccAddress, subject, sendCompleted);
            }
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="fromAddress">From address.</param>
        /// <param name="bccAddress">The BCC address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="sendCompleted">The send completed.</param>
        protected virtual void SendEmail(string body, string fromAddress, string bccAddress, string subject, SendCompletedDelegate sendCompleted)
        {
            // Send e-mail in a separate thread in order to keep the web site responsive.
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromAddress);
                message.To.Add(new MailAddress(EmailAddress));
                message.Bcc.Add(new MailAddress(bccAddress));
                message.Subject = subject;
                message.Body = body;

                SmtpClient smtp = new SmtpClient();
                smtp.SendCompleted += new SendCompletedEventHandler(sendCompleted);

                smtp.SendAsync(message, subject);
            }
            catch (SmtpFailedRecipientException)
            {
                // Well, not much we can do about that
            }
            catch (SmtpException smtpException)
            {
                Logger.Error(smtpException);
            }
            catch (ArgumentNullException argumentNullException)
            {
                Logger.Error(argumentNullException);
            }
            catch (InvalidOperationException argumentNullException)
            {
                Logger.Error(argumentNullException);
            }

        }

        /// <summary>
        /// Delegate for when SendComplete is called on e-mail.
        /// </summary>
        public delegate void SendCompletedDelegate(object sender, AsyncCompletedEventArgs e);
        
    }
}
