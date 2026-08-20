using System.Text.Json;
using TicketTracker.Services;

namespace TicketTracker.Services
{
    public class MapService
    {
        private readonly HttpClient _httpClient;

        public MapService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // Required by Nominatim's usage policy.
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "TicketTracker/1.0 (college-project)"
            );
        }

        public async Task<List<MapPoint>> GetMapPointsAsync(
            List<MapPoint> existingPoints)
        {
            var updatedPoints = new List<MapPoint>();

            foreach (var point in existingPoints)
            {
                try
                {
                    string url =
                        $"https://nominatim.openstreetmap.org/search" +
                        $"?q={Uri.EscapeDataString(point.CityName)}" +
                        $"&format=json&limit=1";

                    var response = await _httpClient.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        updatedPoints.Add(point);
                        continue;
                    }

                    var json = await response.Content.ReadAsStringAsync();

                    var results = JsonSerializer.Deserialize<
                        List<NominatimResult>
                    >(json);

                    var result = results?.FirstOrDefault();

                    if (result != null &&
                        double.TryParse(
                            result.lat,
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out double latitude) &&
                        double.TryParse(
                            result.lon,
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out double longitude))
                    {
                        updatedPoints.Add(new MapPoint
                        {
                            CityName = point.CityName,
                            Latitude = latitude,
                            Longitude = longitude,
                            Label = point.Label
                        });
                    }
                    else
                    {
                        // If API doesn't return a result,
                        // keep the coordinates from your database.
                        updatedPoints.Add(point);
                    }
                }
                catch
                {
                    // If API fails, use your database coordinates.
                    updatedPoints.Add(point);
                }

                // Public Nominatim service has a usage limit.
                await Task.Delay(1100);
            }

            return updatedPoints;
        }
    }

    public class NominatimResult
    {
        public string lat { get; set; } = "";
        public string lon { get; set; } = "";
        public string display_name { get; set; } = "";
    }
}