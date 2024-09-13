using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace PathFinder.BusinessLogic.Helpers
{
    public class Convertions
    {
        private readonly IServer _server;
        public Convertions(IServer server)
        {
            _server = server;
        }

        public string buildPath(string path)
        {
            var adresses = _server.Features.Get<IServerAddressesFeature>().Addresses.ToList<string>();
            if (!string.IsNullOrEmpty(path))
            {
                var uri = path.Replace(@"\", @"/");
                if (adresses is not null && adresses.Count > default(int))
                {
                    return $"{adresses[0]}{uri}";
                }
                return uri;
            }
            else
                return $"{adresses[0]}/Images/NoImage.jpg";
        }
        public string buildProfileImagePath(string path)
        {
            var adresses = _server.Features.Get<IServerAddressesFeature>().Addresses.ToList<string>();
            if (!string.IsNullOrEmpty(path))
            {
                var uri = path.Replace(@"\", @"/");
                if (adresses is not null && adresses.Count > default(int))
                {
                    return $"{adresses[0]}{uri}";
                }
                return uri;
            }
            else
                return $"{adresses[0]}/Images/NoProfile.png";
        }

        public DateTime? ToDateTime(string dateOnly)
        {
            try
            {
                if (!string.IsNullOrEmpty(dateOnly))
                    return DateTime.Parse(dateOnly);
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TimeSpan? ToTimeSpan(string timeOnly)
        {
            try
            {
                if (!string.IsNullOrEmpty(timeOnly))
                    return TimeSpan.Parse(timeOnly);
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string ToDateOnly(DateTime? dateTime)
        {
            try
            {
                return dateTime?.ToString("dd-MM-yyyy");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string ToTimeOnly(TimeSpan? timeSpan)
        {
            try
            {
                return timeSpan?.ToString(@"hh\:mm");
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
