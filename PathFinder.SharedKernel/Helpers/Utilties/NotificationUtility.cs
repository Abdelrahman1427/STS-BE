using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using STS.Common.Helpers.Models;
using Twilio.Http;

namespace STS.SharedKernel.Helpers.Utilties
{
    public static class NotificationUtility
    {
        public static NotifiactionsResult SendNotification(List<string> tokens, string title, string body)
        {
            NotifiactionsResult finalResult = new NotifiactionsResult();
            if (tokens.Count > 0 && tokens != null)
            {
                if (FirebaseApp.DefaultInstance == null)
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile("FirebaseConfig.json")
                    });
                }
                var message = new MulticastMessage()
                {
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body,
                    },
                    Tokens = tokens,

                    Apns = new ApnsConfig
                    {
                        Aps = new Aps
                        {
                            Sound = "DefaultSound"
                        }
                    }
                };
                var response = FirebaseMessaging.DefaultInstance.SendMulticastAsync(message).Result;
                List<SendNotifiactionsStatus> listNotificationsStatus = new List<SendNotifiactionsStatus>();
                for (int index = 0; index < response.Responses.Count(); index++)
                {
                    SendNotifiactionsStatus sendNotifiactions = new SendNotifiactionsStatus()
                    {
                        IsSuccess = response.Responses[index].IsSuccess,
                        Token = tokens[index]
                    };
                    listNotificationsStatus.Add(sendNotifiactions);
                }
                finalResult.FinalStatus = response.FailureCount == 0;
                finalResult.notifiactionsStatuses = listNotificationsStatus;
                return finalResult;
            }
            return finalResult;
        }

        public static string SendNotification(string token, string title, string body)
        {
            string response = string.Empty;
            if (token != null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("FirebaseConfig.json")
                });

                var message = new Message()
                {
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body
                    },
                    Token = token
                };
                response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
            }
            return response;
        }
    }
}


