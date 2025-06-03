using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using MimeKit;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.API.MiscAPI
{
    public class MessagingAPI
    {
        private readonly IError _error;
        public MessagingAPI(IError error)
        {
            _error = error;
        }
        public bool SendBanEmail(BanReport report)
        {
            try
            {
               
                using ( var ctx = new MainContext() ) 
                    {
                    string subject = "Account suspended - Retro Console Store";
                    UDBTablecs userM = ctx.Users.FirstOrDefault(p => p.id == report.UserID);
                    string body = GenerateBanEmailTemplate(report, userM.email, userM.username);
                    SendEmail(userM.email, subject,body);
                    return true;
                    }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Issue sending ban email ");
            }
            return true;
        }
        public bool SendEmail(string sendTo, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    WebConfigurationManager.AppSettings["FromName"],
                    WebConfigurationManager.AppSettings["FromEmail"]
                    ));
                message.To.Add(new MailboxAddress("", sendTo));
                message.Subject = subject;
                message.Body = new TextPart("html") { Text = body };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(
                        WebConfigurationManager.AppSettings["SmtpHost"],
                        int.Parse(WebConfigurationManager.AppSettings["SmtpPort"]),
                        bool.Parse(WebConfigurationManager.AppSettings["EnableSsl"])
                        );
                    client.Authenticate(
                          WebConfigurationManager.AppSettings["SmtpUsername"],
                          WebConfigurationManager.AppSettings["SmtpPassword"]
                                       );

                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error sending email ");
                return false;
            }
        }
        private string GenerateBanEmailTemplate(BanReport report, string userEmail, string username)
        {
            bool isPermanent = report.numberOfDays == 0;
           

            return $@"
            <!DOCTYPE html>
            <html>
            <head>
            <meta charset='utf-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <style>
        @import url('https://fonts.googleapis.com/css2?family=VT323&display=swap');
        
        body {{ 
            font-family: 'VT323', monospace; 
            margin: 0; 
            padding: 20px; 
            background: linear-gradient(135deg, #1a0b2e 0%, #2a1144 100%);
            color: #ffffff;
            line-height: 1.6;
        }}
        
        .container {{ 
            max-width: 600px; 
            margin: 0 auto; 
            background-color: #2a1144; 
            border: 3px solid #e6007e;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(230, 0, 126, 0.3);
        }}
        
        .header {{ 
            background: linear-gradient(45deg, #e6007e, #ff1493);
            color: #ffffff; 
            padding: 30px 20px; 
            text-align: center; 
            border-radius: 7px 7px 0 0;
            border-bottom: 3px solid #ffcc33;
        }}
        
        .header h1 {{ 
            margin: 0; 
            font-size: 2.5em;
            text-shadow: 2px 2px 4px rgba(0,0,0,0.5);
            letter-spacing: 2px;
        }}
        
        .header h2 {{ 
            margin: 10px 0 0 0; 
            font-size: 1.5em;
            color: #ffcc33;
        }}
        
        .content {{ 
            padding: 30px 25px;
            background-color: #1a0b2e;
        }}
        
        .greeting {{ 
            font-size: 1.3em; 
            margin-bottom: 20px;
            color: #ffcc33;
        }}
        
        .ban-alert {{ 
            background: linear-gradient(45deg, #ff4444, #cc0000);
            border: 2px solid #ffcc33;
            border-radius: 8px;
            padding: 20px;
            margin: 20px 0;
            text-align: center;
        }}
        
        .ban-details {{ 
            background-color: #2a1144; 
            border: 2px solid #e6007e;
            border-radius: 8px;
            padding: 20px; 
            margin: 20px 0;
        }}
        
        .ban-details h3 {{ 
            margin: 0 0 15px 0;
            color: #ffcc33;
            font-size: 1.4em;
            border-bottom: 2px solid #e6007e;
            padding-bottom: 10px;
        }}
        
        .detail-row {{ 
            display: flex;
            margin: 12px 0;
            padding: 8px 0;
            border-bottom: 1px solid rgba(230, 0, 126, 0.3);
        }}
        
        .detail-label {{ 
            font-weight: bold;
            color: #e6007e;
            min-width: 120px;
            font-size: 1.1em;
        }}
        
        .detail-value {{ 
            color: #ffffff;
            font-size: 1.1em;
        }}
        
        .permanent {{ 
            color: #ff4444;
            font-weight: bold;
            text-transform: uppercase;
        }}
        
        .temporary {{ 
            color: #ffcc33;
            font-weight: bold;
        }}
        
        .contact-info {{ 
            background-color: #2a1144;
            border: 2px solid #ffcc33;
            border-radius: 8px;
            padding: 20px;
            margin: 25px 0;
            text-align: center;
        }}
        
        .footer {{ 
            background-color: #1a0b2e;
            border-top: 3px solid #e6007e;
            padding: 20px; 
            text-align: center; 
            border-radius: 0 0 7px 7px;
            color: #999;
            font-size: 0.9em;
        }}
        
        .pixel-border {{ 
            border: 2px solid #e6007e;
            background: repeating-linear-gradient(
                90deg,
                transparent,
                transparent 2px,
                #e6007e 2px,
                #e6007e 4px
            );
            height: 4px;
            margin: 20px 0;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎮 RETRO CONSOLE STORE</h1>
            <h2>⚠️ ACCOUNT SUSPENSION ALERT ⚠️</h2>
        </div>
        
        <div class='content'>
            <div class='greeting'>
                Hello <strong>{username}</strong>,
            </div>
            
            <div class='ban-alert'>
                <h3>🚫 YOUR ACCOUNT HAS BEEN SUSPENDED 🚫</h3>
            </div>
            
            <p>We regret to inform you that your RetroConsoleStore account has been suspended due to a violation of our community guidelines.</p>
            
            <div class='ban-details'>
                <h3>📋 SUSPENSION DETAILS</h3>
                
                <div class='detail-row'>
                    <div class='detail-label'>📅 Date:</div>
                    <div class='detail-value'>{DateTime.Now:dddd, MMMM dd, yyyy 'at' HH:mm}</div>
                </div>
                
                <div class='detail-row'>
                    <div class='detail-label'>⚖️ Reason:</div>
                    <div class='detail-value'>{report.reason}</div>
                </div>
                
                <div class='detail-row'>
                    <div class='detail-label'>⏰ Duration:</div>
                    <div class='detail-value {(isPermanent ? "permanent" : "temporary")}'>
                        {(isPermanent ? "🔒 PERMANENT SUSPENSION" : $"Until {DateTime.Now.AddDays(report.numberOfDays):dddd, MMMM dd, yyyy 'at' HH:mm}")}
                    </div>
                </div>
                
                <div class='detail-row'>
                    <div class='detail-label'>👤 Admin:</div>
                    <div class='detail-value'>{report.adminB.Name}</div>
                </div>
                
                <div class='detail-row'>
                    <div class='detail-label'>🌐 IP Address:</div>
                    <div class='detail-value'>{report.UserIp}</div>
                </div>
            </div>
            
            <div class='pixel-border'></div>
            
            <p><strong>What this means:</strong></p>
            <ul style='color: #ffcc33; padding-left: 20px;'>
                <li>🚫 You cannot log into your account</li>
                <li>🛒 All pending orders remain valid</li>
                <li>💳 No new purchases can be made</li>
                <li>🎮 Access to retro gaming content is restricted</li>
            </ul>
            
            <div class='contact-info'>
                <h4 style='color: #ffcc33; margin: 0 0 10px 0;'>📞 NEED HELP?</h4>
                <p style='margin: 0;'>If you believe this suspension was issued in error, please contact our support team with your account details and a detailed explanation.</p>
            </div>
            
            <p style='margin-top: 25px;'><em>Thank you for your understanding as we work to maintain a safe and enjoyable gaming community for all players.</em></p>
        </div>
        
        <div class='footer'>
            <div class='pixel-border'></div>
            <p><strong>🎮 RetroConsoleStore</strong> - Your Ultimate Retro Gaming Destination</p>
            <p>© 2024 RetroConsoleStore. All rights reserved.</p>
            <p style='font-size: 0.8em; margin-top: 15px;'>
                This is an automated message. Please do not reply directly to this email.<br>
                For support inquiries, please contact our customer service team.
            </p>
        </div>
    </div>
</body>
</html>";
        }

    }
}
