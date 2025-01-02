namespace Services
{
    public class CitiesService
    {
        private List<string> _cities;
        public CitiesService() {
            _cities = new List<string>() 
            {
            "London", "Rome", "Paris", "France", "Tokyo"
            };
        }

        public List<string> GetCities() { return _cities; }
    }


}
